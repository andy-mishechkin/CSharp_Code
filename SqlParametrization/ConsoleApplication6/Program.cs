using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("The message is: ");
            string p = Console.ReadLine();

            p = p.Replace("char(13)", "\n").Replace("char(10)", "\r");
            
            //p = p.Replace("#13", "\n").Replace("#10", "\r");
            //Console.WriteLine();
            Console.WriteLine("Entered: ({0})", p);
            //Console.ReadLine();
            //return;

            string cs = "Data Source=.\\E12;Database=AS_DataSource;Integrated Security=SSPI;Connection Timeout=1;"; // network=dbmssocn;";

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandText = "select @1 as s"; // '" + p.Replace("'", "''") + "' as s";
                    //cmd.CommandText = "select '" + p + "' as s";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandTimeout = 1;

                    SqlParameter p1 = cmd.Parameters.Add("@1", System.Data.SqlDbType.VarChar, 2000);
                    p1.Value = p;

                    /*SqlParameter id = cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    SqlParameter flag = cmd.Parameters.Add("@flag", System.Data.SqlDbType.Int);*/

                    string s = (string) cmd.ExecuteScalar();

                    Console.WriteLine("Selected: ({0})", s);
                    Console.ReadLine();
                }
            }
        }
    }
}
