using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV_Reader
{

    public partial class MainForm : Form
    {
        private SeparatorType separatorType
        {
            get
            {
                if (this.cbSeparatorType.SelectedIndex == 0)
                    return SeparatorType.colon;
                else
                    return SeparatorType.comma;
            }
        }
        public MainForm()
        {
            InitializeComponent();
            this.cbSeparatorType.SelectedIndex = 0;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CSV csv = new CSV(openFileDialog.FileName, this.separatorType);
                this.gvTable.DataSource = csv.ToTable();
            }
        }

        private void cbSeparatorType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
