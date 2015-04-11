namespace ASong
{
    partial class FreeSpaceFinderDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.cFSByte = new System.Windows.Forms.ComboBox();
            this.txtSearchStart = new ASong.NumericTextBox();
            this.txtNeeded = new ASong.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.listResults = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Free Space Byte:";
            // 
            // cFSByte
            // 
            this.cFSByte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cFSByte.FormattingEnabled = true;
            this.cFSByte.Items.AddRange(new object[] {
            "00",
            "FF"});
            this.cFSByte.Location = new System.Drawing.Point(107, 45);
            this.cFSByte.Name = "cFSByte";
            this.cFSByte.Size = new System.Drawing.Size(41, 21);
            this.cFSByte.TabIndex = 1;
            this.cFSByte.SelectedIndexChanged += new System.EventHandler(this.cFSByte_SelectedIndexChanged);
            // 
            // txtSearchStart
            // 
            this.txtSearchStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchStart.Location = new System.Drawing.Point(107, 72);
            this.txtSearchStart.MaxValue = ((uint)(100663296u));
            this.txtSearchStart.Name = "txtSearchStart";
            this.txtSearchStart.NumberStyle = ASong.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtSearchStart.Size = new System.Drawing.Size(102, 20);
            this.txtSearchStart.TabIndex = 2;
            this.txtSearchStart.Text = "0x6B0000";
            this.txtSearchStart.Value = ((uint)(7012352u));
            this.txtSearchStart.TextChanged += new System.EventHandler(this.txtSearchStart_TextChanged);
            // 
            // txtNeeded
            // 
            this.txtNeeded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNeeded.Location = new System.Drawing.Point(107, 12);
            this.txtNeeded.MaxValue = ((uint)(4294967294u));
            this.txtNeeded.Name = "txtNeeded";
            this.txtNeeded.Size = new System.Drawing.Size(102, 20);
            this.txtNeeded.TabIndex = 3;
            this.txtNeeded.Text = "0";
            this.txtNeeded.Value = ((uint)(0u));
            this.txtNeeded.TextChanged += new System.EventHandler(this.txtNeeded_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Needed Bytes:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 1);
            this.panel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Search From:";
            // 
            // listResults
            // 
            this.listResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResults.FormattingEnabled = true;
            this.listResults.Location = new System.Drawing.Point(12, 98);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(197, 173);
            this.listResults.TabIndex = 7;
            this.listResults.SelectedIndexChanged += new System.EventHandler(this.listResults_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Location = new System.Drawing.Point(12, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 1);
            this.panel2.TabIndex = 9;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(134, 284);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(53, 284);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 11;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // FreeSpaceFinderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 319);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNeeded);
            this.Controls.Add(this.txtSearchStart);
            this.Controls.Add(this.cFSByte);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FreeSpaceFinderDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Free Space";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FreeSpaceFinderDialog_FormClosed);
            this.Load += new System.EventHandler(this.FreeSpaceFinderDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cFSByte;
        private NumericTextBox txtSearchStart;
        private NumericTextBox txtNeeded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listResults;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
    }
}