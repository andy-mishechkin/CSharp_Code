using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RunSql
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        [STAThread]
        static void Main()
        {
            //For Akvelon
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new gui());
        }
    }

    class SqlExec
    {
        private string SqlServer;
        private int Port;
        private string SqlDb;
        private string User;
        private string Password;

        public SqlExec(string SqlServer, int Port, string SqlDb) : this(SqlServer, Port, SqlDb, null, null) { }
        public SqlExec(string SqlServer, int Port, string SqlDb, string User, string Password)
        {
            this.SqlServer = SqlServer;
            this.Port = Port;
            this.SqlDb = SqlDb;
            this.User = User;
            if(User != null)
                this.Password = Password;
        }

        public SqlConnection OpenConnection()
        {
            string ConnectionString = "";
            if (this.User == null)
                ConnectionString = System.String.Format("Server={0},{1};Database={2};Integrated Security=True;", this.SqlServer, this.Port, this.SqlDb);
            else
                ConnectionString = System.String.Format("Server={0},{1};Database={2};User Id={3};Password='{4}'", this.SqlServer, this.Port, this.SqlDb, this.User, this.Password);
            SqlConnection Connection = new SqlConnection();
            Connection.ConnectionString = ConnectionString;
            Connection.InfoMessage += new SqlInfoMessageEventHandler(InfoMessage);
            Connection.FireInfoMessageEventOnUserErrors = true;
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(("Sql Server Connection error " + ex.Message),"RunSQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Connection;
        }

        private void InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            for (int i = 0; i < e.Errors.Count; i++)
            {
                using (TextWriterTraceListener SqlInfoMessageWriter = new TextWriterTraceListener(Console.Out))
                {
                    Trace.Listeners.Add(SqlInfoMessageWriter);
                    Trace.AutoFlush = true;
                    Trace.WriteLine("SqlServer message: {0}", e.Errors[i].Message);
                }
            }
        }

        public object Execute(string Sql)
        {
            object SqlResult = null;
            int timeout = 0;

            using (SqlConnection Connection = OpenConnection())
            {
                using (TextWriterTraceListener SqlExecWriter = new TextWriterTraceListener(Console.Out))
                {
                    Debug.Listeners.Add(SqlExecWriter);
                    Debug.AutoFlush = true;

                    Debug.WriteLine("Execute SQL package:");
                    Debug.WriteLine(Sql);
                    Debug.WriteLine("---------------------------------");
                }
                SqlCommand Command = GetSqlCommand(Connection, timeout);
                Command.CommandText = Sql ?? throw new ArgumentNullException("Test of SQL query cannot be null", nameof(Sql));
                if (Sql.Contains("SELECT") == true)
                    SqlResult = RunSelectCommand(Command);
                else if (Sql.Contains("USE") == true)
                {
                    string NewDb = (Sql.Trim()).Substring(4);
                    this.SqlDb = NewDb;
                }
                else
                {
                    SqlResult = RunExecuteNonQuery(Command);
                }
            }
            return SqlResult;
        }

        private SqlCommand GetSqlCommand(SqlConnection Connection, int timeout)
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Connection;
            Command.CommandTimeout = timeout;
            Command.CommandText = "SET ROWCOUNT 2000";
            Command.ExecuteNonQuery();

            return Command;
        }

        private DataTableCollection RunSelectCommand(SqlCommand Command)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter();
            Adapter.SelectCommand = Command;

            DataSet dataSet = new DataSet();
            try
            {
                Adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                using (TextWriterTraceListener SqlAdapterWriter = new TextWriterTraceListener(Console.Out))
                {
                    Trace.Listeners.Add(SqlAdapterWriter);
                    Trace.AutoFlush = true;
                    Trace.WriteLine("SqlServer message:");
                    Trace.Indent();
                    Trace.WriteLine(ex.Message);
                    Trace.Unindent();
                }
            }
            return dataSet.Tables;
        }

        private int RunExecuteNonQuery(SqlCommand Command)
        {
            int ExecResult = 0;
            try
            {
                ExecResult = Command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                using (TextWriterTraceListener SqlExecuteNonQueryWriter = new TextWriterTraceListener(Console.Out))
                {
                    Trace.Listeners.Add(SqlExecuteNonQueryWriter);
                    Trace.AutoFlush = true;
                    Trace.WriteLine("ExecuteNonQuery error:");
                    Trace.Indent();
                    Trace.WriteLine(ex.Message);
                    Trace.Unindent();
                }
            }
            return ExecResult;
        }
    }
}
