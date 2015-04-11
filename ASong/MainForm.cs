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
    public struct SongHeader
    {
        // table entry
        public uint ID;
        public uint TableOffset;
        public byte TrackGroup;

        // header
        public uint HeaderOffset;
        public byte Unknown;
        public byte Priority, Reverb;
        public uint Voicegroup;
        public uint[] Tracks;
    }

    public partial class MainForm : Form
    {
        private enum OpenROMResult
        {
            Success, FileNotFound, NoM4A
        }

        private string romFilePath;
        private GameInfo gameInfo;
        private SongHeader song;

        private bool xx = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Create a ROMs folder
            if (!Directory.Exists("ROMs")) Directory.CreateDirectory("ROMs");
            gameInfo = new GameInfo();
            romFilePath = "";

            // Lock up
            LockControls();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Save current game's information.
            gameInfo.Save();
        }

        private void LockControls()
        {
            // Menu
            mnuEditSongNames.Enabled = false;
            //mnuSongs.Enabled = false;
            mnuAssemble.Enabled = false;
            mnuDisassemble.Enabled = false;
            mnuEditVoicegroup.Enabled = false;
            mnuWaveformEditor.Enabled = false;
            mnuSampleEditor.Enabled = false;
            mnuFSF.Enabled = false;

            // Interface
            cSong.Enabled = false;
            txtSong.Enabled = false;
            bSongMinus.Enabled = false;
            bSongPlus.Enabled = false;
            bEditTrack.Enabled = false;
        }

        private void UnlockControls()
        {
            // Menu
            mnuEditSongNames.Enabled = true;
            //mnuSongs.Enabled = true;
            mnuAssemble.Enabled = true;
            mnuDisassemble.Enabled = true;
            mnuEditVoicegroup.Enabled = true;
            mnuWaveformEditor.Enabled = true;
            mnuSampleEditor.Enabled = true;
            mnuFSF.Enabled = true;

            // Interfaec
            cSong.Enabled = true;
            txtSong.Enabled = true;
            bSongMinus.Enabled = true;
            bSongPlus.Enabled = true;
        }

        #region Menu

        private void mnuOpenROM_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Game Boy Advance ROMs|*.gba";
            openFileDialog.Title = "Open ROM";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                // Pressed cancel.
                if (gameInfo.ROMCode == string.Empty) // Keep old ROM open.
                {
                    LockControls();
                }
                return;
            }
            else
            {
                // Save old settings if something was already loaded.
                gameInfo.Save();

                // Then try to open a new ROM
                OpenROMResult result = OpenROM(openFileDialog.FileName);
                if (result == OpenROMResult.Success)
                {
                    UnlockControls();
                    ChangeSong();
                }
                else if (result == OpenROMResult.NoM4A)
                {
                    MessageBox.Show("Unable to load ROM!\nIt probably doesn't support the M4A Music Player.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LockControls();
                }
                else
                {
                    LockControls();
                }
            }
        }

        private void mnuEditSongNames_Click(object sender, EventArgs e)
        {
            SongListForm slf = new SongListForm(ref gameInfo);
            slf.ShowDialog();

            // Reset cSong
            xx = true; cSong.Items.Clear();
            for (uint i = 0; i < gameInfo.SongCount; i++)
            {
                string name = gameInfo.GetSongName(i);
                if (name != string.Empty)
                {
                    cSong.Items.Add(name);
                }
            }

            UpdateSongName();
        }

        private void mnuAssemble_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Assembly Files|*.s";
            openFileDialog.Title = "Assemble Song From";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                // TODO: Use assembler
                AssembleSongDialog asd = new AssembleSongDialog(openFileDialog.FileName, (song.Tracks.Length > 0 ? song.Tracks[0] : 0), song.HeaderOffset, song.Voicegroup);
                if (asd.ShowDialog() != DialogResult.OK) return;

                LineAssembler.Assemble(openFileDialog.FileName, romFilePath,
                    asd.TracksOffset, asd.HeaderOffset, asd.Voicegroup); // TODO: Customize

                MessageBox.Show("Song assembled successfully!", "Yay!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ChangeSong(); // Load new song header
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuDisassemble_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = "Assembly Files|*.s";
            saveFileDialog.Title = "Dissassemble Song To";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                Disassembler.DisassembleM4A(saveFileDialog.FileName, romFilePath, song);

                MessageBox.Show("Song disassembled successfully!", "Yay!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuEditVoicegroup_Click(object sender, EventArgs e)
        {
            if (song.ID >= gameInfo.SongCount) return;

            // Do it.
            VoicegroupEditorForm vef = new VoicegroupEditorForm(romFilePath, song.Voicegroup);
            vef.ShowDialog(); // Should this be a ShowDialog()?

            // Yeah.
        }

        private void mnuFSF_Click(object sender, EventArgs e)
        {
            if (romFilePath == "") return;

            FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(romFilePath);
            fsf.ShowDialog();
        }

        private void mnuWaveformEditor_Click(object sender, EventArgs e)
        {
            if (romFilePath == "") return;

            WaveformEditorForm wef = new WaveformEditorForm(romFilePath);
            wef.ShowDialog();
        }

        private void mnuSampleEditor_Click(object sender, EventArgs e)
        {
            if (romFilePath == "") return;

            SampleEditorForm sef = new SampleEditorForm(romFilePath);
            sef.ShowDialog();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (AboutDialog a = new AboutDialog()) a.ShowDialog();
        }

        #endregion

        #region ROM Stuff

        private OpenROMResult OpenROM(string file)
        {
            try
            {
                return uOpenROM(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("For whatever reason, the ROM couldn't be loaded.\n\n" +
                    "If it is open in another program, be sure to close it before trying again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return OpenROMResult.FileNotFound;
            }
        }

        private OpenROMResult uOpenROM(string file)
        {
            // Get a quick peek at the header
            //BinaryReader br = new BinaryReader(File.OpenRead(file));
            using (GBABinaryReader br = new GBABinaryReader(file))
            {
                br.BaseStream.Seek(0xACL, SeekOrigin.Begin);
                string romCode = Encoding.UTF8.GetString(br.ReadBytes(4));

                // Try to load the game's info, or create it~
                if (!gameInfo.Load(romCode))
                {
                    // If the game info load failed, we need to generate it.
                    // Step 1. Find the Song Table
                    uint table = FindSongTable(br);
                    if (table >= br.BaseStream.Length)
                    {
                        br.Dispose();
                        return OpenROMResult.NoM4A;
                    }
                    else
                    {
                        MessageBox.Show("Song Table found at 0x" + table.ToString("X"), "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gameInfo.SongTableOffset = table;
                    }

                    // Step 2. Set up the Names we need to know.
                    // Perhaps there is a way to load this from the internet? (I need a website of some kind)

                    // Step 3. Set ROM code
                    gameInfo.ROMCode = romCode;

                    // Step 4. Set the file path up.
                    gameInfo.FilePath = Path.Combine(Environment.CurrentDirectory, "ROMs", romCode);
                }

                // Finally, load the song count.
                // We don't save this so that we can expand the song table.
                gameInfo.SongCount = GetSongTableLength(br, gameInfo.SongTableOffset);
                song = new SongHeader();
                song.ID = (uint)gameInfo.SongCount; // We'll use this to say no song is loaded.

                // Load ROM Info.
                br.BaseStream.Seek(0xA0L, SeekOrigin.Begin);
                lblROMInfo.Text = "Name: " + Encoding.UTF8.GetString(br.ReadBytes(12)) +
                    "\nCode: " + Encoding.UTF8.GetString(br.ReadBytes(4)) +
                    "\nSong Table: 0x" + gameInfo.SongTableOffset.ToString("X") +
                    "\nSongs: " + gameInfo.SongCount;

                // Populate song combobox
                cSong.Items.Clear(); xx = true;
                for (uint i = 0; i < gameInfo.SongCount; i++)
                {
                    string name = gameInfo.GetSongName(i);
                    if (name != string.Empty)
                    {
                        cSong.Items.Add(name);
                    }
                }
                txtSong.Value = 0; xx = false;
                UpdateSongName(); // Try to select the first name.

            }

            // Done
            romFilePath = file;
            return OpenROMResult.Success;
        }

        private unsafe uint FindSongTable(GBABinaryReader br)
        {
            // select song offset
            br.BaseStream.Seek(0x0, SeekOrigin.Begin);
            byte[] rom = br.ReadBytes((int)br.BaseStream.Length);
            uint selectSongAddress = FindSelectSong(rom);

            if (selectSongAddress + 40 >= br.BaseStream.Length)
            {
                return (uint)br.BaseStream.Length;
            }
            else
            {
                // the song table pointer is just 40 more bytes away?
                br.BaseStream.Seek(selectSongAddress + 40L, SeekOrigin.Begin);
                uint offset = br.ReadPointer(); // Read the poitner!
                return offset;
            }
        }

        // This is based off of the Song Table finder from SapTapper.
        // It seems to work so far.
        private unsafe uint FindSelectSong(byte[] rom)
        {
            byte[] code = {
		        0x00, 0xB5, 0x00, 0x04, 0x07, 0x4A, 0x08, 0x49, 
		        0x40, 0x0B, 0x40, 0x18, 0x83, 0x88, 0x59, 0x00, 
		        0xC9, 0x18, 0x89, 0x00, 0x89, 0x18, 0x0A, 0x68, 
		        0x01, 0x68, 0x10, 0x1C, 0x00, 0xF0
	        };

            uint offset;

            // Do a loose comparison search
            fixed (byte* codePtr = &code[0])
            {
                for (offset = 0; offset < rom.Length - code.Length; offset += 4)
                {
                    fixed (byte* romPtr = &rom[offset])
                    {
                        if (MemoryCompareLoose(romPtr, codePtr, code.Length, 8) < 8)
                        {
                            break;
                        }
                    }
                }
            }

            return offset;
        }

        private unsafe int MemoryCompareLoose(byte* main, byte* other, int count, int maxDifferences)
        {
            int diff = 0;

            if (maxDifferences == 0) return 0;

            for (int i = 0; i < count; i++)
            {
                if (*(main + i) != *(other + i))
                {
                    diff++;

                    if (diff >= maxDifferences) break;
                }
            }

            return diff;
        }

        private int GetSongTableLength(GBABinaryReader br, uint tableOffset)
        {
            br.BaseStream.Seek(tableOffset, SeekOrigin.Begin);

            // Basically count the number of valid entries in the song table.
            int count = 0;
            while(br.BaseStream.Position + 8 < br.BaseStream.Length)
            {
                // Check if valid offset
                uint u = br.ReadUInt32();
                if (!(u > 0x8000000 && u - 0x8000000 < br.BaseStream.Length)) break;

                // Skip other crap in song table entry
                br.BaseStream.Seek(4L, SeekOrigin.Current);

                // Increase count
                count++;
            }
            return count;
        }

        private void LoadSongHeader()
        {
            uint songID = (uint)txtSong.Value;
            uint tableOffset = gameInfo.SongTableOffset + (songID * 8);

            //BinaryReader br = new BinaryReader(File.OpenRead(romFilePath));
            using (GBABinaryReader br = new GBABinaryReader(romFilePath))
            {
                br.BaseStream.Seek(tableOffset, SeekOrigin.Begin);

                // Song Table Entry
                song = new SongHeader();
                song.ID = songID;
                song.TableOffset = tableOffset;
                song.HeaderOffset = br.ReadPointer();
                song.TrackGroup = br.ReadByte();
                // filler, trackgroup again, filler

                // Song Header
                br.BaseStream.Seek(song.HeaderOffset, SeekOrigin.Begin);
                song.Tracks = new uint[br.ReadByte()];
                song.Unknown = br.ReadByte();
                song.Priority = br.ReadByte();
                song.Reverb = br.ReadByte();

                song.Voicegroup = br.ReadPointer();

                for (int i = 0; i < song.Tracks.Length; i++)
                {
                    song.Tracks[i] = br.ReadPointer();
                }

                //br.Dispose();
            }
        }

        private void SaveSongHeader()
        {
            //if (song == null) return;
            if (song.ID >= gameInfo.SongCount) return;

            //BinaryWriter bw = new BinaryWriter(File.OpenWrite(romFilePath));
            using (GBABinaryWriter bw = new GBABinaryWriter(romFilePath))
            {
                // We'll go straight to the header writing, and skip the table entry. ;)
                bw.BaseStream.Seek(song.HeaderOffset, SeekOrigin.Begin);
                bw.Write((byte)song.Tracks.Length);
                bw.Write(song.Unknown);
                bw.Write(song.Priority);
                bw.Write(song.Reverb);

                bw.WritePointer(song.Voicegroup); // This is what will probably change most.

                for (int i = 0; i < song.Tracks.Length; i++)
                {
                    bw.WritePointer(song.Tracks[i]);
                }

                //bw.Dispose();
            }
        }

        #endregion

        #region Song Selection

        private void ChangeSong()
        {
            // Load
            LoadSongHeader();

            // Display
            xx = true;
            lblHeaderOffset.Text = "Header: 0x" + song.HeaderOffset.ToString("X");
            lblVoicegroupOffset.Text = "Voicegroup: 0x" + song.Voicegroup.ToString("X");

            lstTracks.Items.Clear();
            for (int i = 0; i < song.Tracks.Length; i++)
            {
                var item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add("0x" + song.Tracks[i].ToString("X"));
                lstTracks.Items.Add(item);
            }

            bEditTrack.Enabled = false;
            xx = false;
        }

        private void TrySelectSong(string name)
        {
            // Figure out the matching index to name
            int i = 0;
            while (i < cSong.Items.Count)
            {
                if (cSong.Items[i].ToString() == name) break;
                i++;
            }

            if (i < cSong.Items.Count)
            {
                cSong.SelectedIndex = i;
            }
            /*else
            {
                cSong.SelectedIndex = 0;
            }*/
        }

        private void txtSong_TextChanged(object sender, EventArgs e)
        {
            if (xx) return;

            if (txtSong.Value >= gameInfo.SongCount)
            {
                xx = true;
                txtSong.Value = (uint)gameInfo.SongCount - 1;
                xx = false;
            }
            else if (txtSong.Value < 0)
            {
                xx = true;
                txtSong.Value = 0;
                xx = false;
            }

            UpdateSongName();
            ChangeSong();
        }

        private void cSong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xx) return;

            UpdateSongID();
            ChangeSong();
        }

        private void bSongMinus_Click(object sender, EventArgs e)
        {
            // Subtract one
            uint val = txtSong.Value - 1;
            if (val < 0) val = (uint)gameInfo.SongCount - 1;

            // Update
            txtSong.Value = val;
            UpdateSongName();

            ChangeSong();
        }

        private void bSongPlus_Click(object sender, EventArgs e)
        {
            // Add one
            uint val = txtSong.Value + 1;
            if (val >= gameInfo.SongCount) val = 0;

            // Update
            txtSong.Value = val;
            UpdateSongName();

            ChangeSong();
        }

        private void UpdateSongName()
        {
            // Try to select the current song, if possible.
            uint song = (uint)txtSong.Value; xx = true;
            if (gameInfo.GetSongName(song) != string.Empty && gameInfo.GetSongName(song) != "")
            {
                TrySelectSong(gameInfo.GetSongName(song));
            } xx = false;
        }

        private void UpdateSongID()
        {
            // Get the song's ID
            string name = cSong.Items[cSong.SelectedIndex].ToString(); xx = true;
            txtSong.Value = gameInfo.GetSongID(name); xx = false;
        }

        #endregion

        #region Song Controls

        private void lblVoicegroupOffset_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gameInfo.ROMCode == string.Empty) return;

            InputOffsetDialog iod = new InputOffsetDialog("Enter a new voicegroup offset:", song.Voicegroup, "Change Voicegroup");
            if (iod.ShowDialog() == DialogResult.OK)
            {
                // Write new voicegroup to ROM
                song.Voicegroup = iod.Offset;
                SaveSongHeader();

                // Update display
                lblVoicegroupOffset.Text = "Voicegroup: 0x" + song.Voicegroup.ToString("X");
            }
        }

        private void lstTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xx) return;

            int trackNum = -1;
            foreach (int x in lstTracks.SelectedIndices) trackNum = x;

            if (trackNum >= 0)
                bEditTrack.Enabled = true;
            else
                bEditTrack.Enabled = false;
        }

        private void bEditTrack_Click(object sender, EventArgs e)
        {
            // Just a safety check (hopefully not needed)
            if (song.ID >= gameInfo.SongCount) return;

            // Get selected track
            int trackNum = 0;
            foreach (int x in lstTracks.SelectedIndices) trackNum = x;

            // Show editor
            TrackEditorForm tef = new TrackEditorForm(romFilePath, song.Tracks[trackNum]);
            tef.Text = "Edit Track " + song.ID + "-" + (trackNum + 1) + " @ 0x" + song.Tracks[trackNum].ToString("X");
            tef.TrackSaved += TrackEditorForm_TrackSaved;
            tef.ShowDialog();

            // Reload if necessary
            //if (tef.NeedToReloadMainForm) ChangeSong();
        }

        // Called whenever a track editor presses the save button.
        private void TrackEditorForm_TrackSaved(TrackEditorForm.TrackSavedEventArgs e)
        {
            // Repoint all references to mah track, yo!
            if (e.Repointed)
            {
                // Do it.
                Tasks.FindAndReplacePointer(romFilePath, e.OldOffset, e.NewOffset);
                // It can return all the old offsets... maybe I should tell the user?

                // And then, reload that biatch
                ChangeSong();
            }
        }

        #endregion

    }
}
