using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ASong
{
    public partial class TrackEditorForm : Form
    {
        private string rom;
        private uint trackOffset;
        private List<Command> trackData;
        private int originalTrackSize;

        public class TrackSavedEventArgs
        {
            public bool Repointed;
            public uint OldOffset, NewOffset;

            public TrackSavedEventArgs()
            {
                Repointed = false;
                OldOffset = 0;
                NewOffset = 0;
            }
        }

        public delegate void TrackSavedEventHandler(TrackSavedEventArgs tse);
        public event TrackSavedEventHandler TrackSaved;

        public TrackEditorForm(string romPath, uint offset)
        {
            InitializeComponent();
            trackOffset = offset;
            trackData = new List<Command>();
            rom = romPath;
            originalTrackSize = 0;
        }

        private void TrackEditorForm_Load(object sender, EventArgs e)
        {
            LoadTrack();
            originalTrackSize = GetTrackSize(); // Will be used for saving (check for repoint needed!)

            UpdateTrackListView();
        }

        private void TrackEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void bSave_Click(object sender, EventArgs e)
        {
            uint originalTrackOffset = trackOffset;
            if (!SaveTrack()) return; // Try to save.

            if (trackOffset != originalTrackOffset) // Change name. We will lose some of the descr. but oh well.
            {
                Text = "Edit Track @ 0x" + trackOffset.ToString("X");
            }

            // Invoke, if save succeeded
            if (TrackSaved != null)
            {
                //  Format event args
                var t = new TrackSavedEventArgs();
                if (trackOffset == originalTrackOffset)
                {
                    t.Repointed = false;
                }
                else
                {
                    t.Repointed = true;
                    t.OldOffset = originalTrackOffset;
                    t.NewOffset = trackOffset;
                }

                // Do it.
                TrackSaved.Invoke(t);
            }
        }

        private void LoadTrack()
        {
            // Read the data
            BinaryReader br = new BinaryReader(File.OpenRead(rom));
            br.BaseStream.Seek(trackOffset, SeekOrigin.Begin);

            List<Command> commands = new List<Command>();
            bool exit = false;
            while (!exit && br.BaseStream.Position < br.BaseStream.Length)
            {
                byte cmd = br.ReadByte();
                Command command = null;

                #region Read Command
                //var listItem = new ListViewItem();
                //listItem.Text = Disassembler.GetCommandName(cmd);
                if (cmd < 0x80)
                {
                    Repeat repeat = new Repeat();
                    repeat.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                    //listItem.SubItems.Add("--");
                    //listItem.BackColor = Color.ForestGreen;
                    if (PeekByte(br) < 0x80)
                    {
                        //listItem.SubItems.Add(((NoteKeys)cmd).ToString() +
                        //            ", " + ((NoteVelocity)br.ReadByte()).ToString());
                        repeat.Arguments.Add(cmd);
                        repeat.Arguments.Add(br.ReadByte());
                    }
                    else
                    {
                        //listItem.SubItems.Add(((NoteKeys)cmd).ToString());
                        repeat.Arguments.Add(cmd);
                    }
                    command = repeat;
                }
                else if (cmd > 0xCF)
                {
                    //listItem.SubItems.Add(cmd.ToString("X2"));
                    //listItem.BackColor = Color.ForestGreen;
                    Generic g = new Generic(cmd);
                    g.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                    byte[] peek = PeekBytes(br, 3);
                    if (peek[0] < 0x80)
                    {
                        if (peek[1] < 0x80)
                        {
                            if (peek[2] < 0x80) // Not sure if there even is a third.
                            {
                                //cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte(), br.ReadByte() };
                                //listItem.SubItems.Add(br.ReadByte().ToString("X2") +
                                //    ", " + br.ReadByte().ToString("X2") +
                                //    ", " + br.ReadByte().ToString("X2"));
                                g.Arguments.Add(br.ReadByte());
                                g.Arguments.Add(br.ReadByte());
                                g.Arguments.Add(br.ReadByte());
                            }
                            else
                            {
                                //cmd.parameters = new uint[] { br.ReadByte(), br.ReadByte() };
                                //listItem.SubItems.Add(((NoteKeys)br.ReadByte()).ToString() +
                                //    ", " + ((NoteVelocity)br.ReadByte()).ToString());
                                g.Arguments.Add(br.ReadByte());
                                g.Arguments.Add(br.ReadByte());
                            }
                        }
                        else
                        {
                            //cmd.parameters = new uint[] { br.ReadByte() };
                            //listItem.SubItems.Add(((NoteKeys)br.ReadByte()).ToString());
                            g.Arguments.Add(br.ReadByte());
                        }
                    }
                    command = g;
                }
                else
                {
                    //listItem.SubItems.Add(cmd.ToString("X2"));
                    //listItem.BackColor = (cmd < 0xB1 ? Color.ForestGreen : Color.Orange);
                    switch (cmd)
                    {
                        case 0xB1:
                            command = new Command(cmd);
                            command.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                            exit = true;
                            break;

                        case 0xB2:
                        case 0xB3:
                            {
                                //exit = true;
                                //listItem.SubItems.Add((br.ReadUInt32() - 0x8000000).ToString("X"));
                                //listItem.BackColor = Color.Red;
                                Jump j = new Jump(cmd);
                                j.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                                j.Pointer = br.ReadUInt32() - 0x08000000;
                                j.RelativePointer = j.Pointer - trackOffset;
                                command = j;
                            }
                            break;

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
                            //listItem.SubItems.Add(br.ReadByte().ToString("X2"));
                            {
                                Generic g = new Generic(cmd);
                                g.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                                g.Arguments.Add(br.ReadByte());
                                command = g;
                            }
                            break;

                        case 0xC8:
                            //listItem.SubItems.Add(br.ReadByte().ToString("X2"));
                            {
                                Generic g = new Generic(cmd);
                                g.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                                g.Arguments.Add(br.ReadByte());
                                command = g;
                            }
                            break;

                        case 0xCD:
                            //listItem.SubItems.Add(br.ReadUInt32().ToString("X"));
                            //listItem.SubItems.Add("Help! itari needs info. on this command!");
                            {
                                command = new xCommand(cmd, br.ReadUInt32());
                                command.RelativeOffset = (uint)br.BaseStream.Position - 4 - trackOffset;
                            }
                            break;
                        case 0xCE:
                        case 0xCF: // Variable parameters (Tie, End Tie)
                            {
                                Generic g = new Generic(cmd);
                                g.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                                byte[] peek = PeekBytes(br, 2);
                                if (peek[0] < 0x80)
                                {
                                    if (peek[1] < 0x80)
                                    {
                                        //listItem.SubItems.Add(((NoteKeys)br.ReadByte()).ToString() +
                                        //    ", " + ((NoteVelocity)br.ReadByte()).ToString());
                                        g.Arguments.Add(br.ReadByte());
                                        g.Arguments.Add(br.ReadByte());
                                    }
                                    else
                                    {
                                        //listItem.SubItems.Add(((NoteKeys)br.ReadByte()).ToString());
                                        g.Arguments.Add(br.ReadByte());
                                    }
                                }
                                command = g;
                            }
                            break;

                        default:
                            {
                                command = new Command(cmd);
                                command.RelativeOffset = (uint)br.BaseStream.Position - trackOffset;
                            }
                            break;
                    }
                }

                //listView1.Items.Add(listItem);
                #endregion

                // This way, we can have errors and not crash.
                if (command != null)
                {
                    commands.Add(command);
                }
            }

            br.Dispose();

            trackData.Clear();
            trackData.AddRange(commands);
        }

        // Calculates size of track data in bytes.
        private int GetTrackSize()
        {
            int size = 0;
            foreach (Command cmd in trackData)
            {
                // A bunch of annoying stuff.
                if (cmd is Repeat) size += ((Repeat)cmd).GetSize();
                else if (cmd is Generic) size += ((Generic)cmd).GetSize();
                else if (cmd is Jump) size += ((Jump)cmd).GetSize();
                else if (cmd is xCommand) size += ((xCommand)cmd).GetSize();
                else size += cmd.GetSize();
            }
            return size;
        }

        private bool SaveTrack()
        {
            if (trackData.Count == 0) return false;

            // Open ROM for writing
            GBABinaryWriter bw = new GBABinaryWriter(File.OpenWrite(rom));
            bw.BaseStream.Seek(trackOffset, SeekOrigin.Begin);

            // Overwrite track with FF, incase we repoint or something silly like that.
            for (int i = 0; i < originalTrackSize; i++)
            {
                bw.Write((byte)0xFF);
            }

            // Find FreeSpace if necessary
            if (GetTrackSize() > originalTrackSize) // Needs to repoint
            {
                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)GetTrackSize());
                if (fsf.ShowDialog() != DialogResult.OK) return false; // Grr.

                // Change track offset. The main form will repoint it
                trackOffset = fsf.FreeSpaceOffset;

                // TODO: Overwrite old track
            }

            // Write the track
            bw.BaseStream.Seek(trackOffset, SeekOrigin.Begin);
            foreach (Command cmd in trackData)
            {
                // Write the appropriate stuff.
                if (cmd is Repeat)
                {
                    // Just write arguments.
                    Repeat rep = (Repeat)cmd;
                    for (int i = 0; i < rep.Arguments.Count; i++)
                    {
                        bw.Write(rep.Arguments[i]);
                    }
                }
                else if (cmd is Jump)
                {
                    // Write command and pointer (we calculate!)
                    Jump jmp = (Jump)cmd;
                    bw.Write(jmp.Value);
                    bw.WritePointer((uint)(jmp.RelativePointer + trackOffset));
                }
                else if (cmd is Generic)
                {
                    Generic gen = (Generic)cmd;
                    bw.Write(gen.Value);
                    for (int i = 0; i < gen.Arguments.Count; i++)
                    {
                        bw.Write(gen.Arguments[i]);
                    }
                }
                else if (cmd is xCommand) // Command 0xCD -- todo: is actually two uint16's
                {
                    xCommand x = (xCommand)cmd;
                    bw.Write(x.Value);
                    bw.Write(x.xValue);
                }
                else
                {
                    bw.Write(cmd.Value);
                }
            }

            bw.Dispose();
            return true;
        }

        private void UpdateTrackListView()
        {
            // Let's do it.
            listEvents.Items.Clear();
            foreach(Command cmd in trackData)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Disassembler.GetCommandName(cmd.Value);
                item.SubItems.Add(cmd.Value.ToString("X2"));

                // Show command args, and color.
                if (cmd is Repeat)
                {
                    Repeat r = (Repeat)cmd;
                    item.SubItems[1].Text = "--";
                    item.BackColor = Color.MediumSeaGreen;

                    if (r.Arguments.Count == 1)
                    {
                        item.SubItems.Add(((NoteKeys)r.Arguments[0]).ToString());
                    }
                    else if (r.Arguments.Count == 2)
                    {
                        item.SubItems.Add(((NoteKeys)r.Arguments[0]).ToString() +
                            ", " + ((NoteVelocity)r.Arguments[1]).ToString());
                    }
                }
                else if (cmd is Jump)
                {
                    Jump jmp = (Jump)cmd;
                    item.BackColor = Color.LightPink;
                    item.SubItems.Add("@" + jmp.RelativePointer.ToString("X"));

                    // Color the correct jump-to destination (I will assume we always jump back)
                    int k = 0; // To get the listView index. :P
                    foreach(Command cmd2 in trackData)
                    {
                        if (cmd2.RelativeOffset == jmp.RelativePointer && k < listEvents.Items.Count)
                        {
                            // Color appropriately.
                            if (jmp.Value == (byte)MPlayDef.GOTO)
                                listEvents.Items[k].ForeColor = Color.Red;
                            else
                                listEvents.Items[k].ForeColor = Color.Purple;
                            // End
                            break;
                        }
                        k++;
                    }
                }
                else if (cmd is Generic)
                {
                    Generic gen = (Generic)cmd;
                    if (cmd.Value > 0xCF)
                    {
                        item.BackColor = Color.MediumSeaGreen;

                        if (gen.Arguments.Count == 1)
                        {
                            item.SubItems.Add(((NoteKeys)gen.Arguments[0]).ToString());
                        }
                        else if (gen.Arguments.Count == 2)
                        {
                            item.SubItems.Add(((NoteKeys)gen.Arguments[0]).ToString() +
                                ", " + ((NoteVelocity)gen.Arguments[1]).ToString());
                        }
                    }
                    else
                    {
                        item.BackColor = Color.LightSkyBlue;

                        if (gen.Arguments.Count > 0)
                        {
                            string args = "";
                            for (int i = 0; i < gen.Arguments.Count; i++)
                            {
                                if (i != 0) args += ", ";
                                args += gen.Arguments[i].ToString("X2");
                            }
                            item.SubItems.Add(args);
                        }
                    }
                }
                else if (cmd is xCommand)
                {
                    xCommand x = (xCommand)cmd;
                    item.BackColor = Color.PaleVioletRed;
                    item.SubItems.Add(x.xValue.ToString("X"));
                    item.SubItems.Add("This command needs more research!");
                }
                else if (cmd.Value == 0xB1)
                {
                    item.BackColor = Color.PaleVioletRed;
                }
                else if (cmd.Value == 0xB4)
                {
                    item.BackColor = Color.LightPink;
                }
                else
                {
                    item.BackColor = Color.MediumSeaGreen;
                }

                listEvents.Items.Add(item);
            }
        }

        private byte PeekByte(BinaryReader br)
        {
            byte b = br.ReadByte();
            br.BaseStream.Seek(-1, SeekOrigin.Current);
            return b;
        }

        private byte[] PeekBytes(BinaryReader br, int count)
        {
            byte[] b = br.ReadBytes(count);
            br.BaseStream.Seek(-count, SeekOrigin.Current);
            return b;
        }

        /*public bool NeedToReloadMainForm
        {
            get { return needToReloadMainForm; }
        }*/

        // The AST, as it were.
        #region Pseudo-AST
        public class Command
        {
            public byte Value;
            public uint RelativeOffset;

            public Command(byte value)
            {
                Value = value;
            }

            public int GetSize()
            {
                return 1;
            }
        }

        public class Jump : Command
        {
            public uint Pointer;
            public uint RelativePointer;

            public Jump(byte value)
                : base(value)
            {
                Pointer = 0;
                RelativePointer = 0;
            }

            public new int GetSize()
            {
                return 5;
            }
        }

        public class Repeat : Command
        {
            public List<byte> Arguments;

            public Repeat()
                : base(0) // For now, a place holder.
            {
                Arguments = new List<byte>();
            }

            public new int GetSize()
            {
                return Arguments.Count;
            }
        }

        public class Generic : Command
        {
            public List<byte> Arguments;

            public Generic(byte value)
                : base(value)
            {
                Arguments = new List<byte>();
            }

            public new int GetSize()
            {
                return 1 + Arguments.Count;
            }
        }

        public class xCommand : Command
        {
            public uint xValue;

            public xCommand(byte value, uint x)
                : base(value)
            {
                xValue = x;
            }

            public new int GetSize()
            {
                return 5;
            }
        }
        #endregion
    }
}
