using System;
using System.Windows.Forms;

namespace LinearProcessamentos
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            txtSenha.Text = "";
            // The password character is an asterisk.
            txtSenha.PasswordChar = '•';
            // The control will allow no more than 14 characters.
            txtSenha.MaxLength = 10;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            dynamic SenhaLinear = Activator.CreateInstance(Type.GetTypeFromProgID("LinearSeguranca.clsInicializa"));
            
            string SenhaValidar = SenhaLinear.Licenca.SenhaLinear(DateTime.Now.ToString("dd-MM-yyyy"));
            //0029e10
            Console.WriteLine(SenhaValidar);
            if (txtSenha.Text == SenhaValidar)
            {
                
                ConfPDV.Form1 f1 = new ConfPDV.Form1(); //this is the change, code for redirect  
                f1.ShowDialog();
            }
            txtSenha.Text = "";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnEntrar_Click(sender, new EventArgs());
            }
        }
    }
}
