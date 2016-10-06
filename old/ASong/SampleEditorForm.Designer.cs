namespace ASong
{
    partial class SampleEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtOffset = new System.Windows.Forms.ToolStripTextBox();
            this.bLoad = new System.Windows.Forms.ToolStripButton();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bExport = new System.Windows.Forms.ToolStripButton();
            this.bImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bPlay = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pWave = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoopTo = new ASong.NumericTextBox();
            this.chkCompressed = new System.Windows.Forms.CheckBox();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pWave)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripSeparator2,
            this.bPlay});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(468, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtOffset
            // 
            this.txtOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(100, 25);
            this.txtOffset.Text = "0x0";
            // 
            // bLoad
            // 
            this.bLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bLoad.Image = global::ASong.Properties.Resources.wrench_screwdriver;
            this.bLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(23, 22);
            this.bLoad.Text = "Load";
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
            this.bPlay.Text = "Play?";
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pWave);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 293);
            this.panel1.TabIndex = 2;
            // 
            // pWave
            // 
            this.pWave.Location = new System.Drawing.Point(0, 0);
            this.pWave.Name = "pWave";
            this.pWave.Size = new System.Drawing.Size(400, 272);
            this.pWave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pWave.TabIndex = 0;
            this.pWave.TabStop = false;
            this.pWave.Paint += new System.Windows.Forms.PaintEventHandler(this.pWave_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 324);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "(Samples are shorter than they appear.)";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLoopTo);
            this.groupBox1.Controls.Add(this.chkCompressed);
            this.groupBox1.Controls.Add(this.chkLoop);
            this.groupBox1.Controls.Add(this.lblLength);
            this.groupBox1.Location = new System.Drawing.Point(12, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 81);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Loop To:";
            // 
            // txtLoopTo
            // 
            this.txtLoopTo.Location = new System.Drawing.Point(62, 55);
            this.txtLoopTo.MaxValue = ((uint)(4294967294u));
            this.txtLoopTo.Name = "txtLoopTo";
            this.txtLoopTo.NumberStyle = ASong.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtLoopTo.Size = new System.Drawing.Size(100, 20);
            this.txtLoopTo.TabIndex = 3;
            this.txtLoopTo.Text = "0x0";
            this.txtLoopTo.Value = ((uint)(0u));
            this.txtLoopTo.TextChanged += new System.EventHandler(this.txtLoopTo_TextChanged);
            // 
            // chkCompressed
            // 
            this.chkCompressed.AutoSize = true;
            this.chkCompressed.Location = new System.Drawing.Point(77, 32);
            this.chkCompressed.Name = "chkCompressed";
            this.chkCompressed.Size = new System.Drawing.Size(84, 17);
            this.chkCompressed.TabIndex = 2;
            this.chkCompressed.Text = "Compressed";
            this.chkCompressed.UseVisualStyleBackColor = true;
            this.chkCompressed.CheckedChanged += new System.EventHandler(this.chkCompressed_CheckedChanged);
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Location = new System.Drawing.Point(9, 32);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(62, 17);
            this.chkLoop.TabIndex = 1;
            this.chkLoop.Text = "Looped";
            this.chkLoop.UseVisualStyleBackColor = true;
            this.chkLoop.CheckedChanged += new System.EventHandler(this.chkLoop_CheckedChanged);
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(6, 16);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(90, 13);
            this.lblLength.TabIndex = 0;
            this.lblLength.Text = "Original Length: 0";
            // 
            // SampleEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 433);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SampleEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SampleEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.SampleEditorForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pWave)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pWave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripTextBox txtOffset;
        private System.Windows.Forms.ToolStripButton bLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton bImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCompressed;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label label2;
        private NumericTextBox txtLoopTo;
    }
}