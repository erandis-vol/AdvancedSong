namespace ASong
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuROM = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSongNames = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSongs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAssemble = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisassemble = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditVoicegroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFSF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSampleEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWaveformEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpROM = new System.Windows.Forms.GroupBox();
            this.lblROMInfo = new System.Windows.Forms.Label();
            this.grpSong = new System.Windows.Forms.GroupBox();
            this.bEditTrack = new System.Windows.Forms.Button();
            this.lblVoicegroupOffset = new System.Windows.Forms.Label();
            this.lblHeaderOffset = new System.Windows.Forms.Label();
            this.lstTracks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cSong = new System.Windows.Forms.ComboBox();
            this.bSongPlus = new System.Windows.Forms.Button();
            this.bSongMinus = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtSong = new ASong.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.grpROM.SuspendLayout();
            this.grpSong.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuROM,
            this.mnuSongs,
            this.toolsToolStripMenuItem,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuROM
            // 
            this.mnuROM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenROM,
            this.toolStripSeparator1,
            this.mnuEditSongNames});
            this.mnuROM.Name = "mnuROM";
            this.mnuROM.Size = new System.Drawing.Size(46, 20);
            this.mnuROM.Text = "ROM";
            // 
            // mnuOpenROM
            // 
            this.mnuOpenROM.Image = global::ASong.Properties.Resources.folder_horizontal_open;
            this.mnuOpenROM.Name = "mnuOpenROM";
            this.mnuOpenROM.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenROM.Size = new System.Drawing.Size(164, 22);
            this.mnuOpenROM.Text = "Open";
            this.mnuOpenROM.Click += new System.EventHandler(this.mnuOpenROM_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // mnuEditSongNames
            // 
            this.mnuEditSongNames.Image = global::ASong.Properties.Resources.document_table;
            this.mnuEditSongNames.Name = "mnuEditSongNames";
            this.mnuEditSongNames.Size = new System.Drawing.Size(164, 22);
            this.mnuEditSongNames.Text = "Edit Song Names";
            this.mnuEditSongNames.Click += new System.EventHandler(this.mnuEditSongNames_Click);
            // 
            // mnuSongs
            // 
            this.mnuSongs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAssemble,
            this.mnuDisassemble,
            this.toolStripSeparator2,
            this.mnuEditVoicegroup});
            this.mnuSongs.Name = "mnuSongs";
            this.mnuSongs.Size = new System.Drawing.Size(46, 20);
            this.mnuSongs.Text = "Song";
            // 
            // mnuAssemble
            // 
            this.mnuAssemble.Image = global::ASong.Properties.Resources.gear;
            this.mnuAssemble.Name = "mnuAssemble";
            this.mnuAssemble.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuAssemble.Size = new System.Drawing.Size(199, 22);
            this.mnuAssemble.Text = "Assemble";
            this.mnuAssemble.Click += new System.EventHandler(this.mnuAssemble_Click);
            // 
            // mnuDisassemble
            // 
            this.mnuDisassemble.Image = global::ASong.Properties.Resources.wrench_screwdriver;
            this.mnuDisassemble.Name = "mnuDisassemble";
            this.mnuDisassemble.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuDisassemble.Size = new System.Drawing.Size(199, 22);
            this.mnuDisassemble.Text = "Disassemble";
            this.mnuDisassemble.Click += new System.EventHandler(this.mnuDisassemble_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // mnuEditVoicegroup
            // 
            this.mnuEditVoicegroup.Image = global::ASong.Properties.Resources.equalizer_pencil;
            this.mnuEditVoicegroup.Name = "mnuEditVoicegroup";
            this.mnuEditVoicegroup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditVoicegroup.Size = new System.Drawing.Size(199, 22);
            this.mnuEditVoicegroup.Text = "Edit Voicegroup";
            this.mnuEditVoicegroup.Click += new System.EventHandler(this.mnuEditVoicegroup_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFSF,
            this.toolStripSeparator3,
            this.mnuSampleEditor,
            this.mnuWaveformEditor});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mnuFSF
            // 
            this.mnuFSF.Name = "mnuFSF";
            this.mnuFSF.Size = new System.Drawing.Size(166, 22);
            this.mnuFSF.Text = "Free Space Finder";
            this.mnuFSF.Click += new System.EventHandler(this.mnuFSF_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(163, 6);
            // 
            // mnuSampleEditor
            // 
            this.mnuSampleEditor.Name = "mnuSampleEditor";
            this.mnuSampleEditor.Size = new System.Drawing.Size(166, 22);
            this.mnuSampleEditor.Text = "Sample Editor";
            this.mnuSampleEditor.Click += new System.EventHandler(this.mnuSampleEditor_Click);
            // 
            // mnuWaveformEditor
            // 
            this.mnuWaveformEditor.Name = "mnuWaveformEditor";
            this.mnuWaveformEditor.Size = new System.Drawing.Size(166, 22);
            this.mnuWaveformEditor.Text = "Waveform Editor";
            this.mnuWaveformEditor.Click += new System.EventHandler(this.mnuWaveformEditor_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::ASong.Properties.Resources.information;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuAbout.Size = new System.Drawing.Size(149, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // grpROM
            // 
            this.grpROM.Controls.Add(this.lblROMInfo);
            this.grpROM.Location = new System.Drawing.Point(12, 27);
            this.grpROM.Name = "grpROM";
            this.grpROM.Size = new System.Drawing.Size(146, 100);
            this.grpROM.TabIndex = 1;
            this.grpROM.TabStop = false;
            this.grpROM.Text = "ROM Info.";
            // 
            // lblROMInfo
            // 
            this.lblROMInfo.AutoSize = true;
            this.lblROMInfo.Location = new System.Drawing.Point(6, 16);
            this.lblROMInfo.Name = "lblROMInfo";
            this.lblROMInfo.Size = new System.Drawing.Size(79, 13);
            this.lblROMInfo.TabIndex = 0;
            this.lblROMInfo.Text = "Open a ROM...";
            // 
            // grpSong
            // 
            this.grpSong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSong.Controls.Add(this.bEditTrack);
            this.grpSong.Controls.Add(this.lblVoicegroupOffset);
            this.grpSong.Controls.Add(this.lblHeaderOffset);
            this.grpSong.Controls.Add(this.lstTracks);
            this.grpSong.Controls.Add(this.cSong);
            this.grpSong.Controls.Add(this.bSongPlus);
            this.grpSong.Controls.Add(this.txtSong);
            this.grpSong.Controls.Add(this.bSongMinus);
            this.grpSong.Location = new System.Drawing.Point(164, 27);
            this.grpSong.Name = "grpSong";
            this.grpSong.Size = new System.Drawing.Size(274, 311);
            this.grpSong.TabIndex = 2;
            this.grpSong.TabStop = false;
            this.grpSong.Text = "Song";
            // 
            // bEditTrack
            // 
            this.bEditTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bEditTrack.Location = new System.Drawing.Point(6, 282);
            this.bEditTrack.Name = "bEditTrack";
            this.bEditTrack.Size = new System.Drawing.Size(75, 23);
            this.bEditTrack.TabIndex = 7;
            this.bEditTrack.Text = "Edit Track";
            this.bEditTrack.UseVisualStyleBackColor = true;
            this.bEditTrack.Click += new System.EventHandler(this.bEditTrack_Click);
            // 
            // lblVoicegroupOffset
            // 
            this.lblVoicegroupOffset.AutoSize = true;
            this.lblVoicegroupOffset.Location = new System.Drawing.Point(6, 58);
            this.lblVoicegroupOffset.Name = "lblVoicegroupOffset";
            this.lblVoicegroupOffset.Size = new System.Drawing.Size(84, 13);
            this.lblVoicegroupOffset.TabIndex = 6;
            this.lblVoicegroupOffset.Text = "Voicegroup: 0x0";
            this.lblVoicegroupOffset.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblVoicegroupOffset_MouseDoubleClick);
            // 
            // lblHeaderOffset
            // 
            this.lblHeaderOffset.AutoSize = true;
            this.lblHeaderOffset.Location = new System.Drawing.Point(6, 45);
            this.lblHeaderOffset.Name = "lblHeaderOffset";
            this.lblHeaderOffset.Size = new System.Drawing.Size(65, 13);
            this.lblHeaderOffset.TabIndex = 5;
            this.lblHeaderOffset.Text = "Header: 0x0";
            // 
            // lstTracks
            // 
            this.lstTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTracks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstTracks.FullRowSelect = true;
            this.lstTracks.GridLines = true;
            this.lstTracks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstTracks.HideSelection = false;
            this.lstTracks.Location = new System.Drawing.Point(6, 74);
            this.lstTracks.Name = "lstTracks";
            this.lstTracks.Size = new System.Drawing.Size(262, 202);
            this.lstTracks.TabIndex = 4;
            this.lstTracks.UseCompatibleStateImageBehavior = false;
            this.lstTracks.View = System.Windows.Forms.View.Details;
            this.lstTracks.SelectedIndexChanged += new System.EventHandler(this.lstTracks_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Track";
            this.columnHeader1.Width = 48;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Offset";
            this.columnHeader2.Width = 72;
            // 
            // cSong
            // 
            this.cSong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cSong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSong.DropDownWidth = 204;
            this.cSong.FormattingEnabled = true;
            this.cSong.Location = new System.Drawing.Point(82, 20);
            this.cSong.Name = "cSong";
            this.cSong.Size = new System.Drawing.Size(148, 21);
            this.cSong.TabIndex = 3;
            this.cSong.SelectedIndexChanged += new System.EventHandler(this.cSong_SelectedIndexChanged);
            // 
            // bSongPlus
            // 
            this.bSongPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSongPlus.Image = global::ASong.Properties.Resources.plus;
            this.bSongPlus.Location = new System.Drawing.Point(236, 19);
            this.bSongPlus.Name = "bSongPlus";
            this.bSongPlus.Size = new System.Drawing.Size(32, 23);
            this.bSongPlus.TabIndex = 2;
            this.bSongPlus.UseVisualStyleBackColor = true;
            this.bSongPlus.Click += new System.EventHandler(this.bSongPlus_Click);
            // 
            // bSongMinus
            // 
            this.bSongMinus.Image = global::ASong.Properties.Resources.minus;
            this.bSongMinus.Location = new System.Drawing.Point(6, 19);
            this.bSongMinus.Name = "bSongMinus";
            this.bSongMinus.Size = new System.Drawing.Size(32, 23);
            this.bSongMinus.TabIndex = 0;
            this.bSongMinus.UseVisualStyleBackColor = true;
            this.bSongMinus.Click += new System.EventHandler(this.bSongMinus_Click);
            // 
            // txtSong
            // 
            this.txtSong.Location = new System.Drawing.Point(44, 21);
            this.txtSong.MaxValue = ((uint)(4294967294u));
            this.txtSong.Name = "txtSong";
            this.txtSong.Size = new System.Drawing.Size(32, 20);
            this.txtSong.TabIndex = 1;
            this.txtSong.Text = "0";
            this.txtSong.Value = ((uint)(0u));
            this.txtSong.TextChanged += new System.EventHandler(this.txtSong_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "v0.5 [Alpha]";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpSong);
            this.Controls.Add(this.grpROM);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Song [Alpha]";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpROM.ResumeLayout(false);
            this.grpROM.PerformLayout();
            this.grpSong.ResumeLayout(false);
            this.grpSong.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuROM;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenROM;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox grpROM;
        private System.Windows.Forms.GroupBox grpSong;
        private System.Windows.Forms.Label lblROMInfo;
        private NumericTextBox txtSong;
        private System.Windows.Forms.Button bSongMinus;
        private System.Windows.Forms.ComboBox cSong;
        private System.Windows.Forms.Button bSongPlus;
        private System.Windows.Forms.ListView lstTracks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblVoicegroupOffset;
        private System.Windows.Forms.Label lblHeaderOffset;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSongNames;
        private System.Windows.Forms.ToolStripMenuItem mnuSongs;
        private System.Windows.Forms.ToolStripMenuItem mnuDisassemble;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem mnuAssemble;
        private System.Windows.Forms.Button bEditTrack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuEditVoicegroup;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuWaveformEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuSampleEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuFSF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label label1;
    }
}

