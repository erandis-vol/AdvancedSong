namespace ASong
{
    partial class AssembleSongDialog
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
            this.lblFile = new System.Windows.Forms.Label();
            this.txtTracks = new ASong.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVoicegroup = new ASong.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHeader = new ASong.NumericTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bHelp1 = new System.Windows.Forms.Button();
            this.bHelp2 = new System.Windows.Forms.Button();
            this.bHelp3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 9);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(47, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "File: ???";
            // 
            // txtTracks
            // 
            this.txtTracks.Location = new System.Drawing.Point(107, 25);
            this.txtTracks.MaxValue = ((uint)(16777215u));
            this.txtTracks.Name = "txtTracks";
            this.txtTracks.NumberStyle = ASong.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtTracks.Size = new System.Drawing.Size(100, 20);
            this.txtTracks.TabIndex = 2;
            this.txtTracks.Text = "0x0";
            this.txtTracks.Value = ((uint)(0u));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Write Tracks To:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Voicegroup:";
            // 
            // txtVoicegroup
            // 
            this.txtVoicegroup.Location = new System.Drawing.Point(107, 51);
            this.txtVoicegroup.MaxValue = ((uint)(16777215u));
            this.txtVoicegroup.Name = "txtVoicegroup";
            this.txtVoicegroup.NumberStyle = ASong.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtVoicegroup.Size = new System.Drawing.Size(100, 20);
            this.txtVoicegroup.TabIndex = 4;
            this.txtVoicegroup.Text = "0x0";
            this.txtVoicegroup.Value = ((uint)(0u));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Write Header To:";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(107, 77);
            this.txtHeader.MaxValue = ((uint)(16777215u));
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.NumberStyle = ASong.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtHeader.Size = new System.Drawing.Size(100, 20);
            this.txtHeader.TabIndex = 6;
            this.txtHeader.Text = "0x0";
            this.txtHeader.Value = ((uint)(0u));
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(12, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 1);
            this.panel1.TabIndex = 8;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(340, 110);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(259, 110);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 10;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "(If more tracks, change)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "(Good luck on this one)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(Offset of Track 1)";
            // 
            // bHelp1
            // 
            this.bHelp1.Image = global::ASong.Properties.Resources.question;
            this.bHelp1.Location = new System.Drawing.Point(213, 23);
            this.bHelp1.Name = "bHelp1";
            this.bHelp1.Size = new System.Drawing.Size(32, 23);
            this.bHelp1.TabIndex = 15;
            this.bHelp1.UseVisualStyleBackColor = true;
            this.bHelp1.Click += new System.EventHandler(this.bHelp1_Click);
            // 
            // bHelp2
            // 
            this.bHelp2.Image = global::ASong.Properties.Resources.question;
            this.bHelp2.Location = new System.Drawing.Point(213, 49);
            this.bHelp2.Name = "bHelp2";
            this.bHelp2.Size = new System.Drawing.Size(32, 23);
            this.bHelp2.TabIndex = 16;
            this.bHelp2.UseVisualStyleBackColor = true;
            this.bHelp2.Click += new System.EventHandler(this.bHelp2_Click);
            // 
            // bHelp3
            // 
            this.bHelp3.Image = global::ASong.Properties.Resources.question;
            this.bHelp3.Location = new System.Drawing.Point(213, 75);
            this.bHelp3.Name = "bHelp3";
            this.bHelp3.Size = new System.Drawing.Size(32, 23);
            this.bHelp3.TabIndex = 17;
            this.bHelp3.UseVisualStyleBackColor = true;
            this.bHelp3.Click += new System.EventHandler(this.bHelp3_Click);
            // 
            // AssembleSongDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 145);
            this.Controls.Add(this.bHelp3);
            this.Controls.Add(this.bHelp2);
            this.Controls.Add(this.bHelp1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVoicegroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTracks);
            this.Controls.Add(this.lblFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssembleSongDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assemble Song";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AssembleSongDialog_FormClosed);
            this.Load += new System.EventHandler(this.AssembleSongDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFile;
        private NumericTextBox txtTracks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NumericTextBox txtVoicegroup;
        private System.Windows.Forms.Label label3;
        private NumericTextBox txtHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bHelp1;
        private System.Windows.Forms.Button bHelp2;
        private System.Windows.Forms.Button bHelp3;

    }
}