using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ASong
{
    /// <summary>
    /// Holds all the information for a ROM. Not very good. ;)
    /// </summary>
    public class GameInfo
    {
        private string path;
        private string code;
        private uint romTable;
        private Dictionary<uint, string> songTable;
        private int songCount;

        public GameInfo()
        {
            path = string.Empty;
            code = string.Empty;
            romTable = 0;
            songTable = new Dictionary<uint, string>();
            songCount = 0;
        }

        public bool Load(string romCode)
        {
            string file = Path.Combine(Environment.CurrentDirectory, "ROMs", romCode);

            if (File.Exists(file))
            {
                path = file;
                code = romCode;

                // Load it
                BinaryReader br = new BinaryReader(File.OpenRead(path));

                romTable = br.ReadUInt32();

                songTable.Clear();
                uint tableLen = br.ReadUInt32();
                for (uint i = 0; i < tableLen; i++)
                {
                    uint id = br.ReadUInt32();
                    string name = br.ReadString();
                    if (!songTable.ContainsKey(id))
                    {
                        songTable.Add(id, name);
                    }
                }

                br.Dispose();
                return true;
            }
            else
            {
                path = string.Empty;
                code = string.Empty;
                romTable = 0;
                songTable.Clear();
                songCount = 0;
                return false;
            }
        }

        public void Save()
        {
            // Return if nothing loaded to save.
            if (path == string.Empty) return;

            // Yeah.
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(path));
            
            // Song Table Offset
            bw.Write(romTable);

            // Write Song Table
            if (songTable.Keys.Count > 0)
            {
                bw.Write((uint)songTable.Keys.Count);
                foreach (uint id in songTable.Keys)
                {
                    bw.Write(id);
                    bw.Write(songTable[id]);
                }
            }
            else
            {
                bw.Write((uint)0);
            }

            // Done~
            bw.Dispose();
        }

        // Stuff
        public string GetSongName(uint id)
        {
            if (songTable.ContainsKey(id))
            {
                return songTable[id];
            }
            else
            {
                return string.Empty;
            }
        }

        public void SetSongName(uint id, string name)
        {
            if (songTable.ContainsKey(id))
            {
                songTable[id] = name;
            }
            else
            {
                songTable.Add(id, name);
            }
        }

        public void RemoveSongName(uint id)
        {
            if (songTable.ContainsKey(id))
            {
                songTable.Remove(id);
            }
        }

        public uint GetSongID(string name)
        {
            // Search all saved songs to see if we have a match.
            foreach(uint id in songTable.Keys)
            {
                if (songTable[id] == name)
                {
                    return id;
                }
            }
            return 0; // For now, we just return this as a failure.
        }

        public uint SongTableOffset
        {
            get { return romTable; }
            set { romTable = value; }
        }

        public string ROMCode
        {
            get { return code; }
            set { code = value; } // -- no need for this, right now.
        }

        public string FilePath
        {
            get { return path; }
            set { path = value; }
        }

        public int SongCount
        {
            get { return songCount; }
            set { songCount = value; }
        }
    }
}