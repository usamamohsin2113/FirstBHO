using System;
using System.Windows.Forms;

namespace FirstBHO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }
    }
}
