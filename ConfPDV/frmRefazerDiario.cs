using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace LinearProcessamentos
{
    public partial class frmRefazerDiario : Form
    {

        string mesant;
        string anoatu;
        public frmRefazerDiario()
        {
            InitializeComponent();
            //GetIniInfo(); - Atualizado 27/09: nova classe dalhelper
            ReprocEstoque.DalHelper.IniciaConexaoDal();
            connectionString = ReprocEstoque.DalHelper.connectionString;
            connection = ReprocEstoque.DalHelper.cn;
            FillCombo();
            FormatarData();

        }
        string connectionString;
        string selectedFilial;
        string dia;
        string mes;
        string ano;
        string mesAno;
        string americandate;

        string diaFinal;
        string mesFinal;
        string anoFinal;
        string mesAnoFinal;
        string americandateFinal;
        MySqlConnection connection;
        private void btnProcessar_Click(object sender, EventArgs e)
        {

            btnProcessar.Enabled = false;
            BtnClickVariableValues();
            CriarTabela();
            InserirDtCad();
            if (cbxProduto.Checked && !cbxApartirDeProd.Checked)
            {
                int codprod = int.Parse(txtCodProd.Text);

                RefazerDiario(codprod);
            }
            else if (!cbxProduto.Checked && !cbxApartirDeProd.Checked)
            {
                GetAllProds();
            }
            else if (cbxApartirDeProd.Checked)
            {
                GetAfterProd(txtProdutoInicio.Text.ToString());

            }
            RemoverDtCad();
            btnProcessar.Enabled = true;
            MessageBox.Show("Processamento Finalizado");
        }

        void RefazerDiario(int codprod)
        {
            int quantInsert = 200;
            string ArmazInsert = "";
            int diaatual = int.Parse(dia);

            string americandateatual = "";
            string americandateanterior = "";
            while (diaatual <= int.Parse(diaFinal))
            {
                int diaanterior = diaatual - 1;
                string diaanteriorS = diaanterior.ToString();
                string diaatualS = diaatual.ToString();

                if (diaanterior.ToString().Length == 1)
                {
                    diaanteriorS = "0" + diaanterior.ToString();
                }

                if (diaatual.ToString().Length == 1)
                {
                    diaatualS = "0" + diaatual.ToString();
                }


                americandateatual = "20" + ano + "-" + mes + "-" + diaatualS;
                if (diaanteriorS == "00")
                {
                    americandateanterior = "20" + anoatu + "-" + mesant + "-" + DateTime.DaysInMonth(int.Parse("20" + ano), int.Parse(mesant)).ToString();
                }
                else
                {
                    americandateanterior = "20" + ano + "-" + mes + "-" + diaanteriorS;
                }
                ApagarDiario(codprod, americandateatual);
                WriteLog("Checando: " + americandateatual);
                lblProgress.Text = "Processando produto " + codprod + " - "+americandateatual;
                Application.DoEvents();
                if (!DiarioExist(americandateatual, codprod))
                {
                    WriteLog("Não existe diário: " + americandateatual);


                    connection = new MySqlConnection(connectionString);


                    //try
                    //{
                    connection.Open();
                    string sql;
                    MySqlCommand cmd;
                    bool addedparam = false;
                    if (americandateatual.Substring(8, 2) == "01")
                    {
                        //   MessageBox.Show("Sou 01");

                        sql = " INSERT INTO diario" + mesAno + " " +

                                    " SELECT d.es1_cod, d.es1_empresa, @dataatual AS DATA, case when s.es2_qatu IS NULL then (case when (SELECT es2_qatu FROM diario" + mesant + anoatu + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) IS NULL then 0.000 ELSE (SELECT es2_qatu FROM diario" + mesant + anoatu + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) END) ELSE s.es2_qatu END AS es2_qatu, d.es1_prcompra, d.es1_prcusto, d.es1_prvarejo, d.reservado1, d.reservado2, d.reservado3, d.es1_ativo, d.es1_suspenso FROM diario" + mesant + anoatu + " d LEFT JOIN(SELECT* FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE es1_cod = @codprod and DATA <= @dataatual UNION ALL SELECT* FROM mov" + selectedFilial + "estoque" + mesant + anoatu + " WHERE es1_cod = @codprod and DATA <= @dataatual ORDER BY cod DESC LIMIT 1) s ON d.es1_cod = s.es1_cod AND d.es1_empresa = s.es1_empresa WHERE d.es1_cod = @codprod AND d.data = @dataanterior and d.es1_empresa = @empresa;";
                        cmd = new MySqlCommand(sql, connection);
                        cmd.CommandTimeout = 99999;

                    }
                    else
                    {
                        sql = " SELECT d.es1_cod, d.es1_empresa, @dataatual AS DATA, case when s.es2_qatu IS NULL then (case when (SELECT es2_qatu FROM diario" + mesAno + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) IS NULL then 0.000 ELSE (SELECT es2_qatu FROM diario" + mesAno + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) END) ELSE s.es2_qatu END AS es2_qatu, d.es1_prcompra, d.es1_prcusto, d.es1_prvarejo, d.reservado1, d.reservado2, d.reservado3, d.es1_ativo, d.es1_suspenso FROM diario" + mesAno + " d LEFT JOIN(SELECT* FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE es1_cod = @codprod and DATA <= @dataatual  ORDER BY cod DESC LIMIT 1) s ON d.es1_cod = s.es1_cod AND d.es1_empresa = s.es1_empresa WHERE d.es1_cod = @codprod AND d.data = @dataanterior and d.es1_empresa = @empresa ;";
                        cmd = new MySqlCommand(sql, connection);
                        cmd.CommandTimeout = 99999;
                        cmd.Parameters.AddWithValue("@dataatual", americandateatual);
                        cmd.Parameters.AddWithValue("@dataanterior", americandateanterior);
                        cmd.Parameters.AddWithValue("@codprod", codprod);
                        cmd.Parameters.AddWithValue("@empresa", selectedFilial);

                        addedparam = true;
                        cmd.Prepare();
                        MySqlDataReader myreader;
                        myreader = cmd.ExecuteReader();



                        if (myreader.Read() == true)
                        {
                            connection.Close();
                            sql = " INSERT INTO diario" + mesAno +
                                " SELECT d.es1_cod, d.es1_empresa, @dataatual AS DATA, case when s.es2_qatu IS NULL then (case when (SELECT es2_qatu FROM diario" + mesAno + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) IS NULL then 0.000 ELSE (SELECT es2_qatu FROM diario" + mesAno + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) END) ELSE s.es2_qatu END AS es2_qatu, d.es1_prcompra, d.es1_prcusto, d.es1_prvarejo, d.reservado1, d.reservado2, d.reservado3, d.es1_ativo, d.es1_suspenso FROM diario" + mesAno + " d LEFT JOIN(SELECT* FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE es1_cod = @codprod and DATA <= @dataatual  ORDER BY cod DESC LIMIT 1) s ON d.es1_cod = s.es1_cod AND d.es1_empresa = s.es1_empresa WHERE d.es1_cod = @codprod AND d.data = @dataanterior and d.es1_empresa = @empresa ;";
                            connection = new MySqlConnection(connectionString);
                            //   MessageBox.Show(sql);


                            connection.Open();

                            cmd = new MySqlCommand(sql, connection);
                            cmd.CommandTimeout = 99999;
                            cmd.Parameters.AddWithValue("@dataatual", americandateatual);
                            cmd.Parameters.AddWithValue("@dataatual2", americandateatual);
                            cmd.Parameters.AddWithValue("@dataanterior", americandateanterior);
                            cmd.Parameters.AddWithValue("@codprod", codprod);
                            cmd.Parameters.AddWithValue("@codprod2", codprod);
                            cmd.Parameters.AddWithValue("@empresa", selectedFilial);
                            cmd.Parameters.AddWithValue("@empresa2", selectedFilial);
                            cmd.Prepare();
                            lblProcessados.Text =(int.Parse(lblProcessados.Text) + cmd.ExecuteNonQuery()).ToString();
                            WriteLog("Reader encontrado, realizando insert:\n "+ cmd.CommandText);
                            connection.Close();
                            
                        }
                        else
                        {
                            connection.Close();
                            sql = "INSERT INTO diario" + mesAno + " " +
                                " SELECT d.es1_cod, d.es1_empresa, @dataatual AS DATA, case when s.es2_qatu IS NULL then (case when (SELECT es2_qatu FROM diario" + mesant + anoatu + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) IS NULL then 0.000 ELSE (SELECT es2_qatu FROM diario" + mesant + anoatu + " WHERE es1_cod = @codprod and DATA < @dataatual ORDER BY data DESC LIMIT 1) END) ELSE s.es2_qatu END AS es2_qatu, d.es1_prcompra, d.es1_prcusto, d.es1_prvarejo, d.reservado1, d.reservado2, d.reservado3, d.es1_ativo, d.es1_suspenso FROM diario" + mesAno + " d LEFT JOIN(SELECT* FROM mov" + selectedFilial + "estoque" + mesant + anoatu + " WHERE es1_cod = @codprod and DATA <= @dataatual  ORDER BY cod DESC LIMIT 1) s ON d.es1_cod = s.es1_cod AND d.es1_empresa = s.es1_empresa WHERE d.es1_cod = @codprod AND d.data = @dataanterior and d.es1_empresa = @empresa ;";
                            //cmd.Prepare();
                            connection = new MySqlConnection(connectionString);
                            connection.Open();
                            cmd = new MySqlCommand(sql, connection);
                            cmd.CommandTimeout = 99999;
                            cmd.Parameters.AddWithValue("@dataatual", americandateatual);
                            cmd.Parameters.AddWithValue("@dataanterior", americandateanterior);
                            cmd.Parameters.AddWithValue("@codprod", codprod);
                            cmd.Parameters.AddWithValue("@empresa", selectedFilial);
                            cmd.Prepare();
                            lblProcessados.Text = (int.Parse(lblProcessados.Text) + cmd.ExecuteNonQuery()).ToString();
                            WriteLog("Reader não encontrado, realizando insert");
                            connection.Close();

                        }
                    }



                    if (!addedparam)
                    {
                        cmd.Parameters.AddWithValue("@dataatual", americandateatual);
                        cmd.Parameters.AddWithValue("@dataanterior", americandateanterior);
                        cmd.Parameters.AddWithValue("@codprod", codprod);
                        cmd.Parameters.AddWithValue("@empresa", selectedFilial);
                        cmd.Prepare();
                        cmd.ExecuteScalar();
                        connection.Close();
                    }

                    connection.Open();

                    sql = "SELECT * FROM fechamento where data = @dataatual;";
                    cmd = new MySqlCommand(sql, connection);
                    cmd.CommandTimeout = 99999;
                    cmd.Parameters.AddWithValue("@dataatual", americandateatual);
                    cmd.Prepare();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        connection.Close();
                        connection = new MySqlConnection(connectionString);
                        connection.Open();
                        sql = "INSERT IGNORE INTO fechamento (data, usuario, inicio, termino) VALUES ( @dataatual , 9999, \"00:00:01\", \"00:00:01\");";
                        cmd = new MySqlCommand(sql, connection);
                        cmd.CommandTimeout = 99999;
                        cmd.Parameters.AddWithValue("@dataatual", americandateatual);
                        cmd.Prepare();
                        cmd.ExecuteScalar();
                        WriteLog("Inserindo tabela fechamento");
                        connection.Close();
                    }
                    WriteLog("Inserindo auditoria");
                    RotinasGerais.Auditoria("LinearProcessamentos", "RefazerDiario", "Processar fechamentos diários faltantes, data: " + dtpDataInicial.Text + " a " + dtpDataFinal.Text, "", int.Parse(selectedFilial));




                }
                else
                {
                    //MessageBox.Show("Existe diário");
                }

                diaatual++;
                Application.DoEvents();
            }
            pgbProgress.Value++;
        }

        void GetAllProds()
        {
            int lastcodprod = 0;
            try
            {
                connection = new MySqlConnection(connectionString);
                string Query = "SELECT es1_cod FROM es1 WHERE es1_empresa = " + selectedFilial + " GROUP BY es1_cod order by es1_cod;";
                MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                cmdDataBase.CommandTimeout = 99999;
                MySqlDataReader myreader;

                connection.Open();
                myreader = cmdDataBase.ExecuteReader();
                while (myreader.Read())
                {

                    string es1_cod = myreader.GetString("es1_cod");
                    int codprod = int.Parse(es1_cod);

                    WriteLog("Produto: " + es1_cod);
                    RefazerDiario(codprod);
                    lastcodprod = codprod;

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                bool tryagain = true;
                int maxtry = 6;
                int trycount = 0;
                WriteLog("Erro 1, tentando novamente..");

                while (tryagain == true && trycount < maxtry)
                {
                    System.Threading.Thread.Sleep(2000);
                    try
                    {
                        connection = new MySqlConnection(connectionString);
                        string Query = "SELECT es1_cod FROM es1 WHERE es1_empresa = " + selectedFilial + " AND es1_cod >= " + lastcodprod + " GROUP BY es1_cod order by es1_cod;";
                        MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                        cmdDataBase.CommandTimeout = 99999;
                        MySqlDataReader myreader;

                        connection.Open();
                        myreader = cmdDataBase.ExecuteReader();
                        while (myreader.Read())
                        {

                            string es1_cod = myreader.GetString("es1_cod");
                            int codprod = int.Parse(es1_cod);
                            WriteLog("Produto: " + es1_cod);
                            RefazerDiario(codprod);
                            lastcodprod = codprod;
                            tryagain = false;
                        }
                        connection.Close();
                    }
                    catch (Exception ex2)
                    {

                        trycount++;
                        WriteLog("Exception tentativa" + trycount + ": " + ex2);
                        if (trycount > 6)
                        {
                            WriteLog("Exception tentativa 6: " + ex2);
                            MessageBox.Show("Exception tentativa 6: " + ex2);
                            Application.Exit();
                        }
                        tryagain = true;
                    }
                }
            }




        }

        void GetIniInfo()
        {
            try
            {
                string iniserv_provider;
                string iniserv_nomebanco;

                ReprocEstoque.IniFile MyReader = new ReprocEstoque.IniFile("sglinx.ini");

                iniserv_provider = MyReader.Read("provider", "SETTINGS").ToString();

                iniserv_nomebanco = MyReader.Read("nomebanco", "SETTINGS").ToString();


                if (Environment.MachineName.ToString() == "MATHEUS-DESENV")
                {
                    connectionString = "Server=localhost;Database=sglinx;Uid=adminlinear;Pwd=@2013linear;" +
                                        "Integrated Security=SSPI; Pooling=false;";
                }
                else
                {
                    connectionString = "Server=" + iniserv_provider + ";Database=" + iniserv_nomebanco + ";Uid=adminlinear;Pwd=@2013linear;" +
                                        "Integrated Security=SSPI; Pooling=false;";
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }//Recebe as informações do ini para montar a string de conexão

        void FillCombo()
        {
            connection = new MySqlConnection(connectionString);
            string Query = "SELECT CONCAT(LPAD(emp_codigo,3,0), ' - ',empresa.emp_razao) AS emp_codigo FROM empresa where emp_ativa = 1 order by emp_codigo";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
            MySqlDataReader myreader;

            string Query2 = "SELECT COUNT(emp_codigo) FROM empresa WHERE emp_ativa = 1";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query2, connection);

            string countFIlial = "";

            try
            {
                connection.Open();
                myreader = cmdDataBase.ExecuteReader();

                while (myreader.Read())
                {
                    string sFilial = myreader.GetString("emp_codigo");
                    cbxFilial.Items.Add(sFilial);
                }
                connection.Close();
                myreader.Dispose();


            }
            catch (Exception ex)
            {
                // WriteLog("Erro ao encontrar conexão com o banco de dados, verifique a chave [SETTINGS] no arquivo sglinx.ini e os serviços do MySQL :" + ex.Message);
                MessageBox.Show("Erro ao encontrar conexão com o banco de dados. \nVerifique a chave [SETTINGS] no arquivo sglinx.ini \n" + ex.Message, "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            try
            {
                connection.Open();
                myreader = cmdDataBase2.ExecuteReader();

                while (myreader.Read())
                {
                    countFIlial = myreader.GetString("COUNT(emp_codigo)");

                }
                connection.Close();
                myreader.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na rotina FillCombo(), COUNT(emp_codigo)" + ex.Message, "Erro de rotina", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (int.Parse(countFIlial) == 1)
            {
                cbxFilial.Enabled = false;
                cbxFilial.SelectedIndex = 0;
                //    monoloja = true;
            }
            //cbxFilial.SelectedIndex = 0;

        }
        private void FormatarData()
        {
            dtpDataInicial.Format = DateTimePickerFormat.Custom;
            dtpDataInicial.CustomFormat = "dd/MM/yyyy";
            dtpDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");


            dtpDataFinal.Format = DateTimePickerFormat.Custom;
            dtpDataFinal.CustomFormat = "dd/MM/yyyy";
            dtpDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }//Formata a data e insere o valor do dia

        void BtnClickVariableValues()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Int64 result;
            string sql = "select count(es1_cod) from es1";
            if (cbxApartirDeProd.Checked)
            {
                sql = "select count(es1_cod) from es1 where es1_cod >= " + txtProdutoInicio.Text;
            }
            else if (cbxProduto.Checked)
            {
                sql = "select count(es1_cod) from es1 where es1_cod = " + txtCodProd.Text;
            }
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandTimeout = 99999;
            result = (Int64)cmd.ExecuteScalar();
            pgbProgress.Maximum = (int)result;
            pgbProgress.Value = 0;
            connection.Close();
            selectedFilial = cbxFilial.SelectedItem.ToString().Substring(0, 3);
            dia = dtpDataInicial.Text.Substring(0, 2);
            mes = dtpDataInicial.Text.Substring(3, 2);
            ano = dtpDataInicial.Text.Substring(8, 2);
            americandate = dtpDataInicial.Text.Substring(6, 4) + "-" + dtpDataInicial.Text.Substring(3, 2) + "-" + dtpDataInicial.Text.Substring(0, 2);
            mesAno = mes + ano;



            diaFinal = dtpDataFinal.Text.Substring(0, 2);
            mesFinal = dtpDataFinal.Text.Substring(3, 2);
            anoFinal = dtpDataFinal.Text.Substring(8, 2);
            americandateFinal = dtpDataFinal.Text.Substring(6, 4) + "-" + dtpDataFinal.Text.Substring(3, 2) + "-" + dtpDataFinal.Text.Substring(0, 2);
            mesAnoFinal = mesFinal + anoFinal;


            Decimal.TryParse(mes, out decimal mesanterior);

            anoatu = ano;

            mesanterior = mesanterior - 1;
            //  adicionar um 0 ao valor do mês caso tenha apenas um digito
            if (mesanterior.ToString().Length == 1)
            {
                mesant = "0" + mesanterior.ToString();
            }
            else
            {
                mesant = mesanterior.ToString();

            }

            //alterar o valor do ano anterior corretamente caso seja o mês seja 01
            if (mesant == "00")
            {
                mesant = "12";
                int tempano = int.Parse(ano) - 1;

                //adicionar 0 a esquerda caso o tenha apenas um digito
                if (tempano.ToString().Length == 1)
                {
                    anoatu = "0" + tempano.ToString();
                }
                else
                {
                    anoatu = tempano.ToString();
                }
            }//Determina os valores das variaveis após clicar no botão de processar  




        }

        bool DiarioExist(string americandateatual, int codprod)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT data FROM diario" + mesAno + " WHERE es1_cod = @codprod and data = @data and es1_empresa = @empresa LIMIT 1";




            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandTimeout = 99999;
            MySqlDataReader myreader;
            cmd.Parameters.AddWithValue("@codprod", codprod);
            cmd.Parameters.AddWithValue("@data", americandateatual);
            cmd.Parameters.AddWithValue("@empresa", selectedFilial);
            cmd.Prepare();
            myreader = cmd.ExecuteReader();


            if (myreader.Read() == true)
            {
                //   MessageBox.Show(myreader.GetString("data"));
                connection.Close();
                return true;
            }
            else
            {
                //  MessageBox.Show("Reader não encontrado");
                connection.Close();
                return false;
            }


        }

        private void cbxProduto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxProduto.Checked == true)
            {
                cbxApartirDeProd.Checked = false;
                txtCodProd.Enabled = true;
            }
            else if (cbxProduto.Checked == false)
            {
                txtCodProd.Enabled = false;
            }
        }

        private void txtCodProd_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodProd.Text))
            {
                txtProdDesc.Clear();
                return;
            }

            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = @"SELECT es1_desc 
                     FROM es1 e 
                     JOIN es1p p 
                       ON e.es1_cod = p.es1_cod 
                    WHERE e.es1_cod = @code";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@code", txtCodProd.Text);
            cmd.Prepare();
            var result = (string)cmd.ExecuteScalar();
            if (result == null)
            {
                txtProdDesc.Clear();
            }
            else
            {
                txtProdDesc.Text = (string)result;
            }
            connection.Close();
        }

        void WriteLog(string text)
        {
            text = DateTime.Now.ToString("dd/MM/yyyy") + " - " + DateTime.Now.ToString("HH:mm:ss") + "= " + text;
            string path = "LogRefazerDiario.log";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(text);

                }
            }
            else if (File.Exists(path))
            {
                using (var sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(text);
                }
            }


        }

        void ApagarDiario(int codprod, string dataatual)
        {

            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "DELETE from diario" + mesAno + " WHERE es1_cod = @codprod AND DATA = @dataatual AND es1_empresa = @empresa ;";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@codprod", codprod);
            cmd.Parameters.AddWithValue("@dataatual", dataatual);
            cmd.Parameters.AddWithValue("@empresa", selectedFilial);
            cmd.Prepare();
            cmd.ExecuteScalar();
            WriteLog("Removido registro: produto: " + codprod + " data = " + dataatual + " empresa: " + selectedFilial);

            connection.Close();
        }

        void CriarTabela()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "CREATE TABLE if not exists diario" + mesAno + " LIKE diario" + mesant + anoatu + ";";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Prepare();
            cmd.ExecuteScalar();
            connection.Close();

        }

        void GetAfterProd(string codprodinicio)

        {
            int lastcodprod = 0;
            try
            {
                connection = new MySqlConnection(connectionString);
                string Query = "SELECT es1_cod FROM es1 WHERE es1_empresa = " + selectedFilial + " AND es1_cod >=" + codprodinicio + " GROUP BY es1_cod order by es1_cod;";
                MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                cmdDataBase.CommandTimeout = 99999;
                MySqlDataReader myreader;

                connection.Open();
                myreader = cmdDataBase.ExecuteReader();
                while (myreader.Read())
                {

                    string es1_cod = myreader.GetString("es1_cod");
                    int codprod = int.Parse(es1_cod);

                    WriteLog("Produto: " + es1_cod);
                    RefazerDiario(codprod);
                    lastcodprod = codprod;

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                bool tryagain = true;
                int maxtry = 6;
                int trycount = 0;
                WriteLog("Erro 1, tentando novamente..  " + ex);

                while (tryagain == true && trycount < maxtry)
                {
                    System.Threading.Thread.Sleep(2000);
                    try
                    {
                        connection = new MySqlConnection(connectionString);
                        string Query = "";
                        if (lastcodprod == 0)
                        {
                            Query = "SELECT es1_cod FROM es1 WHERE es1_empresa = " + selectedFilial + " AND es1_cod >= " + codprodinicio + " GROUP BY es1_cod order by es1_cod;";

                        }
                        else
                        {
                            Query = "SELECT es1_cod FROM es1 WHERE es1_empresa = " + selectedFilial + " AND es1_cod >= " + lastcodprod + " GROUP BY es1_cod order by es1_cod;";
                        }

                        MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                        cmdDataBase.CommandTimeout = 99999;
                        MySqlDataReader myreader;

                        connection.Open();
                        myreader = cmdDataBase.ExecuteReader();
                        while (myreader.Read())
                        {

                            string es1_cod = myreader.GetString("es1_cod");
                            int codprod = int.Parse(es1_cod);
                            WriteLog("Produto: " + es1_cod);
                            RefazerDiario(codprod);
                            lastcodprod = codprod;
                            tryagain = false;
                        }
                        connection.Close();
                    }
                    catch (Exception ex2)
                    {

                        trycount++;
                        WriteLog("Exception tentativa" + trycount + ": " + ex2);
                        if (trycount > 6)
                        {
                            WriteLog("Exception tentativa 6: " + ex2);
                            MessageBox.Show("Exception tentativa 6: " + ex2);
                            Application.Exit();
                        }
                        tryagain = true;
                    }
                }

            }
        }

        private void cbxApartirDeProd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxApartirDeProd.Checked == true)
            {
                cbxProduto.Checked = false;
                txtProdutoInicio.Enabled = true;
            }
            else if (cbxApartirDeProd.Checked == false)
            {
                txtProdutoInicio.Enabled = false;
            }
        }
        string americandiaanterior;

        int CountExDiario;
        void InserirDtCad()
        {
            WriteLog("Inserindo registros de produtos cadastrados entre as datas...");
            try
            {
                string sql;
                if (dia != "01")
                {
                    string diaantes = (int.Parse(dtpDataInicial.Text.Substring(0, 2)) - 1).ToString();
                    if (diaantes.Length == 1)
                    {
                        diaantes = "0" + diaantes;
                    }

                    americandiaanterior = dtpDataInicial.Text.Substring(6, 4) + "-" + dtpDataInicial.Text.Substring(3, 2) + "-" + diaantes;
                    sql = "INSERT INTO diario" + mesAno + " select es1_cod, es1_empresa, @americandateanterior AS DATA, 0 AS es2_qatu, es1_prcompra, es1_prcusto, es1_prvarejo, 0.000 AS reservado1, \"\" AS reservado2, 0 AS reservado3, es1_ativo, es1_suspenso FROM es1 WHERE es1.ES1_DTCAD BETWEEN @dtini and @dtfin and es1.es1_empresa = @empresa ";
                }
                else
                {
                    string diaantes = DateTime.DaysInMonth(int.Parse("20" + anoatu), int.Parse(mesant)).ToString();
                    americandiaanterior = dtpDataInicial.Text.Substring(6, 4) + "-" + mesant + "-" + diaantes;

                    sql = "INSERT INTO diario" + mesant + anoatu + " select es1_cod, es1_empresa, @americandateanterior AS DATA, 0 AS es2_qatu, es1_prcompra, es1_prcusto, es1_prvarejo, 0.000 AS reservado1, \"\" AS reservado2, 0 AS reservado3, es1_ativo, es1_suspenso FROM es1 WHERE es1.ES1_DTCAD BETWEEN @dtini and @dtfin and es1.es1_empresa = @empresa ";
                }

                if (cbxProduto.Checked)
                {
                    sql += "and es1.es1_cod = " + txtCodProd.Text + " ;";
                }
                else
                {
                    sql += " ;";
                }

                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@americandateanterior", americandiaanterior);
                cmd.Parameters.AddWithValue("@dtini", americandate);
                cmd.Parameters.AddWithValue("@dtfin", americandateFinal);
                cmd.Parameters.AddWithValue("@empresa", selectedFilial);
                cmd.Prepare();
                cmd.ExecuteScalar();
                connection.Close();
                WriteLog("Registros inseridos com sucesso");
            }
            catch (Exception ex)
            {
                if (CountExDiario >= 1)
                {
                    MessageBox.Show("Erro ao inserir registro de datas não cadastradas: " + ex);
                }
                WriteLog("Erro ao inserir registro de datas não cadastradas: " + ex);
                CountExDiario++;
            }
        }

        void RemoverDtCad()

        {
            WriteLog("Removendo registros desnecessários...");
            try
            {
                if (dia != "01")
                {
                    string sql = "DELETE d FROM diario" + mesAno + " d INNER JOIN es1 ON d.es1_cod = es1.es1_cod and d.es1_empresa = es1.es1_empresa WHERE DATA < ES1_DTCAD and es1.es1_empresa = @empresa ";
                    if (cbxProduto.Checked)
                    {
                        sql += "and es1.es1_cod = " + txtCodProd.Text + " ;";
                    }
                    else
                    {
                        sql += " ;";
                    }

                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    //     cmd.Parameters.AddWithValue("@americandateanterior", americandiaanterior);
                    //     cmd.Parameters.AddWithValue("@dtini", americandate);
                    //     cmd.Parameters.AddWithValue("@dtfin", americandateFinal);
                    cmd.Parameters.AddWithValue("@empresa", selectedFilial);
                    cmd.Prepare();
                    cmd.ExecuteScalar();
                    connection.Close();
                    WriteLog("Registros desnecessários removidos com sucesso");
                }
                else
                {
                    int count = 0;
                    string sql;
                    while (count <= 1)
                    {
                        if (count == 0)
                        {
                            sql = "DELETE d FROM diario" + mesant + anoatu + " d INNER JOIN es1 ON d.es1_cod = es1.es1_cod and d.es1_empresa = es1.es1_empresa WHERE DATA < ES1_DTCAD and es1.es1_empresa = @empresa and d.data = @americandiaanterior ";
                        }
                        else
                        {
                            sql = "DELETE d FROM diario" + mesAno + " d INNER JOIN es1 ON d.es1_cod = es1.es1_cod and d.es1_empresa = es1.es1_empresa WHERE DATA < ES1_DTCAD and es1.es1_empresa = @empresa ";
                        }

                        if (cbxProduto.Checked)
                        {
                            sql += "and es1.es1_cod = " + txtCodProd.Text + " ;";
                        }
                        else
                        {
                            sql += " ;";
                        }
                        connection = new MySqlConnection(connectionString);
                        connection.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@americandiaanterior", americandiaanterior);
                        cmd.Parameters.AddWithValue("@dtini", americandate);
                        cmd.Parameters.AddWithValue("@dtfin", americandateFinal);
                        cmd.Parameters.AddWithValue("@empresa", selectedFilial);
                        cmd.Prepare();
                        cmd.ExecuteScalar();
                        connection.Close();
                        count++;
                    }
                    WriteLog("Registros desnecessários removidos com sucesso");

                }
            }
            catch (Exception ex)
            {
                WriteLog("Erro ao remover registros desnecessários: " + ex);
                MessageBox.Show("Erro ao remover registros desnecessários: " + ex);
            }

        }
    }

}
