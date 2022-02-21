using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfPDV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2(); //this is the change, code for redirect  
            f2.ShowDialog();
        }



        private void recalcularHistDeMovimentaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //this.Hide();
            Form4 f4 = new Form4(); //this is the change, code for redirect  
            f4.ShowDialog();
        }

        private void importaçãoPLUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(); //this is the change, code for redirect  
            f3.ShowDialog();
        }

        private void recalcularMargemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(); //this is the change, code for redirect  
            f2.ShowDialog();
        }

        private void refazerDiárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearProcessamentos.frmRefazerDiario f5 = new LinearProcessamentos.frmRefazerDiario(); //this is the change, code for redirect  
            f5.ShowDialog();
        }
    }
}
