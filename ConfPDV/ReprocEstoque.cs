using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;




namespace ConfPDV
{


    public partial class Form4 : Form
    {
        string dia;
        string mes;
        string mesant;
        string ano;
        string mesAno;
        string anoatu;

        string americandate;
        MySqlConnection connection;

        //  MySqlCommand command;
        //      MySqlDataAdapter da;
        //    MySqlDataReader dr;
        //       string strSQL;
        public int CountUpdate = 0;
        public Form4()
        {

            InitializeComponent();
            //GetIniInfo(); - Atualizado 27/09: nova classe dalhelper
            ReprocEstoque.DalHelper.IniciaConexaoDal();
            connectionString= ReprocEstoque.DalHelper.connectionString;
            //     mtxtData.Mask = "00/00/0000";
            //    txtCodProd.Maximum = 300000000;
            mtxtData.Format = DateTimePickerFormat.Custom;
            mtxtData.CustomFormat = "dd/MM/yyyy";
            FillCombo();
            GetDate();
            


        }
        static string iniserv_provider;
        static string iniserv_nomebanco;
        string connectionString;
        // = "Server="+iniserv_provider+";Database="+iniserv_nomebanco+";Uid=adminlinear;Pwd=@2013linear;" +
        //                             "Integrated Security=SSPI; Pooling=false;";





        void Genaratelbl(string codprod)
        {
            WriteLog("Inserindo auditoria");
            LinearProcessamentos.RotinasGerais.Auditoria("LinearProcessamentos", "RefazerMovimentacaoEstoque", "Processar movimentação de estoque incorreta, data: " + mtxtData.Text + " produto: "+codprod, "", int.Parse(selectedFilial));

            WriteScreenLog("Analisando produto: " + codprod);
           
            int prodatualizados = 0;
            pgbProcesso.Step = 1;
            pgbProcesso.Value = 0;  
            Decimal.TryParse(mes, out decimal mesanterior);

            anoatu = ano;

            mesanterior = mesanterior - 1;
            //  adicionar um 0 ao valor do mês caso tenha apenas um digito
            if (mesanterior.ToString().Length == 1)
            {
                mesant = "0" + mesanterior.ToString();
                //   MessageBox.Show(mesant + "if");
            }
            else
            {
                mesant = mesanterior.ToString();
                //    MessageBox.Show(mesant +"else");
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

            }
            decimal countregister = 0;
            const string quote = "\"";
            connection = new MySqlConnection(connectionString);
            //contagem de registros para o While (se o dia for 1 ele soma com um registro do mês aterior)
            string Query5;
            
            if (dia == "01")
            {                                                                                                 //Alterado em 30/04 -- não testado " LIKE "+"mov"+selectedFilial+"estoque"+mesAno+" ; "+//
                Query5 = "CREATE TABLE IF NOT EXISTS `mov" + selectedFilial + "estoque" + mesant + anoatu + "` (`cod` int(10) NOT NULL AUTO_INCREMENT,`es1_cod` mediumint(6) NOT NULL DEFAULT '0',`data` date DEFAULT NULL,`hora` time DEFAULT NULL,`quant` decimal(16,3) NOT NULL DEFAULT '0.000',`hist` varchar(255) DEFAULT NULL,`prcusto` decimal(16,3) NOT NULL DEFAULT '0.000',`prcompra` decimal(16,3) NOT NULL DEFAULT '0.000',`prvarejo` decimal(16,3) NOT NULL DEFAULT '0.000',`tipo` char(1) DEFAULT NULL,`modulo` varchar(50) DEFAULT NULL,`es2_qatu` decimal(16,3) NOT NULL DEFAULT '0.000',`status` tinyint(1) NOT NULL DEFAULT '1',`es1_empresa` smallint(3) NOT NULL DEFAULT '1',`anterior` decimal(16,3) NOT NULL DEFAULT '0.000',PRIMARY KEY (`cod`),KEY `newindex` (`es1_cod`,`data`,`tipo`)) AUTO_INCREMENT=36347 DEFAULT CHARSET=LATIN1; " +
                    " SELECT" +
                                " (SELECT CAST(COUNT(cod) AS DECIMAL)"+
                                " FROM mov"+selectedFilial+"estoque"+mesant+anoatu+
                                " WHERE es1_cod = "+codprod.ToString()+
                                " LIMIT 1) +("+
                                "SELECT CAST(COUNT(cod) AS DECIMAL)"+
                                " FROM mov"+selectedFilial+"estoque"+mesAno+
                                " WHERE DATA >= "+americandate+" AND es1_cod = "+codprod.ToString()+") AS contagem";
            }
            else
            {
                Query5 = "SELECT CAST(COUNT(cod) AS DECIMAL) as contagem FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE data >= " + quote + "" + americandate + "" + quote + " and es1_cod = " + codprod.ToString();
            }
            connection = new MySqlConnection(connectionString);
            MySqlCommand cmdDataBase5 = new MySqlCommand(Query5, connection);
            MySqlDataReader myreader;

            connection.Open();
            myreader = cmdDataBase5.ExecuteReader();


            while (myreader.Read())
            {

                string contagem = myreader.GetString("contagem");
                countregister = Decimal.Parse(contagem);

                //calculo do máximo para a progress bar
                int maxcount = int.Parse(contagem);
                pgbProcesso.Maximum = maxcount;

            }
            connection.Close();



            int count = 0;

            decimal savedest1 = 0;
            string cod1 = "0";
            string cod2 = "0";

            decimal total = 0;
            lblProcessando.Text = "Processando...";
            while (count <= countregister)
            {

              //  Object selectedFilial = cbxFilial.SelectedItem;
                
                string Query;

                if (count == 0 && dia == "01")
                {

                    Query =  "SELECT cod, es1_cod, data, hora, quant, tipo, es2_qatu FROM mov" + selectedFilial + "estoque" + mesant + anoatu + " WHERE es1_cod =" + codprod.ToString() + " order BY cod desc limit 1;";

                }
                else
                {
                    Query = "SELECT cod, es1_cod, data, hora, quant, tipo, es2_qatu FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE data >= " + quote + "" + americandate + "" + quote + " and es1_cod =" + codprod.ToString() + " and cod > " + cod1.ToString() + " order BY cod limit 1;";
                }
                // string Query = "SELECT cod, es1_cod, data, hora, quant, tipo, es2_qatu FROM mov" + cbxFilial.SelectedItem.ToString() + "estoque0221 WHERE modulo <> 'ATUEST' and data >= '" + txtData.Text.ToString() + "' and es1_cod =" + codprod.ToString() + " and cod > " + cod1.ToString() + " order BY cod limit 1;";
                
                //   MySqlDataReader myreader;

                //    try
                //   {
                connection = new MySqlConnection(connectionString);
                MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                connection.Open();
                myreader = cmdDataBase.ExecuteReader();

                while (myreader.Read())
                {
                    string cod = myreader.GetString("cod");

                    //buscar do primeiro registro da tabela caso sejam meses diferentes
                    if (count == 0 && dia == "01")
                    { cod1 = "0"; }
                    else { cod1 = cod; }

                    // se a quantidade de registros for igual 1 ele vai puxar o primeiro registro no segundo select também
                    if (countregister == 1)
                    {
                        cod1 = "0";
                    }

                    string data1 = myreader.GetString("data");
                    string hora1 = myreader.GetString("hora");
                    string quant1 = myreader.GetString("quant");
                    string tipo = myreader.GetString("tipo");
                    string est1 = myreader.GetString("es2_qatu");
                    savedest1 = Decimal.Parse(est1);





                }
                connection.Close();
                //   }
                //   catch (Exception ex)
                //   {
                //       MessageBox.Show("Get first info: " + ex.Message);
                //    }

                
                //   string Query2 = "SELECT cod, es1_cod, data, hora, quant, tipo, es2_qatu FROM mov" + cbxFilial.SelectedItem + "estoque0221 WHERE modulo <> "+quote+"ATUEST"+quote+" and data >= " + txtData.Text + " and es1_cod =" + codprod + " and cod > " + cod1 + " order BY cod limit 1;";
                string Query2 = "SELECT cod, es1_cod, data, hora, quant, tipo, es2_qatu, modulo FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE data >= " + quote + "" + americandate + "" + quote + " and es1_cod =" + codprod + " and cod > " + cod1 + " order BY cod limit 1;";
                                                                                                                                                        
                MySqlCommand cmdDataBase2 = new MySqlCommand(Query2, connection);

                connection.Open();
                myreader = cmdDataBase2.ExecuteReader();

                while (myreader.Read())
                {
                    string cod = myreader.GetString("cod");
                    cod2 = cod;

                    string data2 = myreader.GetString("data");
                    string hora2 = myreader.GetString("hora");
                    string quant2 = myreader.GetString("quant");
                    string tipo = myreader.GetString("tipo");
                    string est = myreader.GetString("es2_qatu");
                    string modulo = myreader.GetString("modulo");
                    decimal est2 = Decimal.Parse(est);



                    if (tipo == "S" && modulo != "ATUEST" && modulo != "ES7")
                    {

                        total = savedest1 - Decimal.Parse(quant2);
                    }
                    else if (tipo == "E" && modulo != "ATUEST" && modulo != "ES7")
                    {

                        total = savedest1 + Decimal.Parse(quant2);
                    }
                    else if (modulo == "ATUEST" || modulo == "ES7")
                    {

                        total = Decimal.Parse(quant2);
                        //      MessageBox.Show("modulo atuest ou ES7 encontrado total vai ser igual="+ total+"\n cod a atualizar = "+cod);
                    }
                    else { MessageBox.Show("tipo " + tipo + " incorreto"); }

                    if ((total - est2) != 0)
                    {
                        //Não atualizar registros caso só tenha ele no mês
                        if (countregister != 1)
                        {
                            Update(cod, total, codprod, tipo);
                            prodatualizados++;
                            WriteLog("Registro atualizado: valor calculado= " + total.ToString() + ", valor encontrado= " + est2.ToString() + "\n Atualizado " + selectedFilial + "estoque" + mesAno + ", produto= " + codprod + ", cod= " + cod);
                            WriteScreenLog("Produto: " + codprod + "Qtde atualizada= " + total.ToString() + "\n Atualizado " + selectedFilial + "estoque" + mesAno + ", produto= " + codprod + ", cod= " + cod);
                        }
                        else 
                        {
                            WriteLog("Registro"+ cod+" não será atualizado por possuir apenas 1 registro no período específicado");
                            WriteScreenLog("Registro" + cod + " não será atualizado por possuir apenas 1 registro no período específicado");
                        }
                            //   MessageBox.Show("update exutado: total= " + total.ToString() + " est2= " + est2.ToString() + "\n Atualizado produto cod= " + cod);

                    }
                    else
                    {// MessageBox.Show("Valores igual (n vou executar update): total= " + total.ToString() + " est2= " + est2.ToString()); 
                    }




                }
                connection.Close();


                count++;
                pgbProcesso.PerformStep();

                if (count > countregister)
                {
                  //  WriteLog("Estoque atual produto: " + codprod + " Quantidade: " + total + " Movimentações atualizadas: " + prodatualizados);
                 //   WriteScreenLog("Estoque atual produto: " + codprod + " Quantidade: " + total + " Movimentações atualizadas: " + prodatualizados);

                    if (prodatualizados >= 1)
                    {
                        UpdateEstoqueAtual(codprod, total);
                        WriteLog("Estoque atualizado do produto: " + codprod + " atualizado para " + total);
                        WriteScreenLog("Estoque atualizado do produto: " + codprod + " Qtde " + total+"\n--------------");
                    }

                }
            }



        }




        string date = DateTime.Now.ToString("dd/MM/yyyy");
        string time = DateTime.Now.ToString("HH:mm:ss");
        private void btnProcessar_Click(object sender, EventArgs e)
        {
            VariableValues();
            btnProcessar.Enabled = false;
            
            CountUpdate = 0;
            lblQuantUpdates.Text = CountUpdate.ToString();
            dia = mtxtData.Text.Substring(0, 2);

            mes = mtxtData.Text.Substring(3, 2);






            ano = mtxtData.Text.Substring(8, 2);
            americandate = mtxtData.Text.Substring(6, 4) + "-" + mtxtData.Text.Substring(3, 2) + "-" + mtxtData.Text.Substring(0, 2);
           //MessageBox.Show(americandate);
            mesAno = mes + ano;


            //Gerar meses escritos
            string mesescrito;
            if (mes == "01") 
            {
                mesescrito = "Janeiro";
            }
            else if (mes == "02")
            {
                mesescrito = "Fevereiro";
            }
            else if (mes == "03")
            {
                mesescrito = "Março";
            }
            else if (mes == "04")
            {
                mesescrito = "Abril";
            }
            else if (mes == "05")
            {
                mesescrito = "Maio";
            }
            else if (mes == "06")
            {
                mesescrito = "Junho";
            }
            else if (mes == "07")
            {
                mesescrito = "Julho";
            }
            else if (mes == "08")
            {
                mesescrito = "Agosto";
            }
            else if (mes == "09")
            {
                mesescrito = "Setembro";
            }
            else if (mes == "10")
            {
                mesescrito = "Outubro";
            }
            else if (mes == "11")
            {
                mesescrito = "Novembro";
            }
            else
            {
                mesescrito = "Dezembro";
            }


            if (cbxProduto.Checked == true)
            {
               
             var selectedOption = MessageBox.Show("Tem certeza que deseja atualizar as movimentações do mês de " + mesescrito + " para o produto a partir da data " + mtxtData.Text + "?"+ "\n\"" + txtProdDesc.Text + "\"", "Mensagem de confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (selectedOption == DialogResult.Yes)
                {

                    WriteLog("--------------------\nProcessamento iniciado");
                    WriteScreenLog("------------------------------------------------------- \nProcessamento iniciado");
                    WriteLog("Reprocessando a partir de " + mtxtData.Text + "\n");

                    WriteScreenLog("Reprocessando a partir de " + mtxtData.Text + "\n");
                    string codproduto = txtCodProd.Text.ToString();             
                    Genaratelbl(codproduto);
                    WriteLog("Processamento finalizado.\nRegistros atualizados= " + lblQuantUpdates.Text);
                    WriteScreenLog("Processamento finalizado. Registros atualizados= " + lblQuantUpdates.Text);
                    lblProcessando.Text = "Processamento finalizado.";

                }
                else 
                {
                    WriteLog("Processamento não autorizado!");
                    WriteScreenLog("Processamento não autorizado!");
                }

                

            }
            else
            {
                var selectedOption = MessageBox.Show("Tem certeza que deseja atualizar as movimentações de TODOS os produtos do mês de " + mesescrito + " a partir da data: " + mtxtData.Text + "?", "Mensagem de confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (selectedOption == DialogResult.Yes)

                {

                    WriteLog("--------------------\nProcessamento iniciado");
                    WriteScreenLog("------------------------------------------------------- \nProcessamento iniciado");
                    WriteLog("Reprocessando a partir de " + mtxtData.Text + "\n");
                    WriteScreenLog("Reprocessando a partir de " + mtxtData.Text + "\n");

                    GetAllProds();
                    WriteLog("Processamento finalizado. Registros atualizados= " + lblQuantUpdates.Text);
                    WriteScreenLog("Processamento finalizado. Registros atualizados= " + lblQuantUpdates.Text);
                    lblProcessando.Text = "Processamento finalizado.";

                }
                else
                {
                    WriteLog("Processamento não autorizado!");
                    WriteScreenLog("Processamento não autorizado!");
                }


            }


            btnProcessar.Enabled = true;


        }

        string selectedFilial;
        private void cbxFilial_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFilial = cbxFilial.SelectedItem.ToString().Substring(0, 3);


        }

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
                WriteLog("Erro ao encontrar conexão com o banco de dados, verifique a chave [SETTINGS] no arquivo sglinx.ini e os serviços do MySQL :" + ex.Message);
                WriteScreenLog("Erro ao encontrar conexão com o banco de dados, verifique a chave [SETTINGS] no arquivo sglinx.ini e os serviços do MySQL :" + ex.Message);
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

            



        }

        void GetDate()
        {

            mtxtData.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }


        private void Update(string cod, decimal total, string codprod, string tipo)
        {




            connection = new MySqlConnection(connectionString);
            string query = "UPDATE mov" + selectedFilial + "estoque" + mesAno + " SET es2_qatu = @total WHERE cod = @cod;";

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@cod", cod);
                cmd.Parameters.AddWithValue("@codprod", codprod);
                //   cmd.Parameters.AddWithValue("@data", data);
                //   cmd.Parameters.AddWithValue("@hora", hora);
                //    cmd.Parameters.AddWithValue("@valorErrado", valorErrado);
                cmd.ExecuteNonQuery();
                connection.Close();
                CountUpdate++;
                lblQuantUpdates.Text = CountUpdate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar movimentação do produto "+codprod+": " + ex, "Erro de rotina", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        void GetAllProds()
        {
            string lastcod = "0" ;
            try
            {
                string Query = "";
                connection = new MySqlConnection(connectionString);
                if (cbxApartirDeProd.Checked)
                {
                   
                        Query = "SELECT es1_cod FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE data >= \"" + americandate + "\" and es1_cod >= " + txtApartirDeProd.Text + " GROUP BY es1_cod;";
                    
                
                }
                else 
                {
                    Query = "SELECT es1_cod FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE data >= " + americandate + " GROUP BY es1_cod;";
                }
                
                MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                MySqlDataReader myreader;

                connection.Open();
                myreader = cmdDataBase.ExecuteReader();

                while (myreader.Read())
                {

                    string es1_cod = myreader.GetString("es1_cod");

                    Genaratelbl(es1_cod);
                    lastcod = es1_cod;

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                bool tryagain = true;
                int maxtry = 6;
                int trycount = 0;
                WriteLog("Tentativa 1 com erro... Produto: " + lastcod);
                while (tryagain == true && trycount < maxtry)
                {
                    System.Threading.Thread.Sleep(2000);
                    try
                    {

                        connection = new MySqlConnection(connectionString);
                        string Query = "SELECT es1_cod FROM mov" + selectedFilial + "estoque" + mesAno + " WHERE data >= " + americandate + "and es1_cod >= " + lastcod + " GROUP BY es1_cod;";
                        MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
                        MySqlDataReader myreader;

                        connection.Open();
                        myreader = cmdDataBase.ExecuteReader();

                        while (myreader.Read())
                        {

                            string es1_cod = myreader.GetString("es1_cod");

                            Genaratelbl(es1_cod);
                            lastcod = es1_cod;

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

        private void cbxProduto_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxProduto.Checked == true)
            {
                txtCodProd.Enabled = true;
            }
            else if (cbxProduto.Checked == false)
            {
                txtCodProd.Enabled = false;
            }
        }

        void WriteLog(string text)
        {
            text = DateTime.Now.ToString("dd/MM/yyyy") + " - " + DateTime.Now.ToString("HH:mm:ss") + "= " + text;
            string path = "LogReprocEstoque.log";

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
        void UpdateEstoqueAtual(string codprod, decimal es2_qatu)
        {
            connection = new MySqlConnection(connectionString);
            string query = "UPDATE es1 SET es1.es2_qatu = @es2_qatu WHERE es1_cod = @codprod AND es1.es1_empresa = " + selectedFilial + " ;";


            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@es2_qatu", es2_qatu);
            cmd.Parameters.AddWithValue("@codprod", codprod);
            cmd.ExecuteNonQuery();
            connection.Close(); 

            string query2 = "UPDATE es2 SET es2.es2_qatu = @es2_qatu WHERE es1_cod = @codprod AND es2.es1_empresa = " + selectedFilial + " ;";
            connection.Open();
            MySqlCommand cmd2 = new MySqlCommand(query2, connection);
            cmd2.Parameters.AddWithValue("@es2_qatu", es2_qatu);
            cmd2.Parameters.AddWithValue("@codprod", codprod);
            cmd2.ExecuteNonQuery();
            connection.Close();

        }

        void GetIniInfo()
        {
            try
            {

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


                //  MessageBox.Show(connectionString);

                // MessageBox.Show(iniserv_nomebanco + iniserv_provider);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }


        void WriteScreenLog(string output)
        {
            if (!string.IsNullOrWhiteSpace(rtxt_log.Text))
            {
                rtxt_log.AppendText("\r\n" + output);
            }
            else
            {
                rtxt_log.AppendText(output);
            }
            rtxt_log.ScrollToCaret();
        }


        void VariableValues()
        {
            selectedFilial = cbxFilial.SelectedItem.ToString().Substring(0, 3);
          
        }

        private void txtCodProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //bloquear entradas diferentes de número
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private void cbxApartirDeProd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxApartirDeProd.Checked)
            {
                txtApartirDeProd.Enabled = true;
            }
            else if (!cbxApartirDeProd.Checked)
            {
                txtApartirDeProd.Text = "";
                txtApartirDeProd.Enabled = false;
            }

            if (cbxProduto.Checked && cbxApartirDeProd.Checked)
            {
                cbxProduto.Checked = false;
                txtCodProd.Text = "";
            }
        }
    }
}
