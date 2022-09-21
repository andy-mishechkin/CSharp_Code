using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SqlServerApp1 is starting");

            //string infoMessageText = "";
            //string stateMessageText = "";
            string DmVersion;
            string Sql;

            using (SqlConnection Connection = new SqlConnection())
            {
                Connection.ConnectionString = "Server=SQLSRV;Database=master;User ID=sa;Password='Ofpekity34_'";
                Connection.StateChange += new StateChangeEventHandler(StateChange);
                Connection.InfoMessage += new SqlInfoMessageEventHandler(InfoMessage);

                /*
                 НЕ ЗАРАБОТАЛО
                Connection.StateChange += delegate (object sender, StateChangeEventArgs e)
                {
                    stateMessageText += e.ToString();
                };
                Connection.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
                {
                    infoMessageText += e.Message.ToString();
                };
                */

                Connection.FireInfoMessageEventOnUserErrors = true;
                Connection.Open();
                /*
                Sql = "SELECT MRegValue FROM M3Registry WHERE MRegKey='DMVERSION'";

                Console.WriteLine("Sql command: {0}", Sql);
                SqlCommand Command = new SqlCommand(Sql, Connection);
                try
                {
                    DmVersion = (string)Command.ExecuteScalar();
                    Console.WriteLine("DM Version: {0}", DmVersion);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
                */
                Sql = "SELECT name FROM sys.databases";
                SqlDataAdapter Adapter = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand(Sql, Connection);
                Adapter.SelectCommand = Command;
                DataSet dataSet = new DataSet();
                Adapter.Fill(dataSet);
                foreach (DataTable Table in dataSet.Tables)
                {
                    Console.WriteLine("Table: {0}", Table.TableName);
                    foreach (DataRow row in Table.Rows)
                        Console.WriteLine(row["name"]);
                }
                /*
                var daDataOutput = new SqlDataAdapter(Sql, Connection);
                DataTable dtOutput = new DataTable();
                daDataOutput.Fill(dtOutput);

                foreach (DataRow i in dtOutput.Rows)
                {
                    string dataRowOutput = "";

                    for (int j = 0; j < dtOutput.Columns.Count; j++)
                    {
                        dataRowOutput += i[j].ToString() + " ";
                    }
                    Console.WriteLine(dataRowOutput);
                */
            }
        }

        private static void StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            Console.WriteLine("State was:" + e.OriginalState.ToString() + ". New state = " + e.CurrentState.ToString());
        }

        private static void InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            int i;
            for (i = 0; i < e.Errors.Count; i++)
            {
                Console.WriteLine("SqlServer Error: {0}", e.Errors[i].Message);
            }
        }
    }
}
