using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace LinearProcessamentos
{
    public static class RotinasGerais
    {

        public static void Auditoria(string Modulo, string Operacao, string historico, string sql, int empresa = 1, string cusu = "") 
        {
            
                MySqlConnection connection;
                connection = new MySqlConnection(GetIniInfo());
                connection.Open();
                string query = "insert into auditoria (nome_computador, modulo, operacao, historico, csql, empresa) values(\"LINEAR_PROCESSAMENTOS\",\"" + Modulo + "\", \"" + Operacao + "\", \"" + historico + "\", \"" + sql + "\", \"" + empresa.ToString() + "\" )";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            
          
        }

       static string GetIniInfo()
        {
            
                ReprocEstoque.IniFile MyReader = new ReprocEstoque.IniFile("sglinx.ini");
                string connectionString;
                string iniserv_provider = MyReader.Read("provider", "SETTINGS").ToString();

               string iniserv_nomebanco = MyReader.Read("nomebanco", "SETTINGS").ToString();
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

                return connectionString;

               
            
           

        }

    }
}
