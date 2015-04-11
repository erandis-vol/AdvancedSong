using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using ASong.Playback;

namespace ASong
{
    public partial class SampleImportDialog : Form
    {
        private SampleEditorForm.Sample sample;
        private bool ok = false;

        private WaveOut waveOut = null;

        public SampleImportDialog(string filePath, SampleEditorForm.Sample sample)
        {
            InitializeComponent();
            this.sample = sample;
            lblFile.Text = "File: " + Path.GetFileName(filePath);
        }

        private void SampleImportDialog_Load(object sender, EventArgs e)
        {

        }

        private void SampleImportDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ok) DialogResult = DialogResult.OK;
            else DialogResult = DialogResult.Cancel;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            ok = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            Close();
        }

        private void trkChannels_Scroll(object sender, EventArgs e)
        {
            
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            if (waveOut == null)
            {
                var waveProvider = new SampleWaveProvider(sample);
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

                bPlay.Text = "Preview";
                bPlay.Image = Properties.Resources.play;
            }
        }

        private void pPreview_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, pPreview.Width, pPreview.Height);
            e.Graphics.DrawLine(Pens.Red, 0, 36, pPreview.Width, 36);
            e.Graphics.DrawLine(Pens.LightSteelBlue, 0, 4, pPreview.Width, 4);
            e.Graphics.DrawLine(Pens.LightSteelBlue, 0, 68, pPreview.Width, 68);

            // Draw graph
            Pen p = new Pen(new SolidBrush(Color.FromArgb(0, 248, 0)), 1);
            Pen p2 = new Pen(new SolidBrush(Color.FromArgb(164, 248, 164)));
            int things = (sample.Data.Length > pPreview.Width ? pPreview.Width : sample.Data.Length);
            for (int i = 0; i < things; i++)
            {
                e.Graphics.DrawLine(p2, i, 36 + (sample.Data[i] / 4), i, 36 + -(sample.Data[i] / 4));

                if (i > 0) // Draw it like this
                {
                    e.Graphics.DrawLine(p, i - 1, 36 + (sample.Data[i - 1] / 4), i, 36 + (sample.Data[i] / 4));
                    e.Graphics.DrawLine(p, i - 1, 36 + -(sample.Data[i - 1] / 4), i, 36 + -(sample.Data[i] / 4));
                }
            }
        }
    }
}
