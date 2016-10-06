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
    public partial class SongListForm : Form
    {
        private GameInfo gameInfo;
        public SongListForm(ref GameInfo game)
        {
            InitializeComponent();
            gameInfo = game;
        }

        private void SongListForm_Load(object sender, EventArgs e)
        {
            // Build the data grid.
            // This could get slow with large lists...
            DataTable table = new DataTable("Songs");
            table.Columns.Add("Song").ReadOnly = true;
            table.Columns.Add("Name");

            for (uint i = 0; i < gameInfo.SongCount; i++)
            {
                var row = table.NewRow();
                row["Song"] = i.ToString() + " - 0x" + i.ToString("X");
                row["Name"] = gameInfo.GetSongName(i);
                table.Rows.Add(row);
            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = table;
        }

        private void SongListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Nothing to see here
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1) return;

            uint song = (uint)e.RowIndex;
            string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            
            //if (name != string.Empty && name != "")
            //{
                gameInfo.SetSongName(song, name);
            //}

            // TODO: Make this real-time?
        }
    }
}
