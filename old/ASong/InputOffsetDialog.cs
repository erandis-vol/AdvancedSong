using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASong
{
    public partial class InputOffsetDialog : Form
    {
        private bool success = false;

        public InputOffsetDialog(string text, uint offset, string caption)
        {
            InitializeComponent();

            label1.Text = text;
            this.Text = caption;
            numericTextBox1.Text = "0x" + offset.ToString("X");
        }

        private void InputOffsetDialog_Load(object sender, EventArgs e)
        {

        }

        private void InputOffsetDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (success) DialogResult = DialogResult.OK;
            else DialogResult = DialogResult.Cancel;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            success = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            success = false;
            Close();
        }

        public uint Offset
        {
            get { return (uint)numericTextBox1.Value; }
            // set { numericTextBox1.Text = "0x" + value.ToString("X"); }
        }
    }
}
