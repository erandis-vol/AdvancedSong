using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using NAudio.Wave;
using ASong.Playback;

namespace ASong
{
    public partial class SampleEditorForm : Form
    {
        private string romFilePath;
        private uint sampleOffset;
        private Sample sampleData;

        private WaveOut waveOut = null;

        public delegate void SampleRepointedHandler(uint oldOffset, uint newOffset);
        public event SampleRepointedHandler SampleRepointed;

        private bool q = false;

        public SampleEditorForm(string rom)
        {
            InitializeComponent();

            romFilePath = rom;
            sampleOffset = 0;
            sampleData = new Sample();
        }

        public SampleEditorForm(string rom, uint sample)
        {
            InitializeComponent();

            romFilePath = rom;
            sampleOffset = sample;
            sampleData = new Sample();

            txtOffset.Visible = false;
            bLoad.Visible = false;
        }

        private void SampleEditorForm_Load(object sender, EventArgs e)
        {
            q = true;
            if (LoadSample())
            {
                Text = "Edit Sample @ 0x" + sampleOffset.ToString("X");

                // And set up the image
                //pWave.Size = new Size((int)sampleData.OriginalSize * 8, 272);
                //pWave.Invalidate();

                chkLoop.Checked = sampleData.Looped;
                chkCompressed.Checked = sampleData.Compressed;
                lblLength.Text = "Length: " + sampleData.OriginalSize + " bytes";
                txtLoopTo.Value = sampleData.LoopStart;
            }
            else
            {
                // Bad Data
                sampleData.Data = null;

                bSave.Enabled = false;
                bPlay.Enabled = false;
            }
            q = false;

            // Draw
            pWave.Image = DrawSample();
            pWave.Invalidate();

            // Show the sample data
            /*txtPCM.Text = "";
            for (int i = 0; i < sampleData.Data.Length; i++)
            {
                //if (sampleData.Looped && i == sampleData.LoopStart) txtPCM.Text += "L";
                txtPCM.Text += sampleData.Data[i].ToString() + " ";
            }*/

            
        }

        private void SampleEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop audio playback if still going.
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            // Get offset
            if (txtOffset.TextLength <= 0 || txtOffset.Text == "0x") return;
            uint u = TryGetOffset(); // Some minor error checking
            if (u > 0x6000000 - 16) return; // Just a size limiter (max size of ROM - 16)
            sampleOffset = u;

            // Try to load the sample
            if (LoadSample())
            {
                Text = "Edit Sample @ 0x" + sampleOffset.ToString("X");

                // And set up the image
                //pWave.Size = new Size((int)sampleData.OriginalSize * 8, 272);
                //pWave.Invalidate();

                bSave.Enabled = true;
                bPlay.Enabled = true;

                chkLoop.Checked = sampleData.Looped;
                chkCompressed.Checked = sampleData.Compressed;
                lblLength.Text = "Length: " + sampleData.OriginalSize + " bytes";
                txtLoopTo.Value = sampleData.LoopStart;
                groupBox1.Enabled = true;
            }
            else
            {
                // Bad Data
                sampleData.Data = null;
                //pWave.Size = new Size(8, 272);

                bSave.Enabled = false;
                bPlay.Enabled = false;

                groupBox1.Enabled = false;
            }

            pWave.Image = DrawSample();
            pWave.Invalidate();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveSample();
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            // Only allow valid samples to be exported
            if (sampleData.Data == null) return;

            // Show dialog
            saveFileDialog1.FileName = "sample_" + sampleOffset.ToString("X");
            saveFileDialog1.Filter = "Wave Files|*.wav|Assembly Files|*.s|RAW Sample Files|*.raw";
            saveFileDialog1.Title = "Export Sample";
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(romFilePath);

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            // Check format
            if (saveFileDialog1.FilterIndex == 1)
            {
                // Convert to Wave
                Conversion.Wave.gba2wav(saveFileDialog1.FileName, sampleData);
            }
            else if (saveFileDialog1.FilterIndex == 2)
            {
                // Assembly
                Disassembler.DisassembleSample(saveFileDialog1.FileName, sampleData);
            }
            else if (saveFileDialog1.FilterIndex == 3)
            {
                // RAW
                BinaryWriter bw = new BinaryWriter(File.Create(saveFileDialog1.FileName));

                // Header
                bw.Write((ushort)(sampleData.Compressed ? 0x1 : 0x0));
                bw.Write((ushort)(sampleData.Looped ? 0x4000 : 0x0));
                bw.Write(sampleData.Pitch);
                bw.Write(sampleData.LoopStart);
                bw.Write(sampleData.Data.Length);

                // Data
                foreach (sbyte s in sampleData.Data)
                {
                    bw.Write(s);
                }

                bw.Dispose();
            }
        }

        private void bImport_Click(object sender, EventArgs e)
        {            
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wave Files|*.wav";
            openFileDialog1.Title = "Import Sample";
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(romFilePath);

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            try
            {
                if (openFileDialog1.FilterIndex == 1)
                {
                    // Convert the .wav file
                    Sample sample = Conversion.Wave.wav2gba(openFileDialog1.FileName);

                    // Select my sample
                    SampleImportDialog sid = new SampleImportDialog(openFileDialog1.FileName, sample);
                    if (sid.ShowDialog() != DialogResult.OK) return;


                    // Set
                    sampleData.Data = sample.Data; // YEAH
                    sampleData.Pitch = sample.Pitch;

                    pWave.Image = DrawSample();
                    pWave.Invalidate();
                    lblLength.Text = "Original Length: " + sampleData.OriginalSize + " bytes - Length: " + sampleData.Data.Length + " bytes";

                    bPlay.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            if (waveOut == null)
            {
                var waveProvider = new SampleWaveProvider(sampleData);
                waveOut = new WaveOut();
                waveOut.Init(waveProvider);
                waveOut.Play();

                bPlay.Text = "Stop";
                bPlay.Image = Properties.Resources.stop;
            }
            else
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;

                bPlay.Text = "Play";
                bPlay.Image = Properties.Resources.play;
            }
            
            // Setup
            /*WaveFormat waveFormat = new WaveFormat((int)sampleData.Pitch / 1024, 8, 1);
            _queue = new byte[sampleData.Data.Length];
            for (int i = 0; i < _queue.Length; i++)
            {
                _queue[i] = (byte)sampleData.Data[i];
            }

            // And play
            using (WaveOutPlayer wop = new WaveOutPlayer(-1, waveFormat, 256, 1, PlayFiller))
            {
                bPlay.Enabled = false;
                Application.DoEvents();

                while (_queue.Length > 0)
                {
                    // Just hang tight
                }
                bPlay.Enabled = true;
            }*/
        }

        /*private static byte[] _queue = new byte[0];
        private static void PlayFiller(IntPtr data, int size)
        {
            var length = Math.Min(size, _queue.Length);
            Marshal.Copy(_queue, 0, data, length);
            _queue = _queue.Skip(size).ToArray();
        }*/

        private uint TryGetOffset()
        {
            uint u;
            if (uint.TryParse(txtOffset.Text.Replace("0x", ""), NumberStyles.HexNumber, null, out u)) return u;
            else return 0;
        }

        private bool LoadSample()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(romFilePath));
            br.BaseStream.Seek(sampleOffset, SeekOrigin.Begin);

            // Read the header
            sampleData = new Sample();

            // Check if comressed header works.
            ushort compressed = br.ReadUInt16(); // 0x1 = compressed, 0x0 = not
            if (compressed == 0x1) sampleData.Compressed = true;
            else if (compressed == 0) sampleData.Compressed = false;
            else return false;

            // Check if the looped format works
            ushort looped = br.ReadUInt16(); // 0x4000 = looped, 0x0 = not
            if (looped == 0x4000) sampleData.Looped = true;
            else if (looped == 0x0) sampleData.Looped = false;
            else return false;

            sampleData.Pitch = br.ReadUInt32(); // There needs to be some tables, no?
            sampleData.LoopStart = br.ReadUInt32() + 1; // Should I adjust these with +1?
            sampleData.OriginalSize = br.ReadUInt32() + 1;

            // Read PCM data
            if (!sampleData.Compressed)
            {
                // This is uncompressed
                sampleData.Data = new sbyte[sampleData.OriginalSize];
                for (int i = 0; i < sampleData.Data.Length; i++)
                {
                    sampleData.Data[i] = br.ReadSByte();
                }
            }
            else // Compressed -- uh-oh!
            {
                byte[] lookup_table = { 0x0, 0x1, 0x4, 0x9, 0x10, 0x19, 0x24, 0x31, 0xC0, 0xCF, 0xDC, 0xE7, 0xF0, 0xF7, 0xFC, 0xFF };

                //MessageBox.Show("Sample Data Length: " + sampleData.OriginalSize);s
                int originalSize = (int)sampleData.OriginalSize;
                List<sbyte> data = new List<sbyte>();
                int blockAlign = 0; sbyte pcmLevel = 0; int mySample = 0;
                for (int i = 0; ; i++)
                {
                    if (blockAlign == 0)
                    {
                        pcmLevel = br.ReadSByte();
                        
                        data.Add(pcmLevel);
                        blockAlign = 32;
                    }
                    else
                    {
                        byte input = br.ReadByte();

                        // Get nybble 1
                        byte delta = lookup_table[input >> 4];
                        if (blockAlign < 32)
                        {
                            pcmLevel += (sbyte)delta;
                            data.Add(pcmLevel);
                        }

                        // Get nybble 2
                        delta = lookup_table[input & 15];
                        pcmLevel += (sbyte)delta;
                        data.Add(pcmLevel);                        

                        // Moving along
                        mySample += 2;
                        if (mySample >= originalSize) break;

                        blockAlign -= 1;
                    }
                }
                sampleData.Data = data.ToArray();
                sampleData.OriginalSize = (uint)data.Count; // This is so we can save correctly
            }

            br.Dispose();
            return true;
        }

        private void SaveSample()
        {
            // First, remove compressed sampling.
            // I need to fix the code for it.
            if(sampleData.Compressed)
            {
                MessageBox.Show("Unable to save compressed samples!", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check for repoint
            if (sampleData.Data.Length > sampleData.OriginalSize)
            {
                // Get free space -- always equal to # of sbytes
                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(romFilePath, (uint)sampleData.Data.Length);
                if (fsf.ShowDialog() != DialogResult.OK) return;

                // Repoint
                uint newOffset = fsf.FreeSpaceOffset;
                Tasks.FindAndReplacePointer(romFilePath, sampleOffset, newOffset);

                // Share with my children.
                if (SampleRepointed != null) SampleRepointed.Invoke(sampleOffset, newOffset);
                txtOffset.Text = "0x" + newOffset.ToString("X");
                Text = "Edit Sample @ 0x" + newOffset.ToString("X");

                // And then continue
                sampleOffset = newOffset;
            }

            BinaryWriter bw = new BinaryWriter(File.OpenWrite(romFilePath));
            bw.BaseStream.Seek(sampleOffset, SeekOrigin.Begin);

            // Write the header
            bw.Write((ushort)(sampleData.Compressed ? 0x1 : 0x0)); // 1= compressed, 0 = not
            bw.Write((ushort)(sampleData.Looped ? 0x4000 : 0x0));
            bw.Write(sampleData.Pitch);
            bw.Write(sampleData.LoopStart - 1);
            if (sampleData.Compressed)
            {
                // Split the data into blocks
                int blockCount = sampleData.Data.Length / 65; // Gets # of blocks
                sbyte[][] blocks = new sbyte[blockCount][];
                for (int i = 0; i < blockCount; i++)
                {
                    // Blocks are 33 bytes each (in the ROM) but 65 for us
                    // The first is 8-bit PCM
                    // The next 32 will be nybbles matching the lookup table
                    blocks[i] = new sbyte[65];
                    for (int k = 0; k < 65; k++)
                    {
                        int index = i * 65 + k;
                        blocks[i][k] = (index >= sampleData.Data.Length ? (sbyte)0 : sampleData.Data[index]);
                    }
                }
                
                // Write compressed size -- total - # of blocks
                bw.Write((uint)(sampleData.Data.Length - blockCount - 1));

                // The delta lookup table
                sbyte[] lookup_table = { 0, 1, 4, 9, 16, 25, 36, 49, -64, -49, -36, -25, -16, -4, -1 };

                // Compress and write
                for (int block = 0; block < blockCount; block++)
                {
                    // Get initial block
                    sbyte pcmLevel = blocks[block][0];
                    //bw.Write(pcmLevel); // Write intial block PCM

                    // Then do the rest
                    for (int i = 0; i < 64; i += 2)
                    {
                        // How to get the next PCM set up:
                        // delta = newValue - pcmLevel;
                        // writeValue = lookup_table[delta]
                        // pcmLevel -= delta
                        int delta = pcmLevel - blocks[block][i + 1];
                        //MessageBox.Show("Delta: 0x" + delta.ToString("X2"));
                        byte writeValue = MatchDeltaToLookupTable(delta);
                        pcmLevel += (sbyte)delta; // Adjust

                        delta = pcmLevel - blocks[block][i + 2];
                        //MessageBox.Show("Delta: 0x" + delta.ToString("X2"));
                        writeValue += (byte)(MatchDeltaToLookupTable(delta) << 4);
                        pcmLevel += (sbyte)delta;

                        // Save
                        bw.Write(writeValue);

                        #region OLD
                        // Get first byte delta
                        /*byte delta = (byte)(last - blocks[block][i + 1]);
                        last -= delta;
                        //if (delta < 0) delta = -delta;

                        // Match said delta to the lookup_table
                        byte write = 0;
                        int diff = 255; byte best = 0;
                        for (byte l = 0; l < lookup_table.Length; l++)
                        {
                            // Get closest entry in the lookup table
                            int s = Math.Abs(lookup_table[l] - delta);
                            if (s < diff)
                            {
                                s = diff;
                                best = (byte)(l & 15);
                            }
                        }
                        write = best;

                        // Get second byte delta
                        delta = (byte)(last - blocks[block][i + 2]);
                        last -= delta;

                        // Match to the lookup table
                        diff = 255; best = 0;
                        for (byte l = 0; l < lookup_table.Length; l++)
                        {
                            int s = Math.Abs(lookup_table[l] - delta);
                            if (s < diff)
                            {
                                s = diff;
                                best = (byte)(l & 15);
                            }
                        }
                        write += (byte)(best << 4);

                        // Save -- did it work?
                        bw.Write(write);*/
                        #endregion
                    }
                }
            }
            else
            {
                // Write uncompressed size
                bw.Write((uint)sampleData.Data.Length - 1);

                // Write the data
                for (int i = 0; i < sampleData.Data.Length; i++)
                {
                    bw.Write(sampleData.Data[i]);
                }
            }

            bw.Dispose();
        }

        private byte MatchDeltaToLookupTable(int Δ)
        {
            //int[] lookup_table = { 0, 1, 4, 9, 16, 25, 36, 49, -64, -49, -36, -25, -16, -4, -1 };
            int[] positive = { 0, 1, 4, 9, 15, 25, 36, 49, 64 };
            int[] negative = { -64, -49, -36, -25, -9, -4, -1 };

            int bestMatch = 0; int difference = 255;
            if (Δ >= 0) // pos. Δ
            {
                for (int i = 0; i < positive.Length; i++)
                {
                    int d = Math.Abs(Δ - positive[i]);
                    if (d < difference) bestMatch = i;
                }
            }
            else // neg. Δ
            {
                for (int i = 0; i < negative.Length; i++)
                {
                    int d = Math.Abs(Δ - negative[i]);
                    if (d < difference) bestMatch = i + 8;
                }
            }
            return (byte)bestMatch;
        }

        private Bitmap DrawSample()
        {
            try
            {
                Bitmap bmp;
                if (sampleData != null && sampleData.Data != null)
                    bmp = new Bitmap((int)sampleData.Data.Length * 8, 272);
                else
                    bmp = new Bitmap(8, 272);
                Graphics g = Graphics.FromImage(bmp);

                // Griding and such
                g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                g.DrawLine(Pens.Red, 0, 136, bmp.Width, 136);
                if (sampleData.Data != null) // Only with data.
                {
                    for (int i = 8; i < (sampleData.Data.Length * 8); i += 8)
                    {
                        g.DrawLine(Pens.MidnightBlue, i * 8 + 4, 0, i * 8 + 4, bmp.Height);
                    }
                }
                g.DrawLine(Pens.LightSteelBlue, 0, 8, bmp.Width, 8);
                g.DrawLine(Pens.LightSteelBlue, 0, 264, bmp.Width, 264);

                // Check the mode
                if (sampleData.Data == null) return bmp;

                // Draw the PCM data -- this is pretty cool looking.
                Pen p = new Pen(new SolidBrush(Color.FromArgb(0, 248, 0)), 2); int scale = 8;
                Pen p2 = new Pen(new SolidBrush(Color.FromArgb(164, 248, 164)), 2);
                for (int i = 0; i < sampleData.Data.Length; i++)
                {
                    // Draw a bar
                    g.DrawLine(p, i * scale, 136 + sampleData.Data[i], (i + 1) * scale, 136 + sampleData.Data[i]);

                    // Connect the bars
                    if (i > 0)
                    {
                        g.DrawLine(p, i * scale, 136 + sampleData.Data[i - 1], i * scale, 136 + sampleData.Data[i]);
                    }
                }

                if (sampleData.Looped)
                {
                    g.DrawLine(Pens.Pink, sampleData.LoopStart * 8 + 4, 0, sampleData.LoopStart * 8 + 4, bmp.Height);
                }

                g.Dispose(); // Done
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new Bitmap(8, 272);
        }

        private void pWave_Paint(object sender, PaintEventArgs e)
        {
            // Nothing. Handled by a function now.
        }

        public class Sample
        {
            public bool Compressed; // :(
            public bool Looped;
            public uint Pitch;
            public uint LoopStart;
            public uint OriginalSize;
            public sbyte[] Data;

            public Sample()
            {
                Compressed = false;
                Looped = false;
                LoopStart = 0;
                OriginalSize = 0;
                Data = null;
            }
        }

        private void chkLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (q) return;

            sampleData.Looped = chkLoop.Checked;
        }

        private void chkCompressed_CheckedChanged(object sender, EventArgs e)
        {
            if (q) return;

            sampleData.Compressed = chkCompressed.Checked;
        }

        private void txtLoopTo_TextChanged(object sender, EventArgs e)
        {
            if (q) return;

            sampleData.LoopStart = txtLoopTo.Value;

            pWave.Image = DrawSample();
            pWave.Invalidate();
        }
    }
}
