using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Wave;
using ASong.Playback;

namespace ASong
{
    public partial class WaveformEditorForm : Form
    {
        private string romFile;
        private uint waveformOffset;
        private byte[] waveformData;

        private WaveOut waveOut = null;

        private bool tye = false;

        public WaveformEditorForm(string rom)
        {
            InitializeComponent();

            romFile = rom;
            waveformOffset = 0;
            waveformData = new byte[32];
            for (int i = 0; i < 32; i++) waveformData[i] = 0; // Fill with 0 JIC
        }

        public WaveformEditorForm(string rom, uint waveform)
        {
            InitializeComponent();

            romFile = rom;
            waveformOffset = waveform;
            waveformData = new byte[32];
            for (int i = 0; i < 32; i++) waveformData[i] = 0; // Fill with 0 JIC

            // Specified offset edition
            txtOffset.Visible = false;
            bLoad.Visible = false;
        }

        private void WaveformEditorForm_Load(object sender, EventArgs e)
        {
            LoadWaveform();
            Text = "Edit Waveform @ 0x" + waveformOffset.ToString("X");

            UpdateWaveText();
        }

        private void WaveformEditorForm_FormClosing(object sender, FormClosingEventArgs e)
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
            waveformOffset = u;

            // Load the wave at said offset
            LoadWaveform();
            Text = "Edit Waveform @ 0x" + waveformOffset.ToString("X");

            // Show it
            UpdateWaveText();
            pWave.Invalidate();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            // This is all it takes.
            SaveWaveform();
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "waveform_" + waveformOffset.ToString("X") + ".raw";
            saveFileDialog1.Filter = "Raw Waveform Files|*.raw";
            saveFileDialog1.Title = "Export Waveform";
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(romFile);

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            File.WriteAllBytes(saveFileDialog1.FileName, waveformData);
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Raw Waveform Files|*.raw";
            openFileDialog1.Title = "Import Waveform";
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(romFile);

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            byte[] temp = File.ReadAllBytes(openFileDialog1.FileName);

            if (temp.Length == 32)
            {
                waveformData = temp;

                pWave.Invalidate();
                UpdateWaveText();
            }
            else
            {
                MessageBox.Show("It seems the file you tried to load isn't the correct size!\n" +
                    "It probably wasn't some waveform data... :(", "Uh-oh!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            if (waveOut == null)
            {
                var waveProvider = new WaveformWaveProvider(waveformData);
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
        }

        private void bAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This allows you to edit the 16-byte programmable waveform data found in an M4A instrument sample."+ 
                "\n\nYes. I know that it's small.",
                "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateWaveText()
        {
            tye = true;
            txtWave.Text = "";
            for (int i = 0; i < 32; i++)
            {
                txtWave.Text += waveformData[i].ToString("X");
            }
            tye = false;
        }

        private void ConvertWaveText()
        {
            if (txtWave.TextLength != 32) return;

            for (int i = 0; i < 32; i++)
            {
                byte b;
                if (byte.TryParse(txtWave.Text[i].ToString(), NumberStyles.HexNumber, null, out b))
                {
                    if (b <= 15) waveformData[i] = b;
                }
            }

            pWave.Invalidate();
        }

        private void LoadWaveform()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(romFile));
            br.BaseStream.Seek(waveformOffset, SeekOrigin.Begin);

            for (int i = 0; i < 16; i++)
            {
                byte b = br.ReadByte();

                // It is big endian, so is this the right order? Yes...
                waveformData[i * 2] = (byte)((b >> 4) & 0xF);
                waveformData[i * 2 + 1] = (byte)(b & 0xF);
            }
            br.Dispose();

            bSave.Text = "Save @ 0x" + waveformOffset.ToString("X");
        }

        private void SaveWaveform()
        {
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(romFile));
            bw.BaseStream.Seek(waveformOffset, SeekOrigin.Begin);

            for (int i = 0; i < 16; i++)
            {
                // We can flip it if the loading was wrong too.
                byte b = (byte)((waveformData[i * 2] << 4) | waveformData[i * 2 + 1]);
                bw.Write(b); // Simple enough.
            }

            bw.Dispose();
        }

        private void pWave_Paint(object sender, PaintEventArgs e)
        {
            // Draw grid and BG
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, pWave.Width, pWave.Height);
            e.Graphics.DrawLine(Pens.Red, 0, 64, pWave.Width, 64);
            for (int i = 8; i < 32; i += 8)
            {
                e.Graphics.DrawLine(Pens.MidnightBlue, i * 8 + 4, 0, i * 8 + 4, pWave.Height);
            }
            e.Graphics.DrawLine(Pens.LightSteelBlue, 0, 8, pWave.Width, 8);
            e.Graphics.DrawLine(Pens.LightSteelBlue, 0, 128, pWave.Width, 128);

            // Draw the wave
            Pen p = new Pen(new SolidBrush(Color.FromArgb(0, 248, 0)), 3);
            for (int i = 0; i < 32; i++)
            {
                // Draw a bar
                e.Graphics.DrawLine(p, i * 8, 128 - waveformData[i] * 8, (i + 1) * 8, 128 - waveformData[i] * 8);

                // Connect to previous
                if (i > 0)
                    e.Graphics.DrawLine(p, i * 8, 128 - waveformData[i - 1] * 8, i * 8, 128 - waveformData[i] * 8);
            }
        }

        private void pWave_MouseMove(object sender, MouseEventArgs e)
        {
            pWave_MouseThing(e);
        }

        private void pWave_MouseDown(object sender, MouseEventArgs e)
        {
            pWave_MouseThing(e);
        }

        private void pWave_MouseThing(MouseEventArgs e)
        {
            // Keep it in bounds
            if (e.Y < 8 || e.Y >= pWave.Height) return;
            if (e.X < 0 || e.X >= pWave.Width) return;

            int x = e.X / 8;
            int y = 16 - e.Y / 8;
            lblXY.Text = "(X, Y) = (" + x + ", " + y + ")";

            if (x < 32 && y < 16 && y > -1 && waveformData[x] == y)
            {
                pWave.Cursor = Cursors.SizeNS;
            }
            else if (e.Button == MouseButtons.None)
            {
                pWave.Cursor = Cursors.Hand;
            }

            // Do this
            if (e.Button == MouseButtons.Left)
            {
                if (x < 32 && y < 16)
                {
                    // Set the data
                    waveformData[x] = (byte)y;

                    // Now redraw
                    pWave.Invalidate();

                    // And update the code
                    UpdateWaveText();
                }
            }
        }

        private uint TryGetOffset()
        {
            uint u;
            if (uint.TryParse(txtOffset.Text.Replace("0x", ""), NumberStyles.HexNumber, null, out u)) return u;
            else return 0;
        }

        private void txtOffset_TextChanged(object sender, EventArgs e)
        {
            bLoad.Text = "Load @ 0x" + TryGetOffset().ToString("X");
        }

        private void txtWave_TextChanged(object sender, EventArgs e)
        {
            if (tye) return;

            ConvertWaveText();
        }

        private bool IsHexDigit(char c)
        {
            if (char.IsDigit(c)) return true;
            else if (c >= 'a' && c <= 'f') return true;
            else if (c >= 'A' && c <= 'F') return true;
            else return false;
        }

        private void txtWave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtWave.SelectionStart <= 32 && txtWave.SelectionStart > -1)
            {
                if (IsHexDigit(e.KeyChar) && txtWave.SelectionStart < 32)
                {
                    txtWave.SelectionLength = 1;
                    txtWave.SelectedText = e.KeyChar.ToString().ToUpper();
                }
                else if (e.KeyChar == '\b' && txtWave.SelectionStart > 0)
                {
                    txtWave.SelectionStart = txtWave.SelectionStart - 1;
                    txtWave.SelectionLength = 1;
                    txtWave.SelectedText = "0";

                    txtWave.SelectionStart = txtWave.SelectionStart - 1;
                }
            }
            e.Handled = true;
        }
    }
}
