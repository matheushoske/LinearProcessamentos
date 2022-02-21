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


namespace ConfPDV
{

    public partial class Form2 : Form
    {
        public static string setapracimakey;

        public Form2()
        {
            InitializeComponent();
            //GetIniInfo(); - Atualizado 27/09: nova classe dalhelper
            ReprocEstoque.DalHelper.IniciaConexaoDal();
            connectionString = ReprocEstoque.DalHelper.connectionString;
          
            FillCombo();
            FormatarData();
        }
        string connectionString;
        string selectedFilial;
        int tipoParametro;//1 = Markdown e 2 = Markup
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
        int countregister;
        private void btnProcessar_Click(object sender, EventArgs e)//Executa as ações descritas ao clicar no botão de processar
        {
            BtnClickVariableValues();
            GetParametro();
            countregister = 0;

            if (!rbtResumo.Checked && !cbxTodos.Checked && !cbxNegativa.Checked)
            {
                MessageBox.Show("Selecione o tipo de registros que deseja atualizar", "Marcação incorreta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (mes != mesFinal)
                {
                    MessageBox.Show("Não é possível realizar o processamento em meses diferentes", "Data incorreta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ano != anoFinal)
                {
                    MessageBox.Show("Não é possível realizar o processamento em anos diferentes", "Data incorreta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    
                    if (cbxProduto.Checked == true)
                    {
                        if (rbtAtual.Checked == true && tipoParametro == 2)
                        {
                            ProcessarMarkUp(int.Parse(txtCodProd.Text));
                        }
                        else if (rbtAtual.Checked == true && tipoParametro == 1)
                        {
                            ProcessarMarkDown(int.Parse(txtCodProd.Text));
                        }
                        else if (rbtMargem.Checked)
                        {
                            ApenasMargem(int.Parse(txtCodProd.Text));
                        }
                        else if (rbtDiario.Checked)
                        {
                            ProcessarDiario(int.Parse(txtCodProd.Text));
                        }

                        if (rbtResumo.Checked)
                        {
                            CriarResumo(int.Parse(txtCodProd.Text));
                        }


                    }
                    else
                    {
                        


                        if (rbtResumo.Checked)
                        {
                            CriarResumo(0);
                        }
                        else
                        {
                        
                            GetAllProds();
                        }
                    }
                    WriteLog("Quantidade de produtos não atualizados por possuir prcompra zerado: "+ countprcomprazerado.ToString());
                    WriteLog("Processamento Finalizado");
                    MessageBox.Show("Processamento Finalizado");
                }// fim do if de validação de mês e ano iguais
            }// fim do if de validção das marcações (todos os produtos ou apenas margem negativa)
            
            
        }//fim do método btnProcessar_Click


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


        }// Insere as informações da empresa na caixa de seleção

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

        void StartVariableValues()
        {


        }//Determina os valores das variaveis ao iniciar o programa

        void BtnClickVariableValues()
        {
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
        }//Determina os valores das variaveis após clicar no botão de processar  

        decimal desp_opr_parametro;
        void GetParametro()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT pr_valor FROM parametro WHERE pr_chave = \"CALCULO_MARGEM\" and empresa = @empresa";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@empresa", selectedFilial);
            cmd.Prepare();
            var result = (string)cmd.ExecuteScalar();
            tipoParametro = int.Parse(result);
            
            string sql2 = "SELECT pr_valor FROM parametro WHERE pr_chave = \"DESPESA_OPERACIONAL\" and empresa = @empresa";
            MySqlCommand cmd2 = new MySqlCommand(sql2, connection);
            cmd2.Parameters.AddWithValue("@empresa", selectedFilial);
            cmd2.Prepare();
            var result2 = (string)cmd2.ExecuteScalar();
            desp_opr_parametro = Decimal.Parse(result2);
            connection.Close();

            
            
        }//Recebe o valor do parametro CALCULO_MARGEM para a empresa selecionada   

        void WriteLog(string text)
        {
            text = DateTime.Now.ToString("dd/MM/yyyy") + " - " + DateTime.Now.ToString("HH:mm:ss") + "= " + text;
            string path = "LogReprocCurvaABC.txt";

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
            if (!string.IsNullOrWhiteSpace(rtxt_log.Text))
            {
                rtxt_log.AppendText("\r\n" + text);
            }
            else
            {
                rtxt_log.AppendText(text);
            }
            rtxt_log.ScrollToCaret();


        }//Escreve no log ReprocCurvaABC.log o texto recebido  

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
        }//Atualiza o Texto de descrição do produto de acordo com o código do produto escrito     

        private void txtCodProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }//Bloqueia entradas diferentes de número no código do produto

        private void cbxProduto_CheckStateChanged(object sender, EventArgs e)//Habilita e desabilita o texto do código do produto de acordo com a caixa de seleção

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

        private void FormatarData()
        {
            dtpDataInicial.Format = DateTimePickerFormat.Custom;
            dtpDataInicial.CustomFormat = "dd/MM/yyyy";
            dtpDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");


            dtpDataFinal.Format = DateTimePickerFormat.Custom;
            dtpDataFinal.CustomFormat = "dd/MM/yyyy";
            dtpDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }//Formata a data e insere o valor do dia

        void ProcessarMarkUp(int codprod) 
        {
            WriteLog("Processando MarkUp atual, produto"+codprod);

            //MessageBox.Show("vou entrar");
            GetDadosProduto(codprod);
           // MessageBox.Show("sai");
            connection = new MySqlConnection(connectionString);
            string Query;
            if (cbxTodos.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and data <= @americandateFinal";
            }
            else if (cbxNegativa.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and margem <= 0 and data <= @americandateFinal";
            }
            else 
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and data <= @americandateFinal";
            }
            MySqlDataReader myreader;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@americandate", americandate);
            cmd.Parameters.AddWithValue("@americandateFinal", americandateFinal);
            cmd.Parameters.AddWithValue("@codprod", codprod.ToString());
            myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {

                string tipo = myreader.GetString("tipo");
                string cupom = myreader.GetString("cupom");
                string data = myreader.GetString("data");
                string caixa = myreader.GetString("caixa");
                string item = myreader.GetString("item");
                string quantS = myreader.GetString("quant");
                decimal quant = Decimal.Parse(quantS);
                string descontoS = myreader.GetString("desconto");
                decimal desconto = Decimal.Parse(descontoS);

                string totalS = myreader.GetString("total");
                decimal total = Decimal.Parse(totalS);
                string prunitS = myreader.GetString("prunit");
                decimal prunit = Decimal.Parse(prunitS);
                string tributoS = myreader.GetString("tributo");
                decimal tributo;
                if (tributoS != "F" || tributoS != "I")
                {
                    if (tributoS == "I")
                    {
                        tributo = 0;
                    }
                    else if (tributoS == "F")
                    {
                        tributo = 0;
                    }
                    else
                    {
                        //  MessageBox.Show(tributoS); 
                        tributo = Decimal.Parse(tributoS);
                    }
                }
                else 
                {
                    tributo = 0;
                }
                
                string margemS = myreader.GetString("margem");
                decimal margem = Decimal.Parse(margemS);
                string piscofinsS = myreader.GetString("es1_piscofins");
                decimal piscofinscod = Decimal.Parse(piscofinsS);
                string cmv = myreader.GetString("cmv");
              //  MessageBox.Show(cupom);
                CalcularMargem(total, prcompra_es1, quant, desconto, margem);
              //  MessageBox.Show(margemCalculada.ToString());
                CalcularCMV(total,prcusto_es1,tributo, piscofinscod, desconto, quant);
              //  MessageBox.Show(cmvCalculado.ToString());
                Update(cmvCalculado, margemCalculada, cupom, data, caixa, item, codprod, tipo, prcompra_es1, prcusto_es1);
                WriteLog("Cupom: "+ cupom + "tipo: Markup caixa: " + caixa + " Produto: "+ codprod);

            }
            connection.Close();
          //  MessageBox.Show("Processamento finalizado, será gerado o resumo referente a data");
         //-----------------------------------------------------------------------------------------   CriarResumo(codprod);



        }//Realiza todo o processamento em caso de empresa Markup
        void Update(decimal cmv, decimal margem, string cupom, string data, string caixa, string item, int codprod, string tipo, decimal prcompra, decimal prcusto) 
        {
            connection = new MySqlConnection(connectionString);
             string query = "UPDATE log" + selectedFilial + "venda" + mesAno + " SET CMV = @cmv, margem = @margem, es1_prcompra = @prcompra, es1_prcusto = @prcusto WHERE cupom = @cupom and caixa = @caixa and item = @item and codprod = @codprod;";
            //string query = "UPDATE log" + selectedFilial + "venda" + mesAno + " SET CMV = "+Math.Round(cmv, 5).ToString().Replace(",",".")+", margem = "+Math.Round(margem, 5).ToString().Replace(",",".")+" WHERE cupom = "+cupom+" and caixa = "+caixa+" and item = "+item+" and codprod = "+codprod.ToString()+";";
            try
              {
                
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@cmv", Math.Round(cmv, 5).ToString().Replace(",", "."));
                cmd.Parameters.AddWithValue("@margem", Math.Round(margem, 5).ToString().Replace(",", "."));
                cmd.Parameters.AddWithValue("@cupom", cupom);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@caixa", caixa);
                cmd.Parameters.AddWithValue("@item", item);
                cmd.Parameters.AddWithValue("@codprod", codprod.ToString());
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@prcompra", prcompra);
                cmd.Parameters.AddWithValue("@prcusto", prcusto);
                //MessageBox.Show(item);
                //MessageBox.Show(query);
                //Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                countregister++;
                lblQuantUpdates.Text = countregister.ToString();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar margem e cmv do produto " + codprod + ": " + ex, "Erro de rotina", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
          //  MessageBox.Show("Cupom: "+ cupom + ", produto: "+codprod+ " atualizado");

        }//Atualiza a tabela logvenda com os dados de margem e cmv atualizados

        decimal prcompra_es1;
        decimal prcusto_es1;
        decimal desp_oper_perc_es1;
        decimal ipi_es1;
        decimal acrescimocusto_es1;
        void GetDadosProduto(int selected_codprod)
        {
           // MessageBox.Show("Entrei aqui");
            connection = new MySqlConnection(connectionString);
            string Query = "SELECT es1_prcusto, es1_prcompra, es1_desp_oper_perc, es1_ipi, es1_acrescimocusto FROM es1 WHERE es1_cod = @codprod and es1_empresa = @filial ";

            MySqlDataReader myreader;

            connection.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@codprod", selected_codprod);
            cmd.Parameters.AddWithValue("@filial", selectedFilial);
          //  MessageBox.Show(Query + " valores: " +selected_codprod.ToString()+ " "+ selectedFilial);

            myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                string es1_prcompra = myreader.GetString("es1_prcompra");
                prcompra_es1 = Decimal.Parse(es1_prcompra);
                string es1_prcusto = myreader.GetString("es1_prcusto");
                prcusto_es1 = Decimal.Parse(es1_prcusto);
                string es1_desp_oper_perc = myreader.GetString("es1_desp_oper_perc");
                desp_oper_perc_es1 = Decimal.Parse(es1_desp_oper_perc);
                string es1_ipi = myreader.GetString("es1_ipi");
                ipi_es1 = Decimal.Parse(es1_ipi);
                string es1_acrescimocusto = myreader.GetString("es1_acrescimocusto");
                acrescimocusto_es1 = Decimal.Parse(es1_acrescimocusto);

            }
            connection.Close();



        }//Busca os dados de prcusto e prcompra da tabela es1

        decimal cmvCalculado;
        
        void CalcularCMV(decimal total, decimal prcusto, decimal tributo, decimal piscofins, decimal desconto, decimal quant) 
        {
            decimal tributoCalculado;
            VerificarPisCofins(piscofins);
            if (tipoParametro == 2)
            {
                // CMV MARKUP
                if (tributo != 0)
                { 
                    tributoCalculado = (tributo / 100) * ((total - desconto)/quant);
                }
                else 
                { 
                    tributoCalculado = 0; 
                }
                cmvCalculado = prcusto + tributoCalculado + (piscofinsCalculado * ((total - desconto)/quant));
            }
            else 
            {
                // CMV MARKDOWN
                decimal despopr_prvenda;
                if (desp_oper_perc_es1 != 0)
                {
                    despopr_prvenda = (desp_oper_perc_es1 / 100) * ((total-desconto)/quant);
                }
                else 
                {
                    despopr_prvenda = (desp_opr_parametro / 100) * ((total - desconto) / quant);
                }
                if (tributo != 0)
                { tributoCalculado = (tributo / 100) * ((total - desconto) / quant); }
                else 
                { tributoCalculado = 0; }
                cmvCalculado = prcusto + despopr_prvenda + ipi_es1 + acrescimocusto_es1 + tributoCalculado + (piscofinsCalculado * ((total - desconto) / quant));

            }
            
        }
        decimal margemCalculada;

        int countprcomprazerado = 0;
        void CalcularMargem(decimal total, decimal prcompra,  decimal quant, decimal desconto, decimal margematual) 
        {
            if (tipoParametro == 2) //se for tipo markup
            {

                if (prcompra == 0 || quant == 0) 
                {
                    margemCalculada = margematual;
                    WriteLog("Produto não será atualizado por possuir quantidade ou es1_prcompra = 0 (não foi possível o cálculo do Markup)");
                    countprcomprazerado++;
                }
                else 
                {
                    margemCalculada = (((((total - desconto) / quant)) - prcompra) / prcompra) * 100;
                }
                
            }
            else // se for markdown (parametro 1)
            {
                margemCalculada = (((total-desconto)/quant) - cmvCalculado) / ((total - desconto)/quant) * 100;
            }
        }

        decimal piscofinsCalculado;
        void VerificarPisCofins(decimal piscofinscod) 
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "SELECT SUM((tab_valor/100) + (tab_cofins/100)) AS piscofins FROM st_piscofins WHERE tab_cod = @cod group by tab_cod";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@cod", piscofinscod); //retorna o valor pronto para ser multiplicado
            cmd.Prepare();
            var result = (decimal)cmd.ExecuteScalar();
            piscofinsCalculado = result;



            connection.Close();

        }
        void ProcessarMarkDown(int codprod)
        {
            WriteLog("Processando MarkDown atual, produto" + codprod);

            GetDadosProduto(codprod);


            connection = new MySqlConnection(connectionString);
            string Query;
            if (cbxTodos.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod";
            }
            else if (cbxNegativa.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and margem <= 0 ";
            }
            else
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod order by cupom, item,caixa, data ";
            }
            MySqlDataReader myreader;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@americandate", americandate);
            cmd.Parameters.AddWithValue("@codprod", codprod.ToString());
            myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {

                string tipo = myreader.GetString("tipo");
                string cupom = myreader.GetString("cupom");
                string data = myreader.GetString("data");
                string caixa = myreader.GetString("caixa");
                string item = myreader.GetString("item");
                string quantS = myreader.GetString("quant");
                decimal quant = Decimal.Parse(quantS);
                string descontoS = myreader.GetString("desconto");
                decimal desconto = Decimal.Parse(descontoS);
                string totalS = myreader.GetString("total");
                decimal total = Decimal.Parse(totalS);
                string prunitS = myreader.GetString("prunit");
                decimal prunit = Decimal.Parse(prunitS);
                string tributoS = myreader.GetString("tributo");
                decimal tributo;
                if (tributoS != "F" || tributoS != "I")
                {
                    if (tributoS == "I")
                    {
                        tributo = 0;
                    }
                    else if (tributoS == "F")
                    {
                        tributo = 0;
                    }
                    else
                    {
                        //  MessageBox.Show(tributoS); 
                        tributo = Decimal.Parse(tributoS);
                    }
                }
                else
                {
                    tributo = 0;
                }

                string margemS = myreader.GetString("margem");
                decimal margem = Decimal.Parse(margemS);
                string piscofinsS = myreader.GetString("es1_piscofins");
                decimal piscofinscod = Decimal.Parse(piscofinsS);
                string cmv = myreader.GetString("cmv");
               // MessageBox.Show(cupom);
                CalcularCMV(total, prcusto_es1, tributo, piscofinscod, desconto,quant);
                CalcularMargem(total, prcompra_es1, quant, desconto,margem);
              //  MessageBox.Show(margemCalculada.ToString());
                
             //   MessageBox.Show(cmvCalculado.ToString());
                Update(cmvCalculado, margemCalculada, cupom, data, caixa, item, codprod, tipo,prcompra_es1, prcusto_es1);

                WriteLog("Cupom: " + cupom + "tipo: MarkDown caixa: " + caixa + " Produto: " + codprod);

            }
            connection.Close();

          //  CriarResumo(codprod);



        }
        void GetAllProds()
        {
            connection = new MySqlConnection(connectionString);
            string Query = "SELECT codprod FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= \"" + americandate + "\" and data <= \""+americandateFinal+"\" GROUP BY codprod;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, connection);
            MySqlDataReader myreader;

            connection.Open();
            myreader = cmdDataBase.ExecuteReader();
            while (myreader.Read())
            {

                string es1_cod = myreader.GetString("codprod");
                int codprod = int.Parse(es1_cod);
                
                if (tipoParametro == 2 && rbtAtual.Checked)
                {
                    ProcessarMarkUp(codprod);
                }
                else if (tipoParametro == 1 && rbtAtual.Checked)
                {
                    ProcessarMarkDown(codprod);
                }
                if (rbtMargem.Checked)
                {

                    ApenasMargem(codprod);
                }
                else if (rbtDiario.Checked)

                {
                    ProcessarDiario(codprod);
                }

                        


            }
            connection.Close();
           



        }

        void CriarResumo(int codprod) 
        {

            WriteLog("Criando resumo do produto: "+codprod.ToString());
            connection = new MySqlConnection(connectionString);
            string query;
            if (codprod == 0) //se o cod prod for 1 ele vai atualizar todos os produtos(só vai ser 0 se o usuário não marcar produto selecionado)

            {
                query = "DELETE from log" + selectedFilial + "venda" + mesAno + "res where data >= @americandate;" +
                                "INSERT INTO log" + selectedFilial + "venda" + mesAno + "res " +
                                "SELECT l.DATA AS DATA, l.codprod AS es1_cod, CONVERT(l.codbusca, CHAR(14)) AS es1_codbarra,p.es1_desc AS es1_desc, " +
                                    "ROUND(SUM(l.total - l.desconto), 3) AS valorliq, SUM(l.quant) AS quant, " +
                                    "ROUND(SUM(l.es1_prcompra * quant), 5) AS es1_prcompra, ROUND(SUM(l.es1_prcusto * quant), 5) AS es1_prcusto, l.es1_prcompra AS es1_pmz, " +
                                    "ROUND(((SUM(total - desconto) - SUM(cmv * quant)) / SUM(total - desconto) * 100), 5) AS margem, " +
                                    "p.es1_familia AS es1_familia, p.es1_departamento AS es1_departamento, p.es1_secao AS es1_secao,  l.tributo AS tributo, l.promo AS promo , l.es1_piscofins AS es1_pisconfins " +
                                    "FROM log" + selectedFilial + "venda" + mesAno + " l " +
                                    "INNER JOIN es1 e ON e.es1_cod = l.codprod AND e.es1_empresa = l.loja " +
                                    "INNER JOIN es1p p ON p.es1_cod = l.codprod AND p.es1_cod = e.es1_cod " +
                                    "WHERE l.data >= @americandate and data <= @americandateFinal " +
                                    "GROUP BY DATA, codprod " +
                                    "ORDER BY codprod; ";

            }
            else
            {
                query = "DELETE from log" + selectedFilial + "venda" + mesAno + "res where es1_cod = @codprod and data >= @americandate;" +
                                "INSERT INTO log" + selectedFilial + "venda" + mesAno + "res " +
                                "SELECT l.DATA AS DATA, l.codprod AS es1_cod, CONVERT(l.codbusca, CHAR(14)) AS es1_codbarra,p.es1_desc AS es1_desc, " +
                                    "ROUND(SUM(l.total - l.desconto), 3) AS valorliq, SUM(l.quant) AS quant, " +
                                    "ROUND(SUM(l.es1_prcompra * quant), 5) AS es1_prcompra, ROUND(SUM(l.es1_prcusto * quant), 5) AS es1_prcusto, l.es1_prcompra AS es1_pmz, " +
                                    "ROUND(((SUM(total - desconto) - SUM(cmv * quant)) / SUM(total - desconto) * 100), 5) AS margem, " +
                                    "p.es1_familia AS es1_familia, p.es1_departamento AS es1_departamento, p.es1_secao AS es1_secao,  l.tributo AS tributo, l.promo AS promo , l.es1_piscofins AS es1_pisconfins " +
                                    "FROM log" + selectedFilial + "venda" + mesAno + " l " +
                                    "INNER JOIN es1 e ON e.es1_cod = l.codprod AND e.es1_empresa = l.loja " +
                                    "INNER JOIN es1p p ON p.es1_cod = l.codprod AND p.es1_cod = e.es1_cod " +
                                    "WHERE e.es1_cod = @codprod AND l.data >= @americandate and data <= @americandateFinal " +
                                    "GROUP BY DATA, codprod " +
                                    "ORDER BY codprod; ";
            }
            try
            {

                connection.Open();
                
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codprod", codprod);
                cmd.Parameters.AddWithValue("@americandate", americandate);
                cmd.Parameters.AddWithValue("@americandateFinal", americandateFinal);
                cmd.ExecuteNonQuery();
                connection.Close();
                if (codprod != 0)
                {
                    WriteLog("Resumo criado, produto: " + codprod);
                //    MessageBox.Show("Resumo gerado com sucesso, produto: " + codprod);
                }
                else 
                {
                    WriteLog("Resumo geral criado");
                    MessageBox.Show("Resumo geral criado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar resumo produto: " + codprod + " : " + ex, "Erro de rotina", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteLog("Erro ao criar resumo produto: " + codprod + " : " + ex);
            }
           
        }

        private void rbtResumo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtResumo.Checked)
            {
                cbxNegativa.Enabled = false;
                cbxNegativa.Checked = false;
                cbxTodos.Enabled = false;
                cbxTodos.Checked = false;
            }
            else 
            {
                cbxNegativa.Enabled = true;
                cbxTodos.Enabled = true;
            }
        }

        void ApenasMargem(int codprod) 
        {
      
            connection = new MySqlConnection(connectionString);
            string Query;
            if (cbxTodos.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins, es1_prcompra, es1_prcusto  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and data <= @americandateFinal";
            }
            else if (cbxNegativa.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins, es1_prcompra, es1_prcusto   FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and margem <= 0 and data <= @americandateFinal";
            }
            else
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins, es1_prcompra, es1_prcusto  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and data <= @americandateFinal";
            }
            MySqlDataReader myreader;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@americandate", americandate);
            cmd.Parameters.AddWithValue("@americandateFinal", americandateFinal);
            cmd.Parameters.AddWithValue("@codprod", codprod.ToString());
            myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {

                string tipo = myreader.GetString("tipo");
                string cupom = myreader.GetString("cupom");
                string data = myreader.GetString("data");
                string caixa = myreader.GetString("caixa");
                string item = myreader.GetString("item");
                string quantS = myreader.GetString("quant");
                decimal quant = Decimal.Parse(quantS);
                string descontoS = myreader.GetString("desconto");
                decimal desconto = Decimal.Parse(descontoS);

                string totalS = myreader.GetString("total");
                decimal total = Decimal.Parse(totalS);
                string prcompraS = myreader.GetString("es1_prcompra");
                decimal prcompra = Decimal.Parse(prcompraS);

                string prcustoS = myreader.GetString("es1_prcusto");
                decimal prcusto = Decimal.Parse(prcustoS);


                string prunitS = myreader.GetString("prunit");
                decimal prunit = Decimal.Parse(prunitS);
                string tributoS = myreader.GetString("tributo");
                decimal tributo;
                if (tributoS != "F" || tributoS != "I")
                {
                    if (tributoS == "I")
                    {
                        tributo = 0;
                    }
                    else if (tributoS == "F")
                    {
                        tributo = 0;
                    }
                    else
                    {
                        //  MessageBox.Show(tributoS); 
                        tributo = Decimal.Parse(tributoS);
                    }
                }
                else
                {
                    tributo = 0;
                }

                string margemS = myreader.GetString("margem");
                decimal margem = Decimal.Parse(margemS);
                string piscofinsS = myreader.GetString("es1_piscofins");
                decimal piscofinscod = Decimal.Parse(piscofinsS);
                string cmv = myreader.GetString("cmv");
                CalcularCMV(total, prcusto, tributo, piscofinscod, desconto, quant);

                CalcularMargem(total, prcompra, quant, desconto, margem);
              //  MessageBox.Show(cmvCalculado.ToString());
                WriteLog("Atualizando Margem e cmv... Cupom: " + cupom + " caixa" + caixa + " Produto: " + codprod);
                Update(cmvCalculado, margemCalculada, cupom, data, caixa, item, codprod, tipo, prcompra, prcusto);

                WriteLog("Margem Atualizada! Cupom: " + cupom + " caixa" + caixa + " Produto: " + codprod);

            }
            connection.Close();
          //---------------------------------------------------------------------  CriarResumo(codprod);
        }

        void ProcessarDiario(int codprod) 
        {
            WriteLog("Processando Diario, produto" + codprod);
            connection = new MySqlConnection(connectionString);
            string Query;
            if (cbxTodos.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and data <= @americandateFinal ";
            }
            else if (cbxNegativa.Checked == true)
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins  FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and codprod = @codprod and margem <= 0 and data <= @americandateFinal";
            }
            else
            {
                Query = "SELECT tipo, cupom, data, caixa, item, quant, desconto, total, prunit, RIGHT(tributo, 2) as tributo, margem, cmv, es1_piscofins FROM log" + selectedFilial + "venda" + mesAno + " WHERE data >= @americandate and data <= @americandateFinal";
            }
            MySqlDataReader myreader;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@americandate", americandate);
            cmd.Parameters.AddWithValue("@americandateFinal", americandateFinal);
            cmd.Parameters.AddWithValue("@codprod", codprod.ToString());
            myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {

                string tipo = myreader.GetString("tipo");
                string cupom = myreader.GetString("cupom");
                string data = myreader.GetString("data");
                if (1 == 1) 
                {
                    data = data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2);
                }// converte a data em formato americano
               
                string caixa = myreader.GetString("caixa");
                string item = myreader.GetString("item");
                string quantS = myreader.GetString("quant");
                decimal quant = Decimal.Parse(quantS);
                string descontoS = myreader.GetString("desconto");
                decimal desconto = Decimal.Parse(descontoS);
                string totalS = myreader.GetString("total");
                decimal total = Decimal.Parse(totalS);
                string prunitS = myreader.GetString("prunit");
                decimal prunit = Decimal.Parse(prunitS);
                string tributoS = myreader.GetString("tributo");
                decimal tributo;
                if (tributoS != "F" || tributoS != "I")
                {
                   //MessageBox.Show(cupom+" "+tributoS);
                    if (tributoS == "I")
                    {
                        tributo = 0;
                    }
                    else if (tributoS == "F")
                    {
                        tributo = 0;
                    }
                    else {
                      //  MessageBox.Show(tributoS); 
                        tributo = Decimal.Parse(tributoS); }
                    
                }
                else
                {
                    tributo = 0;
                }

                string margemS = myreader.GetString("margem");
                decimal margem = Decimal.Parse(margemS);
                string piscofinsS = myreader.GetString("es1_piscofins");
                decimal piscofinscod = Decimal.Parse(piscofinsS);
                string cmv = myreader.GetString("cmv");
            //    MessageBox.Show(cupom);
                GetDadosDiario(codprod, data);
                CalcularCMV(total, prcusto_diario, tributo, piscofinscod, desconto, quant);
                CalcularMargem(total, prcompra_diario, quant, desconto, margem);
                //  MessageBox.Show(margemCalculada.ToString());

                //  MessageBox.Show(cmvCalculado.ToString());
               WriteLog("Atualizando com valores do diario, cupom: " + cupom + " caixa: " + caixa + " Produto:" + codprod);
                Update(cmvCalculado, margemCalculada, cupom, data, caixa, item, codprod, tipo, prcompra_diario, prcusto_diario);
                WriteLog("Atualizado com valores do diario, cupom: " + cupom + " caixa: " + caixa + " Produto:" + codprod);

            }
            connection.Close();
            WriteLog("Atualizado de acordo com diario, produto" + codprod);
            //MessageBox.Show("Processamento finalizado, será gerado o resumo referente a data");
           //----------------------------------------------------------------------------------- CriarResumo(codprod);


        }

        decimal prcompra_diario;
        decimal prcusto_diario;

        void GetDadosDiario(int codprod, string data) 
        {
           // MessageBox.Show("Entrei no getdados diario");
            connection = new MySqlConnection(connectionString);
            string Query = "SELECT es1_prcompra, es1_prcusto FROM diario"+mesAno+" WHERE es1_cod = @codprod and es1_empresa = @filial and data = @data ";

            MySqlDataReader myreader;

            connection.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connection);
            cmd.Parameters.AddWithValue("@codprod", codprod);
            cmd.Parameters.AddWithValue("@filial", selectedFilial);
            cmd.Parameters.AddWithValue("@data", data);
            myreader = cmd.ExecuteReader();

           // MessageBox.Show(Query+ " --- codprod: "+ codprod + " filial: "+selectedFilial + " data: "+data.ToString());

            while (myreader.Read())
            {
                string es1_prcompra = myreader.GetString("es1_prcompra");
             //   MessageBox.Show(es1_prcompra);
                prcompra_diario = Decimal.Parse(es1_prcompra);
                string es1_prcusto = myreader.GetString("es1_prcusto");
                prcusto_diario = Decimal.Parse(es1_prcusto);

          
            }
            connection.Close();

        }

        private void cbxTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTodos.Checked) 
            {
                cbxNegativa.Checked = false;
            
            }

        }

        private void cbxNegativa_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxNegativa.Checked)
            {
                cbxTodos.Checked = false;

            }

        }
    }

    
}
