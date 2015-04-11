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
    public partial class VoicegroupEditorForm : Form
    {
        // An unused instrument is always this. Thanks Bregalad!
        private byte[] unused = { 0x01, 0x3C, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x00 };
        // Blank instruments, keeps things clean.
        private byte[] blankDirectSound = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00 };
        private byte[] blankSquare1 = { 0x01, 0x3C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] blankSquare2 = { 0x02, 0x3C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] blankMulti = { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x08 };
        private byte[] blankDrums = { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00 };

        private string romPath;
        private uint voicegroupOffset;
        private byte[] voicegroupData;
        private int selectedVoice = -1;

        private bool ysa = false;

        public VoicegroupEditorForm(string rom, uint voicegroup)
        {
            InitializeComponent();

            romPath = rom;
            voicegroupOffset = voicegroup;
            voicegroupData = new byte[12];
        }

        private void VoicegroupEditorForm_Load(object sender, EventArgs e)
        {
            // Load the data, which was provided in the constructor.
            Text = "Edit Voicegroup @ 0x" + voicegroupOffset.ToString("X");

            // Now, display it.
            ysa = true;
            using (BinaryReader br = new BinaryReader(File.OpenRead(romPath)))
            {
                br.BaseStream.Seek(voicegroupOffset, SeekOrigin.Begin);

                listVoices.Items.Clear();
                for (int i = 0; i < 128; i++)
                {
                    ListViewItem it = new ListViewItem(i.ToString());
                    it.SubItems.Add("0x" + (voicegroupOffset + i * 12).ToString("X"));
                    it.SubItems.Add(GetInstrumentName(br.ReadByte()));
                    listVoices.Items.Add(it);

                    br.BaseStream.Seek(11, SeekOrigin.Current);
                }
            }

            // Yeah...
            ColorInstrumentListView();
            ysa = false;

            // Hide all control panels
            pnlDirectSound.Visible = false;
            pnlDrums.Visible = false;
            pnlMulti.Visible = false;
            pnlSQ1.Visible = false;
            pnlSQ2.Visible = false;
            pnlPW.Visible = false;
            pnlNoise.Visible = false;
            cType.Enabled = false;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (selectedVoice > -1)
            {
                SaveVoice(selectedVoice);
            }
            // TODO: More?
        }

        private void LoadVoice(int index)
        {
            /*BinaryReader br = new BinaryReader(File.OpenRead(romPath));
            br.BaseStream.Seek(voicegroupOffset, SeekOrigin.Begin);

            // There should be 128 instruments, going every 12 bytes
            for (int i = 0; i < 128; i++)
            {
                byte[] instrument = br.ReadBytes(12);
                voicegroupData[i] = instrument;
                // TODO: structs or classes for this stuff.
            }

            br.Dispose();*/

            using (BinaryReader br = new BinaryReader(File.OpenRead(romPath)))
            {
                br.BaseStream.Seek(voicegroupOffset + index * 12, SeekOrigin.Begin);
                voicegroupData = br.ReadBytes(12);
                // TODO: Put this in an object.
            }
        }

        private void SaveVoice(int index)
        {
            // Save instrument at offset...
            using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(romPath)))
            {
                bw.BaseStream.Seek(voicegroupOffset + index * 12, SeekOrigin.Begin);
                bw.Write(voicegroupData);

                /*for (int i = 0; i < 128; i++)
                {
                    // Write the 12 byte data sets
                    bw.Write(voicegroupData[i]);
                    // TODO: Make this better than dealing with an array.
                }*/
            }
        }

        private void ColorInstrumentListView()
        {
            ysa = true;
            using (BinaryReader br = new BinaryReader(File.OpenRead(romPath)))
            {
                br.BaseStream.Seek(voicegroupOffset, SeekOrigin.Begin);
                for (int i = 0; i < 128; i++)
                {
                    byte[] voice = br.ReadBytes(12);
                    if (i == selectedVoice && IsUnusedInstrument(voicegroupData))
                    {
                        listVoices.Items[i].BackColor = Color.LightPink;
                    }
                    else if (IsUnusedInstrument(voice))
                    {
                        listVoices.Items[i].BackColor = Color.LightPink;
                    }
                    else
                    {
                        listVoices.Items[i].BackColor = SystemColors.Window;
                    }
                }
            }
            ysa = false;
        }

        private bool IsUnusedInstrument(byte[] data)
        {
            // Compare an instrument against the "unused instrument" provided by sappy.txt
            for (int x = 0; x < 12; x++)
            {
                if (unused[x] != data[x])
                {
                    return false;
                }
            }
            return true;
        }

        private string GetInstrumentName(byte type)
        {
            // Just a lot of different types.
            switch (type)
            {
                case 0x0: return "DirectSound";
                case 0x1:
                case 0x9: return "Square Wave 1";
                case 0x2:
                case 0xA: return "Square Wave 2";
                case 0x3:
                case 0xB: return "Programmable Wave";
                case 0x4:
                case 0xC: return "Noise";
                case 0x8: return "Static DirectSound";
                case 0x40: return "Multi-Sample";
                case 0x80: return "Drum Sample";
                default: return "?";
            }
        }

        private void listVoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            // Get selected
            foreach (int s in listVoices.SelectedIndices) selectedVoice = s;

            // Do something about it. ;)
            ChangeVoice();
        }

        private void cType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa || selectedVoice == -1) return;

            // Fill voice data
            for (int i = 0; i < 12; i++) voicegroupData[i] = 0;

            // Set type.
            switch (cType.SelectedIndex)
            {
                default: // There are lots of types. I wish this was easier...
                case 0: blankDirectSound.CopyTo(voicegroupData, 0); break;
                case 1: blankSquare1.CopyTo(voicegroupData, 0); break;
                case 2: blankSquare2.CopyTo(voicegroupData, 0); break;
                case 3: voicegroupData[0] = 0x3; break; // TODO: More of this crap.
                case 4: voicegroupData[0] = 0x4; break;
                case 5: voicegroupData[0] = 0x8; break;
                case 6: blankMulti.CopyTo(voicegroupData, 0); break;
                case 7: blankDrums.CopyTo(voicegroupData, 0); break;
            }

            // And go with it.
            SwapAndFillDisplay();
            ColorInstrumentListView();

            // Then, update the item in the listView
            ysa = true;
            listVoices.Items[selectedVoice].SubItems[2].Text = GetInstrumentName(voicegroupData[0]);
            ysa = false;
        }

        private void ChangeVoice()
        {
            // No need to load, but we do need to get our data.
            LoadVoice(selectedVoice);
            
            // Then, display it.
            ysa = true;

            // Show type
            cType.SelectedIndex = SelectVoiceType(voicegroupData[0]);
            cType.Enabled = true;

            // Change display based upon type
            SwapAndFillDisplay();

            ysa = false;
        }

        private void SwapAndFillDisplay()
        {
            byte type = voicegroupData[0];

            // Hide all.
            pnlDirectSound.Visible = false;
            pnlDrums.Visible = false;
            pnlMulti.Visible = false;
            pnlSQ1.Visible = false;
            pnlSQ2.Visible = false;
            pnlPW.Visible = false;
            pnlNoise.Visible = false;

            // Then show the correct stuff
            ysa = true;
            if (type == 0x0 || type == 0x8) // The two direct sound ones.
            {
                pnlDirectSound.Visible = true;
                //bDSSampleEdit.Enabled = false; // Later~

                cDSBaseNote.SelectedIndex = voicegroupData[1];
                if (type == 0x0)
                {
                    cDSBaseNote.Enabled = true;
                }
                else
                {
                    cDSBaseNote.Enabled = false;
                }

                if (voicegroupData[3] >= 0x80) // Last bit indicates whether pan is forced or not. ;)
                {
                    chkDSPan.Checked = true;
                    txtDSPan.Enabled = true;
                    txtDSPan.Value = (uint)voicegroupData[3] & 0x7F;
                }
                else
                {
                    chkDSPan.Checked = false;
                    txtDSPan.Enabled = false;
                    txtDSPan.Value = 0;
                }

                txtDSSample.Value = BitConverter.ToUInt32(voicegroupData, 4) - 0x08000000;
                txtDSAtk.Value = voicegroupData[8];
                txtDSDec.Value = voicegroupData[9];
                txtDSSus.Value = voicegroupData[10];
                txtDSRel.Value = voicegroupData[11];
            }
            else if (type == 0x1 || type == 0x9)
            {
                pnlSQ1.Visible = true;

                chkSQ1Noise.Checked = (voicegroupData[0] == 0x9);

                cSQ1Pattern.SelectedIndex = voicegroupData[4];

                byte sweep = voicegroupData[3];
                cSQ1Time.SelectedIndex = (sweep >> 4) & 0xF; // should it be & 0x7?
                cSQ1Swift.SelectedIndex = sweep & 0xF;

                txtSQ1Atk.Value = voicegroupData[8];
                txtSQ1Dec.Value = voicegroupData[9];
                txtSQ1Sus.Value = voicegroupData[10];
                txtSQ1Rel.Value = voicegroupData[11];
            }
            else if (type == 0x2 || type == 0xA)
            {
                pnlSQ2.Visible = true;

                chkSQ2Noise.Checked = (voicegroupData[0] == 0xA);
                cSQ2Pattern.SelectedIndex = voicegroupData[4];

                txtSQ2Atk.Value = voicegroupData[8];
                txtSQ2Dec.Value = voicegroupData[9];
                txtSQ2Sus.Value = voicegroupData[10];
                txtSQ2Rel.Value = voicegroupData[11];
            }
            else if (type == 0x3 || type == 0xB)
            {
                pnlPW.Visible = true;

                chkPWNoise.Checked = (voicegroupData[0] == 0xB);
                txtPWWaveform.Value = BitConverter.ToUInt32(voicegroupData, 4) - 0x08000000;
                txtPWAtk.Value = voicegroupData[8];
                txtPWDec.Value = voicegroupData[9];
                txtPWSus.Value = voicegroupData[10];
                txtPWRel.Value = voicegroupData[11];
            }
            else if (type == 0x4 || type == 0xC)
            {
                pnlNoise.Visible = true;

                chkNZNoise.Checked = (voicegroupData[0] == 0xC);
                cNZPeriod.SelectedIndex = voicegroupData[4];
                txtNZAtk.Value = voicegroupData[8];
                txtNZDec.Value = voicegroupData[9];
                txtNZSus.Value = voicegroupData[10];
                txtNZRel.Value = voicegroupData[11];
            }
            else if (type == 0x40)
            {
                pnlMulti.Visible = true;
                txtMulti1.Value = BitConverter.ToUInt32(voicegroupData, 4) - 0x08000000;
                txtMulti2.Value = BitConverter.ToUInt32(voicegroupData, 8) - 0x08000000;
                //bEditMulti.Enabled = false;
                bEditMulti2.Enabled = false;
            }
            else if (type == 0x80)
            {
                pnlDrums.Visible = true;
                txtDrumSample.Value = BitConverter.ToUInt32(voicegroupData, 4) - 0x08000000;
                //bDrumEdit.Enabled = false; // Not yet~
            }
            ysa = false;

            ColorInstrumentListView();
        }

        private int SelectVoiceType(byte type)
        {
            switch(type)
            {
                case 0x0: return 0;
                case 0x1:
                case 0x9: return 1;
                case 0x2:
                case 0xA: return 2;
                case 0x3:
                case 0xB: return 3;
                case 0x4:
                case 0xC: return 4;
                case 0x8: return 5;
                case 0x40: return 6;
                case 0x80: return 7;
                default: return 0;
            }
        }

        #region DirectSound

        private void bDSSampleEdit_Click(object sender, EventArgs e)
        {
            // TODO: Open in Sample Editor.
            SampleEditorForm sef = new SampleEditorForm(romPath, txtDSSample.Value);
            sef.SampleRepointed += SampleEditor_SampleRepointed;
            sef.ShowDialog();
        }

        private void SampleEditor_SampleRepointed(uint oldOffset, uint newOffset)
        {
            ysa = true;
            // Update text
            txtDSSample.Value = newOffset;
            
            // Save in memory
            byte[] sample = BitConverter.GetBytes(newOffset + 0x08000000);
            for (int i = 0; i < 4; i++)
            {
                voicegroupData[4 + i] = sample[i];
            }
            ysa = false;
        }

        private void cDSBaseNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[1] = (byte)cDSBaseNote.SelectedIndex;
        }

        private void chkDSPan_CheckedChanged(object sender, EventArgs e)
        {
            txtDSPan.Enabled = chkDSPan.Checked;

            if (ysa) return;

            // Just allows us to get this started.
            if (chkDSPan.Checked == true)
            {
                voicegroupData[3] = 0x80;
            }
            else
            {
                voicegroupData[3] = 0x0;
            }

            ysa = true;
            txtDSPan.Value = 0;
            ysa = false;
        }

        private void txtDSPan_TextChanged(object sender, EventArgs e)
        {
            // Show what it means.
            lblDSPan.Text = "= c_v";
            if (txtDSPan.Value < 0x40)
            {
                lblDSPan.Text += "-" + (0x40 - txtDSPan.Value);
            }
            else
            {
                lblDSPan.Text += "+" + (txtDSPan.Value - 0x40);
            }

            if (ysa) return; // Machine set

            if (chkDSPan.Checked == true) // If enabled, set it.
            {
                voicegroupData[3] = (byte)(0x80 + txtDSPan.Value);
            }
        }

        private void txtDSSample_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte[] sample = BitConverter.GetBytes(txtDSSample.Value + 0x08000000);
            for (int i = 0; i < 4; i++)
            {
                voicegroupData[4 + i] = sample[i];
            }
        }

        private void txtDSAtk_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[8] = (byte)txtDSAtk.Value;
        }

        private void txtDSDec_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[9] = (byte)txtDSDec.Value;
        }

        private void txtDSSus_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[10] = (byte)txtDSSus.Value;
        }

        private void txtDSRel_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[11] = (byte)txtDSRel.Value;
        }

        #endregion

        #region Drums & Multi

        private void txtDrumSample_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte[] sample = BitConverter.GetBytes(txtDrumSample.Value + 0x08000000);
            for (int i = 0; i < 4; i++)
            {
                voicegroupData[4 + i] = sample[i];
            }
        }

        private void bDrumEdit_Click(object sender, EventArgs e)
        {
            VoicegroupEditorForm vef2 = new VoicegroupEditorForm(romPath, txtDrumSample.Value);
            vef2.ShowDialog();
        }

        private void txtMulti1_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte[] sample = BitConverter.GetBytes(txtMulti1.Value + 0x08000000);
            for (int i = 0; i < 4; i++)
            {
                voicegroupData[4 + i] = sample[i];
            }

        }

        private void bEditMulti_Click(object sender, EventArgs e)
        {
            VoicegroupEditorForm vef2 = new VoicegroupEditorForm(romPath, txtMulti1.Value);
            vef2.ShowDialog();
        }

        private void txtMulti2_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte[] sample = BitConverter.GetBytes(txtMulti2.Value + 0x08000000);
            for (int i = 0; i < 4; i++)
            {
                voicegroupData[8 + i] = sample[i];
            }
        }

        #endregion

        #region Square 1

        private void bSQ1Unused_Click(object sender, EventArgs e)
        {
            // Copy the unused data.
            unused.CopyTo(voicegroupData, 0);

            // And go with it.
            SwapAndFillDisplay();
            ColorInstrumentListView();
        }

        private void chkSQ1Noise_CheckedChanged(object sender, EventArgs e)
        {
            if (ysa) return;
            
            voicegroupData[0] = (byte)(chkSQ1Noise.Checked ? 0x9 : 0x1);
        }

        private void cSQ1Pattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[4] = (byte)cSQ1Pattern.SelectedIndex;
        }

        private void cSQ1Time_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte time = (byte)cSQ1Time.SelectedIndex; // Upper 4 bits
            byte sweep = voicegroupData[3];

            // Smush them together (and preserve the swift part)
            voicegroupData[3] = (byte)(((time << 4) & 0xF) | (sweep & 0xF));
        }

        private void cSQ1Swift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte swift = (byte)cSQ1Swift.SelectedIndex; // Lower 4 bits
            byte sweep = voicegroupData[3];

            // Smush, and preserve the time.
            voicegroupData[3] = (byte)((sweep & 0xF0) | (swift & 0xF));
        }

        private void txtSQ1Atk_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[8] = (byte)txtSQ1Atk.Value;
        }

        private void txtSQ1Dec_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[9] = (byte)txtSQ1Dec.Value;
        }

        private void txtSQ1Sus_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[10] = (byte)txtSQ1Sus.Value;
        }

        private void txtSQ1Rel_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[11] = (byte)txtSQ1Rel.Value;
        }

        #endregion

        #region Square 2

        private void cSQ2Pattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[4] = (byte)cSQ2Pattern.SelectedIndex;
        }

        private void chkSQ2Noise_CheckedChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[0] = (byte)(chkSQ2Noise.Checked ? 0xA : 0x2);
        }

        private void txtSQ2Atk_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[8] = (byte)txtSQ1Atk.Value;
        }

        private void txtSQ2Dec_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[9] = (byte)txtSQ2Dec.Value;
        }

        private void txtSQ2Sus_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[10] = (byte)txtSQ2Sus.Value;
        }

        private void txtSQ2Rel_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[11] = (byte)txtSQ2Rel.Value;
        }

        #endregion

        #region Programmable Wave

        private void txtPWWaveform_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            byte[] sample = BitConverter.GetBytes(txtPWWaveform.Value + 0x08000000);
            for (int i = 0; i < 4; i++)
            {
                voicegroupData[4 + i] = sample[i];
            }
        }

        // This is a sub-tool.
        private void bEditWaveform_Click(object sender, EventArgs e)
        {
            // Show it in the editor
            if (txtPWWaveform.Value < 0x6000000 - 16) // Try to limit the offset to inside the ROM
            {
                WaveformEditorForm wef = new WaveformEditorForm(romPath, txtPWWaveform.Value);
                wef.ShowDialog();
            }
        }

        private void chkPWNoise_CheckedChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[0] = (byte)(chkPWNoise.Checked ? 0xB : 0x3);
        }

        private void txtPWAtk_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[8] = (byte)txtPWAtk.Value;
        }

        private void txtPWDec_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[9] = (byte)txtPWDec.Value;
        }

        private void txtPWSus_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[10] = (byte)txtPWSus.Value;
        }

        private void txtPWRel_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[11] = (byte)txtPWRel.Value;
        }

        #endregion

        #region Noise

        private void cNZPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[4] = (byte)cNZPeriod.SelectedIndex;
        }

        private void chkNZNoise_CheckedChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[0] = (byte)(chkNZNoise.Checked ? 0xC : 0x4);
        }

        private void txtNZAtk_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[8] = (byte)txtNZAtk.Value;
        }

        private void txtNZDec_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[9] = (byte)txtNZDec.Value;
        }

        private void txtNZSus_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[10] = (byte)txtNZSus.Value;
        }

        private void txtNZRel_TextChanged(object sender, EventArgs e)
        {
            if (ysa) return;

            voicegroupData[11] = (byte)txtNZRel.Value;
        }

        #endregion

    }
}
