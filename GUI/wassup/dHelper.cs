using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
namespace wassup
{
    public static class dHelper
    {
        public static DataSet GetDataSet(string command)
        {
            try
            {
                string ConString = "USER ID='\"C##PARTH\"';PASSWORD=oracle;DATA SOURCE=127.0.0.1:1521/ORCL;PERSIST SECURITY INFO=True";
                using (OracleConnection con = new OracleConnection(ConString))
                {
                    OracleCommand cmd = new OracleCommand(command, con);
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    oda.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(" : SQL ERROR !");
                return null;
            }
        }
        public static void SetDataSet()
        {

        }
        public static Object SendMessage(string command)
        {
            try
            {
                string ConString = "USER ID='\"C##PARTH\"';PASSWORD=oracle;DATA SOURCE=127.0.0.1:1521/ORCL;PERSIST SECURITY INFO=True";
                using (OracleConnection con = new OracleConnection(ConString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand(command, con);
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message + " : SQL ERROR !");
                return null;
            }
        }
        public static Object ExecuteCommand(string command)
        {
            try
            {
                string ConString = "USER ID='C##PARTH';PASSWORD=oracle;DATA SOURCE=127.0.0.1:1521/ORCL;PERSIST SECURITY INFO=True";
                using (OracleConnection con = new OracleConnection(ConString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand(command, con);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message+" : SQL ERROR !");
                return null;
            }
        }
    }
}
