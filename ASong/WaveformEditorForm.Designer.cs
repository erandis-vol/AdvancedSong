namespace ASong
{
    partial class WaveformEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveformEditorForm));
            this.txtWave = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtOffset = new System.Windows.Forms.ToolStripTextBox();
            this.bLoad = new System.Windows.Forms.ToolStripButton();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bExport = new System.Windows.Forms.ToolStripButton();
            this.bImport = new System.Windows.Forms.ToolStripButton();
            this.bAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bPlay = new System.Windows.Forms.ToolStripButton();
            this.lblXY = new System.Windows.Forms.Label();
            this.pWave = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pWave)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWave
            // 
            this.txtWave.Location = new System.Drawing.Point(12, 28);
            this.txtWave.Name = "txtWave";
            this.txtWave.Size = new System.Drawing.Size(256, 20);
            this.txtWave.TabIndex = 0;
            this.txtWave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWave.TextChanged += new System.EventHandler(this.txtWave_TextChanged);
            this.txtWave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWave_KeyPress);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtOffset,
            this.bLoad,
            this.bSave,
            this.toolStripSeparator1,
            this.bExport,
            this.bImport,
            this.bAbout,
            this.toolStripSeparator2,
            this.bPlay});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(280, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtOffset
            // 
            this.txtOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(100, 25);
            this.txtOffset.Text = "0x0";
            this.txtOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
            // 
            // bLoad
            // 
            this.bLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bLoad.Image = global::ASong.Properties.Resources.wrench_screwdriver;
            this.bLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(23, 22);
            this.bLoad.Text = "Load @ 0x0";
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bSave
            // 
            this.bSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bSave.Image = global::ASong.Properties.Resources.disk;
            this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(23, 22);
            this.bSave.Text = "Save";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bExport
            // 
            this.bExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bExport.Image = global::ASong.Properties.Resources.disks_black;
            this.bExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(23, 22);
            this.bExport.Text = "Export";
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // bImport
            // 
            this.bImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bImport.Image = global::ASong.Properties.Resources.folder_open;
            this.bImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bImport.Name = "bImport";
            this.bImport.Size = new System.Drawing.Size(23, 22);
            this.bImport.Text = "Import";
            this.bImport.Click += new System.EventHandler(this.bImport_Click);
            // 
            // bAbout
            // 
            this.bAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bAbout.Image = global::ASong.Properties.Resources.question;
            this.bAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAbout.Name = "bAbout";
            this.bAbout.Size = new System.Drawing.Size(23, 22);
            this.bAbout.Text = "About";
            this.bAbout.Click += new System.EventHandler(this.bAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bPlay
            // 
            this.bPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bPlay.Image = global::ASong.Properties.Resources.play;
            this.bPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(23, 22);
            this.bPlay.Text = "Play";
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // lblXY
            // 
            this.lblXY.AutoSize = true;
            this.lblXY.Location = new System.Drawing.Point(12, 194);
            this.lblXY.Name = "lblXY";
            this.lblXY.Size = new System.Drawing.Size(69, 13);
            this.lblXY.TabIndex = 3;
            this.lblXY.Text = "(X, Y) = (0, 0)";
            // 
            // pWave
            // 
            this.pWave.Location = new System.Drawing.Point(12, 54);
            this.pWave.Name = "pWave";
            this.pWave.Size = new System.Drawing.Size(256, 137);
            this.pWave.TabIndex = 2;
            this.pWave.TabStop = false;
            this.pWave.Paint += new System.Windows.Forms.PaintEventHandler(this.pWave_Paint);
            this.pWave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pWave_MouseDown);
            this.pWave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pWave_MouseMove);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // WaveformEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 216);
            this.Controls.Add(this.lblXY);
            this.Controls.Add(this.pWave);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtWave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaveformEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Waveform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaveformEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.WaveformEditorForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pWave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox pWave;
        private System.Windows.Forms.Label lblXY;
        private System.Windows.Forms.ToolStripButton bExport;
        private System.Windows.Forms.ToolStripButton bImport;
        private System.Windows.Forms.ToolStripButton bAbout;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripTextBox txtOffset;
        private System.Windows.Forms.ToolStripButton bLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bPlay;
    }
}