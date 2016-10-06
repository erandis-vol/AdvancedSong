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

namespace ASong
{
    public partial class AssembleSongDialog : Form
    {
        private bool ok = false;

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        static extern bool PathCompactPathEx(
           [Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

        public AssembleSongDialog(string fileToAssemble, uint track1, uint header, uint voicegroup)
        {
            InitializeComponent();
            //lblFile.Text = "File: " + (fileToAssemble.Length > 120 ? fileToAssemble.Substring(0, 120) + "..." : fileToAssemble);
            lblFile.Text = ".s File: " + ShrinkFilePath(fileToAssemble, 64);
            txtTracks.Value = track1;
            txtVoicegroup.Value = voicegroup;
            txtHeader.Value = header;
        }

        private void AssembleSongDialog_Load(object sender, EventArgs e)
        {

        }

        private void AssembleSongDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ok) DialogResult = DialogResult.OK;
            else DialogResult = DialogResult.Cancel;
        }

        private string ShrinkFilePath(string path, int desiredLength)
        {
            StringBuilder sb = new StringBuilder(desiredLength + 1);
            PathCompactPathEx(sb, path, desiredLength + 1, 0);
            return sb.ToString();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            ok = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            Close();
        }

        private void bHelp1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the offset that the song's track data will be written to." +
                "\n\nBy default, it will be the first track of the original song.\n\nIf you " +
                "think the new song will be longer than the old song, you should change this.\n" + 
                "Not changing it can be destructive if the song was too large, as it will likely" +
                " overwrite the next song's data.",
                "Help!", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void bHelp2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This offset specifies the location of a piece of data that tells" +
                " the M4A engine how to play various sounds/instruments.\n\nIf the instruments" +
                " used by the original song match those used by yours, then leave this alone." +
                " Otherwise, look through some other songs first to find one that matches.\n\n" +
                "If the voicegroup does not work, it can be easily changed by double-clicking" +
                " its label.",
                "Help!", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void bHelp3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The song header is a small piece of data the gives basic info. about" +
                " the song to the M4A engine, like voicegroup and track offsets.\n\nIf your song" +
                " has more tracks than the original, provide a new offset here.\nYou will need " +
                "8 + (track count * 4) bytes of free space. ;)",
                "Help!", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        public uint TracksOffset
        {
            get { return txtTracks.Value; }
        }

        public uint Voicegroup
        {
            get { return txtVoicegroup.Value; }
        }

        public uint HeaderOffset
        {
            get { return txtHeader.Value; }
        }
    }
}
