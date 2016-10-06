using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ASong
{
    public class Disassembler
    {
        private static readonly string[] instruments = { "Drums/Acoustic Grand", "Bright Acoustic Piano", "Electric Piano", "Honky-tonk Piano", "Electric Piano 1", "Electric Piano 2", "Harpischord", "Clavi", "Celesta", "Glockenspiel", "Music Box", "Vibraphone", "Marimba", "Xylophone", "Tubular Bells", "Dulcimer", "Drawbar Organ", "Percussive Organ", "Rock Organ", "Church Organ", "Reed Organ", "Accordion", "Harmonica", "Tango Harmonica", "Acoustic Guitar (nylon)", "Acoustic Guitar (steel)", "Electric Guitar (jazz)", "Electric Guitar (clean)", "Electric Guitar (muted)", "Overdriven Guitar", "Distortion Guitar", "Guitar Harmonics", "Acoustic Bass", "Electric Bass (finger)", "Electric Bass (pick)", "Fretless Bass", "Slap Bass 1", "Slap Bass 2", "Synth Bass 1", "Synth Bass 2", "Violin", "Viola", "Cello", "Contrabass", "Tremolo Strings", "Pizzicato Strings", "Orchestral Harp", "Timpani", "String Ensemble 1", "String Ensemble 2", "SynthStrings 1", "SynthStrings 2", "Choir Aahs", "Choir Oohs", "Synth Voice", "Orchestra Hit", "Trumpet", "Trombone", "Tuba", "Muted Trumpet", "French Horn", "Brass Section", "SynthBrass 1", "SynthBrass 2", "Soprano Sax", "Alto Sax", "Tenor Sax", "Baritone Sax", "Oboe", "English Horn", "Bassoon", "Clarinet", "Piccolo", "Flute", "Recorder", "Pan Flute", "Blown Bottle", "Shakuhachi", "Whistle", "Ocarina", "Lead 1 (square)", "Lead 2 (sawtooth)", "Lead 3 (calliope)", "Lead 4 (chiff)", "Lead 5 (charang)", "Lead 6 (voice)", "Lead 7 (fifths)", "Lead 8 (bass + lead)", "Pad 1 (new age)", "Pad 2 (warm)", "Pad 3 (polysynth)", "Pad 4 (choir)", "Pad 4 (bowed)", "Pad 6 (metallic)", "Pad 7 (halo)", "Pad 8 (sweep)", "FX 1 (rain)", "FX 2 (soundtrack)", "FX 3 (crystal)", "FX 4 (atmosphere)", "FX 5 (brightness)", "FX 6 (goblins)", "FX 7 (echoes)", "FX 8 (sci-fi)", "Sitar", "Banjo", "Shamisen", "Koto", "Kalimba", "Bag Pipe", "Fiddle", "Shanai", "Tinkle Bell", "Agogo", "Steel Drums", "Woodblock", "Taiko Drum", "Melodic Tom", "Synth Drum", "Reverse Cymbal", "Guitar Fret Noise", "Breath Noise", "Seashore", "Bird Tweet", "Telephone Ring", "Helicopter", "Applause", "Gunshot" };

        public static void DisassembleM4A(string output, string rom, SongHeader song)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(rom));
            StreamWriter sw = File.CreateText(output);

            string songName = Path.GetFileNameWithoutExtension(output).Replace(' ', '_');

            // .s File Header for Songs
            sw.WriteLine("\t.include\t\"MPlayDef.s\"");
            sw.WriteLine();

            sw.WriteLine("\t.equ\t" + songName + "_grp, voicegroup000");
            sw.WriteLine("\t.equ\t" + songName + "_pri, " + song.Priority);
            sw.WriteLine("\t.equ\t" + songName + "_rev, " + song.Reverb);
            // I don't think we really need this.
            //sw.WriteLine("\t.equ\t" + songName + "_mvl, 127");
            //sw.WriteLine("\t.equ\t" + songName + "_key, 0");
            //sw.WriteLine("\t.equ\t" + songName + "_tbs, 1");
            //sw.WriteLine("\t.equ\t" + songName + "_exg, 0");
            //sw.WriteLine("\t.equ\t" + songName + "_cmp, 1");
            sw.WriteLine();

            sw.WriteLine("\t.section .rodata");
            sw.WriteLine("\t.global " + songName);
            sw.WriteLine("\t.align 2\n");
            sw.WriteLine();

            // Do tracks
            for (int i = 0; i < song.Tracks.Length; i++)
            {
                sw.WriteLine("@ **************** Track " + (i + 1) + " **************** @");
                sw.WriteLine();

                br.BaseStream.Seek(song.Tracks[i], SeekOrigin.Begin);
                DisassembleM4ATrack(songName + "_" + (i + 1), sw, br);

                sw.WriteLine();
            }

            // Write Section 2: The Song Header
            sw.WriteLine("@******************************************************@");
            sw.WriteLine("\t.align 2");
            sw.WriteLine();
            sw.WriteLine(songName + ":");
            sw.WriteLine("\t.byte\t" + song.Tracks.Length);
            sw.WriteLine("\t.byte\t" + song.Unknown);
            sw.WriteLine("\t.byte\t" + songName + "_pri");
            sw.WriteLine("\t.byte\t" + songName + "_rev");
            sw.WriteLine();
            sw.WriteLine("\t.word\t" + songName + "_grp");
            sw.WriteLine();
            for (int i = 1; i <= song.Tracks.Length; i++)
            {
                sw.WriteLine("\t.word\t" + songName + "_" + i);
            }
            sw.WriteLine();
            sw.WriteLine("\t.end");

            sw.Dispose();
            br.Dispose();
        }

        private static void DisassembleM4ATrack(string trackName, StreamWriter sw, BinaryReader br)
        {
            // Do it
            List<Command> commands = new List<Command>();
            List<Command> jumpCmds = new List<Command>();
            uint startOffset = (uint)br.BaseStream.Position;

            #region Read Commands
            bool exit = false;
            while (!exit)
            {
                Command cmd = new Command();
                cmd.absoulteOffset = (uint)br.BaseStream.Position;
                cmd.relativeOffset = cmd.absoulteOffset - startOffset;
                cmd.parameters = null;
                cmd.insertLabel = false;

                cmd.value = br.ReadByte();
                if (cmd.value < 128)
                {
                    cmd.isRepeat = true;
                    if (PeekByte(br) < 128)
                    {
                        cmd.parameters = new uint[2];
                        cmd.parameters[0] = cmd.value;
                        cmd.parameters[1] = br.ReadByte();
                    }
                    else
                    {
                        cmd.parameters = new uint[1];
                        cmd.parameters[0] = cmd.value;
                    }
                }
                else
                {
                    cmd.isRepeat = false;

                    switch (cmd.value)
                    {
                        #region W00-W96
                        case 128:
                        case 129:
                        case 130:
                        case 131:
                        case 132:
                        case 133:
                        case 134:
                        case 135:
                        case 136:
                        case 137:
                        case 138:
                        case 139:
                        case 140:
                        case 141:
                        case 142:
                        case 143:
                        case 144:
                        case 145:
                        case 146:
                        case 147:
                        case 148:
                        case 149:
                        case 150:
                        case 151:
                        case 152:
                        case 153:
                        case 154:
                        case 155:
                        case 156:
                        case 157:
                        case 158:
                        case 159:
                        case 160:
                        case 161:
                        case 162:
                        case 163:
                        case 164:
                        case 165:
                        case 166:
                        case 167:
                        case 168:
                        case 169:
                        case 170:
                        case 171:
                        case 172:
                        case 173:
                        case 174:
                        case 175:
                        case 176:
                            {
                                /* byte[] peek = PeekBytes(br, 3);
                                if (peek[0] < 128)
                                {
                                    if (peek[1] < 128)
                                    {
                                        if (peek[2] < 128)
                                        {
                                            cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte(), br.ReadByte() };
                                        }
                                        else
                                        {
                                            cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte() };
                                        }
                                    }
                                    else
                                    {
                                        cmd.parameters = new uint[] { br.ReadByte() };
                                    }
                                } */
                            }
                            break;
                        #endregion

                        case 0xB1:
                            exit = true;
                            break;
                        case 0xB2:
                            {
                                cmd.parameters = new uint[] { br.ReadUInt32() - 0x8000000 };
                                //if (cmd.parameters[0] < 0x8000000) throw new Exception("Check 0x" + br.BaseStream.Position.ToString("X"));
                                jumpCmds.Add(cmd);
                                //exit = true;
                            }
                            break;
                        case 0xB3:
                            {
                                cmd.parameters = new uint[] { br.ReadUInt32() - 0x8000000 };
                                //if (cmd.parameters[0] < 0x8000000) throw new Exception("Check 0x" + br.BaseStream.Position.ToString("X"));
                                jumpCmds.Add(cmd);
                            }
                            break;
                        case 0xB4:
                        case 0xB5: break;

                        case 0xB9:
                        case 0xBA:
                        case 0xBB:
                        case 0xBC:
                        case 0xBD:
                        case 0xBE:
                        case 0xBF:
                        case 0xC0:
                        case 0xC1:
                        case 0xC2:
                        case 0xC3:
                        case 0xC4:
                        case 0xC5:
                            {
                                cmd.parameters = new uint[] { br.ReadByte() };
                            }
                            break;

                        case 0xC8:
                            {
                                cmd.parameters = new uint[] { br.ReadByte() };
                            }
                            break;

                        case 0xCD: // XCMD -- does something...
                            {
                                cmd.parameters = new uint[] { br.ReadUInt32() }; // -- pointer, leave intact
                            }
                            break;

                        case 0xCE:
                        case 0xCF: // Variable parameters
                            {
                                byte[] peek = PeekBytes(br, 2);
                                if (peek[0] < 128)
                                {
                                    if (peek[1] < 128)
                                    {
                                        cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte() };
                                    }
                                    else
                                    {
                                        cmd.parameters = new uint[] { br.ReadByte() };
                                    }
                                }
                            }
                            break;

                        #region NOTE
                        case 208:
                        case 209:
                        case 210:
                        case 211:
                        case 212:
                        case 213:
                        case 214:
                        case 215:
                        case 216:
                        case 217:
                        case 218:
                        case 219:
                        case 220:
                        case 221:
                        case 222:
                        case 223:
                        case 224:
                        case 225:
                        case 226:
                        case 227:
                        case 228:
                        case 229:
                        case 230:
                        case 231:
                        case 232:
                        case 233:
                        case 234:
                        case 235:
                        case 236:
                        case 237:
                        case 238:
                        case 239:
                        case 240:
                        case 241:
                        case 242:
                        case 243:
                        case 244:
                        case 245:
                        case 246:
                        case 247:
                        case 248:
                        case 249:
                        case 250:
                        case 251:
                        case 252:
                        case 253:
                        case 254:
                        case 255:
                            {
                                byte[] peek = PeekBytes(br, 3);
                                if (peek[0] < 128)
                                {
                                    if (peek[1] < 128)
                                    {
                                        if (peek[2] < 128)
                                        {
                                            cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte(), br.ReadByte() };
                                        }
                                        else
                                        {
                                            cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte() };
                                        }
                                    }
                                    else
                                    {
                                        cmd.parameters = new uint[] { br.ReadByte() };
                                    }
                                }
                            }
                            break;

                        #endregion

                        default: //AddByte(val); relative++; break;
                            //break; // nothing for now...
                        //throw new Exception("Unknown Command 0x" + val.ToString("X2"));
                            {
                                // Stop stuff from working in memory.
                                sw.WriteLine("\tERROR: Cannot decompile command 0x" + cmd.value.ToString("X"));
                                br.Dispose();
                                sw.Dispose();
                                throw new Exception("Unknown command 0x" + cmd.value.ToString("X") + "!");
                            }
                    }
                }

                commands.Add(cmd);
            }

            // Insert labels
            if (jumpCmds.Count > 0)
            {
                foreach (Command cmd in jumpCmds)
                {
                    uint jumpOff = cmd.parameters[0];

                    for (int i = 0; i < commands.Count; i++)
                    {
                        if (commands[i].absoulteOffset == jumpOff)
                        {
                            commands[i].insertLabel = true;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region Disassemble Commands

            Dictionary<uint, string> labels = new Dictionary<uint, string>();
            labels.Add(startOffset, trackName);
            sw.WriteLine(trackName + ":");

            int labelNo = 1;
            for (int i = 0; i < commands.Count; i++)
            {
                Command cmd = commands[i];
                if (cmd.insertLabel)
                {
                    string lbl = trackName + "_" + labelNo;
                    labels.Add(cmd.absoulteOffset, lbl);

                    sw.WriteLine(lbl + ":");
                    labelNo++;
                }

                if (cmd.isRepeat)
                {
                    #region Repeat Command
                    if (cmd.parameters.Length == 1)
                    {
                        NoteKeys key = (NoteKeys)cmd.parameters[0];
                        sw.WriteLine("\t.byte\t\t" + key);
                    }
                    else if (cmd.parameters.Length == 2)
                    {
                        NoteKeys key = (NoteKeys)cmd.parameters[0];
                        NoteVelocity vel = (NoteVelocity)cmd.parameters[1];
                        sw.WriteLine("\t.byte\t\t" + key.ToString() + ", " + vel.ToString());
                    }
                    else // This shouldn't be called, but who knows.
                    {
                        for (int u = 0; u < cmd.parameters.Length; u++)
                        {
                            sw.WriteLine("\t.byte\t" + cmd.parameters[u]);
                        }
                    }
                    #endregion
                }
                //else if (cmd.value == 128)
                //{
                //    sw.WriteLine("\t.byte\t\t");
                //}
                else
                {
                    MPlayDef def = (MPlayDef)cmd.value;
                    if (cmd.parameters != null && cmd.parameters.Length > 0)
                    {
                        if (def == MPlayDef.GOTO ||
                            def == MPlayDef.PATT) // Jump commands
                        {
                            sw.WriteLine("\t.byte\t" + def.ToString());
                            if (labels.ContainsKey(cmd.parameters[0]))
                            {
                                sw.WriteLine("\t.word\t" + labels[cmd.parameters[0]]);
                            }
                            else
                            {
                                br.Dispose();
                                sw.WriteLine("- ERROR -");
                                sw.Dispose();
                                throw new Exception("Track did not have a label that can jump to 0x" +
                                    cmd.parameters[0].ToString("X"));
                            }
                        }
                        else if (def == MPlayDef.VOICE) // TODO: For the specialty stuff, do I want to add checks? (No)
                        {
                            sw.WriteLine("\t.byte\t" + def.ToString() + ", " + cmd.parameters[0] + " @ " + instruments[cmd.parameters[0]]);
                        }
                        else if (def == MPlayDef.TEMPO)
                        {
                            sw.WriteLine("\t.byte\t" + def.ToString() + ", " + cmd.parameters[0] + " @ " + (cmd.parameters[0] * 2) + " bpm");
                        }
                        else if (def == MPlayDef.XCMD) // Call ASM?
                        {
                            sw.WriteLine("\t.byte\t" + def.ToString() + " @ This command is under research! Please contact itari about it.");
                            sw.WriteLine("\t.word\t0x" + cmd.parameters[0].ToString("X8"));
                        }
                        else if (def == MPlayDef.MEMACC)
                        {
                            MEMACCParameters mem = (MEMACCParameters)cmd.parameters[0];
                            sw.WriteLine("\t.byte\t" + def.ToString() + ", " + mem.ToString());
                        }
                        else if (def == MPlayDef.MODT)
                        {
                            MPlayDef def2 = MPlayDef.W00; // Special command parameters
                            switch(cmd.parameters[0])
                            {
                                case 0: def2 = MPlayDef.mod_vib; break;
                                case 1: def2 = MPlayDef.mod_tre; break;
                                case 2: def2 = MPlayDef.mod_pan; break;
                            }

                            sw.Write("\t.byte\t" + def.ToString() + ", ");
                            if (def2 != MPlayDef.W00) sw.WriteLine(def2.ToString());
                            else sw.WriteLine(cmd.parameters[0].ToString());
                        }
                        else // Normal commands
                        {
                            sw.Write("\t.byte\t" + def.ToString());
                            if (cmd.value <= 175 || cmd.value >= 208) // Notes or rests
                            {
                                if (cmd.parameters.Length == 1)
                                {
                                    NoteKeys key = (NoteKeys)cmd.parameters[0];
                                    sw.Write(", " + key.ToString());
                                }
                                else if (cmd.parameters.Length == 2)
                                {
                                    NoteKeys key = (NoteKeys)cmd.parameters[0];
                                    NoteVelocity vel = (NoteVelocity)cmd.parameters[1];

                                    sw.Write(", " + key.ToString() + ", " + vel.ToString());
                                }
                                else
                                {
                                    for (int ii = 0; ii < cmd.parameters.Length; ii++)
                                    {
                                        sw.Write(", " + cmd.parameters[ii]);
                                    }
                                }
                            }
                            else // Random other commands.
                            {
                                for (int ii = 0; ii < cmd.parameters.Length; ii++)
                                {
                                    sw.Write(", " + cmd.parameters[ii]);
                                }
                            }
                            sw.WriteLine();
                        }
                    }
                    else
                    {
                        sw.WriteLine("\t.byte\t" + def.ToString());
                    }
                }
            }

            #endregion

            // TODO: I need to clean up the text writing code
        }

        public static void DisassembleSample(string output, SampleEditorForm.Sample sample)
        {
            StreamWriter sw = File.CreateText(output);
            string global = Path.GetFileNameWithoutExtension(output).Replace(" ", "_");

            // Do ASM header
            sw.WriteLine("\t.section .rodata");
            sw.WriteLine("\t.global " + global);
            sw.WriteLine("\t.align 2");
            sw.WriteLine();

            // Write the sample header
            sw.WriteLine(global + ":");
            if (sample.Compressed) sw.WriteLine("\t.byte\t0x00, 0x00"); // Compression
            else sw.WriteLine("\t.byte\t0x00, 0x00"); // TODO:Add code to allow compression
            if (sample.Looped) sw.WriteLine("\t.byte\t0x00, 0x40"); // Looping
            else sw.WriteLine("\t.byte\t0x00, 0x00");
            sw.WriteLine("\t.word\t0x" + sample.Pitch.ToString("X"));
            sw.WriteLine("\t.word\t0x" + sample.LoopStart.ToString("X"));
            // TODO: Compression
            sw.WriteLine("\t.word\t0x" + sample.Data.Length.ToString("X"));
            sw.WriteLine();

            // Write PCM data, uncompressed
            // I should do something better, but this works
            int r = 0;
            foreach (sbyte level in sample.Data)
            {
                // Write it
                if (r == 0)
                {
                    sw.Write("\t.byte\t0x" + level.ToString("X2"));
                }
                else
                {
                    sw.Write(", 0x" + level.ToString("X2"));
                }

                r++;
                if (r == 8) // New Line
                {
                    r = 0;
                    sw.WriteLine();
                }
            }

            if (r != 0) sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("\t.end"); // Yay! All done.
            sw.Dispose();
        }

        private static byte PeekByte(BinaryReader br)
        {
            byte b = br.ReadByte();
            br.BaseStream.Seek(-1, SeekOrigin.Current);
            return b;
        }

        private static byte[] PeekBytes(BinaryReader br, int count)
        {
            byte[] b = br.ReadBytes(count);
            br.BaseStream.Seek(-count, SeekOrigin.Current);
            return b;
        }

        public static string GetCommandName(byte cmd)
        {
            if (cmd < 128) return "Play Previous Note";
            else if (cmd < 0xB1)
            {
                return "Rest";
            }
            else if (cmd > 0xCF)
            {
                return "Play Note";
            }
            else
                switch (cmd)
                {
                    case 0xB1: return "End of Track";
                    case 0xB2: return "Jump/Goto";
                    case 0xB3: return "Call";
                    case 0xB4: return "Return";
                    case 0xB5: return "Repeat?";

                    case 0xBA: return "Set Priority";
                    case 0xBB: return "Set Tempo";
                    case 0xBC: return "Key Shift/Transpose";
                    case 0xBD: return "Set Instrument";
                    case 0xBE: return "Set Volume";
                    case 0xBF: return "Pan";
                    case 0xC0: return "Pitch Bend";
                    case 0xC1: return "Set Pitch Bend Range";
                    case 0xC2: return "Set LFO Speed";
                    case 0xC3: return "LFO Delay";
                    case 0xC4: return "Set LFO Depth";
                    case 0xC5: return "Set LFO Type";

                    case 0xC8: return "Detune (Micro Tuning)";

                    case 0xCD: return "Extend?";
                    case 0xCE: return "End of Tie";
                    case 0xCF: return "Start of Tie";
                    default: return "???";
                }
        }

        // AST-ish stuff
        /// <summary>
        /// Represents a command in a song to disassemble.
        /// </summary>
        private class Command
        {
            public uint absoulteOffset, relativeOffset;

            public byte value;
            public uint[] parameters;

            public bool insertLabel;
            public bool isRepeat; // Is a repetition command. (Only parameters, no value)
        }
    }
}
