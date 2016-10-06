using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace ASong
{
    public class LineAssembler
    {
        public static Dictionary<string, uint> definitions = new Dictionary<string, uint>();
        public static List<Section> sections = new List<Section>();
        public static string global = "";

        public static int currentLine = 0;

        public static void Assemble(string file, string rom, uint writeTracksTo, uint writeHeaderTo, uint voicegroup)
        {
            BinaryWriter bw = null;
            try
            {
                // Parse
                Parse(file);

                if (sections.Count != 2)
                {
                    throw new Exception("Number of sections (.align) in file is incorrect!");
                }

                // Burn to ROM
                bw = new BinaryWriter(File.OpenWrite(rom));

                // write tracks
                bw.BaseStream.Seek(writeTracksTo, SeekOrigin.Begin);
                Dictionary<string, uint> labelOffsets = new Dictionary<string, uint>();
                foreach (Label label in sections[0].labels)
                {
                    // store pointer locations
                    labelOffsets.Add(label.name, (uint)bw.BaseStream.Position);

                    // write data
                    foreach (Line line in label.lines)
                    {
                        if (line is Pointer)
                        {
                            Pointer ptr = (Pointer)line;

                            // get offset
                            uint offset = ptr.offset;
                            if (ptr.label != string.Empty) // label to offset
                            {
                                offset = labelOffsets[ptr.label];
                            }
                            /*else
                            {
                                throw new Exception("Label '" + ptr.label + "' was never set to an actual offset!");
                            }*/

                            // write
                            bw.Write((uint)(offset + 0x08000000));
                        }
                        else if (line is Command)
                        {
                            // a simple raw value command
                            Command cmd = (Command)line;
                            bw.Write(cmd.value);
                        }
                    }
                }

                // Write song header
                bw.BaseStream.Seek(writeHeaderTo, SeekOrigin.Begin);
                foreach (Line line in sections[1].labels[0].lines)
                {
                    if (line is Pointer)
                    {
                        Pointer ptr = (Pointer)line;

                        // get offset
                        uint offset = ptr.offset;
                        if (ptr.label != string.Empty) // label to offset
                        {
                            if (ptr.label.EndsWith("_grp"))
                            {
                                offset = voicegroup;
                            }
                            else
                            {
                                offset = labelOffsets[ptr.label];
                            }
                        }

                        // write
                        bw.Write((uint)(offset + 0x08000000));
                    }
                    else if (line is Command)
                    {
                        // a simple raw value command
                        Command cmd = (Command)line;
                        bw.Write(cmd.value);
                    }
                }

                bw.Dispose();
            }
            catch (Exception ex)
            {
                if (bw != null) bw.Dispose();
                throw new Exception("Line " + currentLine + ": " + ex.Message, ex.InnerException);
            }
        }

        #region Parsing

        private static void Parse(string file)
        {
            StreamReader sr = File.OpenText(file);

            // reset
            definitions.Clear();
            sections.Clear();
            global = "";

            // parse
            Section section = null;
            Label label = null;
            currentLine = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                currentLine++;

                // comment
                if (line.Contains('@'))
                {
                    int index = line.IndexOf('@');
                    line = line.Remove(index).Trim();
                }
                // blank lines
                if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;

                // parse commands
                if (line.StartsWith(".include")) continue;
                else if (line.StartsWith(".section")) continue;
                else if (line.StartsWith(".equ")) // definition
                {
                    #region .equ
                    line = line.Substring(4).Replace("\t", "").Replace(" ", ""); // remove .equ

                    // split line
                    string[] parts = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2) throw new Exception("Incorrect .equ format!");

                    // parse it
                    if (parts[1] == "voicegroup000") parts[1] = "0"; // voicegroup is set by user

                    if (parts[1].StartsWith("0x"))
                    {
                        definitions.Add(parts[0], Convert.ToUInt32(parts[1], 16));
                    }
                    else
                    {
                        definitions.Add(parts[0], Convert.ToUInt32(parts[1]));
                    }
                    #endregion
                }
                else if (line.StartsWith(".global"))
                {
                    line = line.Substring(7).TrimStart();
                    global = line;
                }
                else if (line.StartsWith(".align")) // section separator
                {
                    if (section != null) // reset for new section
                    {
                        if (label != null)
                        {
                            section.labels.Add(label);
                        }

                        sections.Add(section);
                        section = null;
                    }

                    // start
                    section = new Section();
                    label = null;
                }
                else if (line.StartsWith(".end")) // only encountered once, at the end
                {
                    section.labels.Add(label); // add final label and quit
                    sections.Add(section);
                    break;
                }
                else if (line.Contains(':')) // label separator
                {
                    if (label != null)
                    {
                        section.labels.Add(label);
                        label = null;
                    }

                    line = line.Replace(":", "");
                    label = new Label(line);
                }
                else if (line.StartsWith(".word")) // so far, only used for pointers
                {
                    #region .word
                    line = line.Substring(5).Replace(" ", "").Replace("\t", "");

                    if (line.StartsWith("0x"))
                    {
                        uint offset = Convert.ToUInt32(line, 16);
                        label.lines.Add(new Pointer(offset));
                    }
                    else if (definitions.ContainsKey(line))
                    {
                        // definition, like as is used for voice groups!
                        label.lines.Add(new Pointer(line));
                    }
                    else
                    {
                        // all this crap is super messy and I don't like it.
                        // not one bit!
                        // oh well, it's better than before.
                        bool exists = false; // determine if the label is valid
                        // check older labels
                        #region Sections Check
                        if (sections.Count > 0)
                        {
                            // other sections
                            for (int i = 0; i < sections.Count; i++)
                            {
                                foreach (Label lbl in sections[i].labels)
                                {
                                    if (lbl.name == line)
                                    {
                                        exists = true;
                                        break;
                                    }
                                }
                            }

                            // current section
                            foreach (Label lbl in section.labels)
                            {
                                if (lbl.name == line)
                                {
                                    exists = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (Label lbl in section.labels)
                            {
                                if (lbl.name == line)
                                {
                                    exists = true;
                                    break;
                                }
                            }
                        }
                        #endregion

                        // check current label
                        if (label.name == line)
                        {
                            exists = true;
                        }

                        // if it exists, add reference
                        if (exists)
                        {
                            label.lines.Add(new Pointer(line));
                        }
                        else
                        {
                            throw new Exception("Unknown label '" + line + "' referenced!");
                        }
                    }
                    #endregion
                }
                else if (line.StartsWith(".byte"))
                {
                    // This is one of the biggest messes I have ever had the displeasure of writing.
                    // Please forgive me.
                    // I'll write a better version once I know it works.
                    #region .byte
                    line = line.Substring(5).Replace(" ", "").Replace("\t", "");

                    if (line.Contains(','))
                    {
                        #region Multiple Bytes

                        string[] parts = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        string part = "";
                        for (int i = 0; i < parts.Length; i++)
                        {
                            part = parts[i];

                            if (part.Contains('+') || part.Contains('-') // Operators...
                                || part.Contains('*') || part.Contains('/'))
                            {
                                string[] ops;
                                string[] vals = SplitMathPhrase(part, out ops);

                                #region Math Performing

                                byte val;
                                if (!TryParseByte(vals[0], out val))
                                {
                                    throw new Exception("Do not know how to parse \"" + part + "\"!");
                                }

                                uint value = val;
                                for (int k = 0; k < ops.Length; k++)
                                {
                                    string op = ops[k];

                                    byte next;
                                    if (!TryParseByte(vals[k + 1], out next))
                                    {
                                        throw new Exception("Do not know how to parse \"" + part + "\"!");
                                    }

                                    switch (op)
                                    {
                                        case "+":
                                            value += next;
                                            break;
                                        case "-":
                                            value -= next;
                                            break;
                                        case "*":
                                            value *= next;
                                            break;
                                        case "/":
                                            value /= next;
                                            break;

                                        default:
                                            throw new Exception("Do not know how to parse \"" + part + "\"!");
                                    }
                                }

                                #endregion

                                label.lines.Add(new Command((byte)value));
                            }
                            else
                            {
                                byte val;
                                if (TryParseByte(part, out val))
                                {
                                    label.lines.Add(new Command(val));
                                }
                                else
                                {
                                    throw new Exception("Do not know how to parse \"" + part + "\"!");
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        // I've only seen MPlayDef values on a single .byte
                        // Oh well.
                        if (line.Contains('+') || line.Contains('-') // Operators...
                                || line.Contains('*') || line.Contains('/'))
                        {
                            string[] ops;
                            string[] vals = SplitMathPhrase(line, out ops);

                            #region Math Performing

                            byte val;
                            if (!TryParseByte(vals[0], out val))
                            {
                                throw new Exception("Do not know how to parse \"" + line + "\"!");
                            }

                            uint value = val;
                            for (int k = 0; k < ops.Length; k++)
                            {
                                string op = ops[k];

                                byte next;
                                if (!TryParseByte(vals[k + 1], out next))
                                {
                                    throw new Exception("Do not know how to parse \"" + line + "\"!");
                                }

                                switch (op)
                                {
                                    case "+":
                                        value += next;
                                        break;
                                    case "-":
                                        value -= next;
                                        break;
                                    case "*":
                                        value *= next;
                                        break;
                                    case "/":
                                        value /= next;
                                        break;

                                    default:
                                        throw new Exception("Do not know how to parse \"" + line + "\"!");
                                }
                            }

                            #endregion

                            label.lines.Add(new Command((byte)value));
                        }
                        else
                        {
                            byte val;
                            if (TryParseByte(line, out val))
                            {
                                label.lines.Add(new Command(val));
                            }
                            else
                            {
                                throw new Exception("Do not know how to parse \"" + line + "\"!");
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    throw new Exception("Unable to assemble '" + line + "'!");
                }

            }

            sr.Dispose();
        }

        private static bool TryParseByte(string line, out byte value)
        {
            MPlayDef def;
            byte b;

            if (Enum.TryParse<MPlayDef>(line, out def))
            {
                value = (byte)def;
            }
            else if (byte.TryParse(line, out b))
            {
                value = b;
            }
            else if (definitions.ContainsKey(line))
            {
                value = (byte)definitions[line];
            }
            else
            {
                value = 0;
                return false;
            }
            return true;
        }

        private static string[] SplitMathPhrase(string line, out string[] operators)
        {
            List<string> parts = new List<string>();
            List<string> ops = new List<string>();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    parts.Add(sb.ToString());
                    ops.Add(c.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            parts.Add(sb.ToString());
            operators = ops.ToArray();

            return parts.ToArray();
        }

        #endregion

        #region AST
        /// <summary>
        /// Represents a portion of the assembly separated by a .align.
        /// </summary>
        public class Section
        {
            public List<Label> labels;

            public Section()
            {
                labels = new List<Label>();
            }
        }

        /// <summary>
        /// Represents a subsection of the assembly under a label.
        /// </summary>
        public class Label
        {
            public string name;
            public List<Line> lines;

            public Label(string name)
            {
                this.name = name;
                lines = new List<Line>();
            }

            public int GetLength()
            {
                if (lines.Count == 0) return 0;

                int s = 0;
                foreach (Line line in lines)
                {
                    s += line.GetLength();
                }
                return s;
            }
        }

        /// <summary>
        /// Base class for the various lines you can encounter in the assembly.
        /// </summary>
        public abstract class Line
        {
            public abstract int GetLength();
        }

        /// <summary>
        /// Represents a .word line.
        /// </summary>
        public class Pointer : Line
        {
            public string label;
            public uint offset;

            public Pointer(uint offset)
            {
                this.offset = offset;
                label = string.Empty;
            }

            public Pointer(string label)
            {
                this.label = label;
                offset = 0;
            }

            public override int GetLength()
            {
                return 4;
            }
        }

        /// <summary>
        /// Represents a .byte line.
        /// </summary>
        public class Command : Line
        {
            public byte value;

            public Command(byte value)
            {
                this.value = value;
            }

            public override int GetLength()
            {
                return 1;
            }
        }

        #endregion
    }
}
