using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace ReprocEstoque
{
    public static class DalHelper
    {

        
        public static string connectionString;
        public static MySqlConnection cn;

        public static void IniciaConexaoDal()
        {
             string iniserv_provider = "localhost";//valor padrao
             string iniserv_nomebanco = "sglinx";//valor padrao
            try
            {


                IniFile MyReader = new IniFile("sglinx.ini");

                iniserv_provider = MyReader.Read("provider", "SETTINGS").ToString();

                iniserv_nomebanco = MyReader.Read("nomebanco", "SETTINGS").ToString();
            }
            catch (Exception ex)
            {
                throw;

            }



            try
            {

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
                cn = new MySqlConnection(connectionString);
                cn.Open();
                cn.Close();
            }
            catch
            {
                try
                {
                    //remove o "Integrated Security=SSPI" da connectionstring
                    if (Environment.MachineName.ToString() == "MATHEUS-DESENV")
                    {
                        connectionString = "Server=localhost;Database=sglinx;Uid=adminlinear;Pwd=@2013linear;" +
                                            "Pooling=false;";
                    }
                    else
                    {
                        connectionString = "Server=" + iniserv_provider + ";Database=" + iniserv_nomebanco + ";Uid=adminlinear;Pwd=@2013linear;" +
                                            "Pooling=false;";
                    }
                    cn = new MySqlConnection(connectionString);
                    cn.Open();
                    cn.Close();
                }

                catch (Exception ex)
                {
                    throw;
                }


            }
        }

    }
    }

