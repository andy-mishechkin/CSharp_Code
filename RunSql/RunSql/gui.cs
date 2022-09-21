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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RunSql
{
    public partial class gui : Form
    {
        public gui()
        {
            InitializeComponent();
        }

        private void SqlGetDbsButton_Click(object sender, EventArgs e)
        {
            GetDbList();
        }

        private void GetDbList()
        {
            SqlExec objSqlExec = CreateSqlExecObject();

            SqlDataComboBox.Items.Clear();
            object Tables = objSqlExec.Execute("SELECT name FROM sys.databases");
            foreach (DataTable Table in (DataTableCollection)Tables)
                foreach (DataRow row in Table.Rows)
                    SqlDataComboBox.Items.Add(row["name"]);
        }

        private void Exec_Click(object sender, EventArgs e)
        {
            string rawSql = "";

            if (SqlCodeCheckBox.Checked == true)
                rawSql = SqlCodeTextBox.Text;
            else if(SqlFileCheckBox.Checked == true)
                rawSql = (File.OpenText(SqlFileTextBox.Text)).ReadToEnd();

            PrepareSqlCode(rawSql);
        }

        private void SqlFileButton_Click(object sender, EventArgs e)
        {
            GetSqlFile();
        }

        private void GetSqlFile()
        {
            using (OpenFileDialog SqlFileDialog = new OpenFileDialog())
            {
                SqlFileDialog.Filter = "T-SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                SqlFileDialog.FilterIndex = 1;
                SqlFileDialog.ShowDialog();
                SqlFileTextBox.Text = SqlFileDialog.FileName;
            }
        }

        private void PrepareSqlCode(string rawSql)
        {
            string line;
            bool dontAddToPackage = false;
            using (StringReader reader = new StringReader(rawSql))
            {
                StringBuilder SqlPackage = new StringBuilder();
                Regex rgx;
                string checkedLine;

                do
                {
                    line = reader.ReadLine();

                    if (!string.IsNullOrEmpty(line))
                    { 
                        rgx = new Regex("[^a-z]", RegexOptions.IgnoreCase);
                        checkedLine = rgx.Replace(line, "");
                        checkedLine = checkedLine.ToUpper();
                        if (checkedLine == "GO")
                        {
                            ExecSql(SqlPackage.ToString());
                            SqlPackage.Clear();
                        }
                        else
                        {
                            rgx = new Regex(" ");
                            checkedLine = rgx.Replace(line, "");
                            if (checkedLine == "/*")
                                dontAddToPackage = true;
                            else if (checkedLine == "*/")
                                dontAddToPackage = false;
                            rgx = new Regex("^--");
                            Match mResult = rgx.Match(line);
                            using (TextWriterTraceListener PrepareSqlCodeWriter = new TextWriterTraceListener(System.Console.Out))
                            {
                                Debug.Listeners.Add(PrepareSqlCodeWriter);
                                Debug.AutoFlush = true;

                                if ((mResult.Success == false) && (dontAddToPackage == false))
                                {
                                    Debug.WriteLine("Line [" + line + "] will be added to SQL package.");
                                    SqlPackage.AppendLine(line);
                                }
                                else
                                    Debug.WriteLine("Line [" + line + "] will be skipped.");
                            }
                        }
                    }
                    else
                        ExecSql(SqlPackage.ToString());
                } while (line != null);
            }
        }
        
        private void ExecSql(string SqlPackage)
        {
            if (!string.IsNullOrEmpty(SqlPackage))
            {
                SqlExec objSqlExec = CreateSqlExecObject();   
                object SqlResult = objSqlExec.Execute(SqlPackage);
                if(SqlResult !=null)
                    if ((SqlResult.ToString()) == "System.Data.DataTableCollection")
                        BuildTables((DataTableCollection)SqlResult, SqlPackage);
            }
        }

        private SqlExec CreateSqlExecObject()
        {
            SqlExec objSqlExec;

            string SqlServer = SqlServerTextBox.Text;
            int SqlPort = Int32.Parse(SqlPortTextBox.Text);
            string SqlDb = SqlDataComboBox.Text;
            if (SqlDb == null)
                SqlDb = "master";
            if (SqlAuthCheckBox.Checked == true)
            {
                string User = SqlUserTextBox.Text;
                string Password = SqlPswdTextBox.Text;
                objSqlExec = new SqlExec(SqlServer, SqlPort, SqlDb, User, Password);
            }
            else
                objSqlExec = new SqlExec(SqlServer, SqlPort, SqlDb);
            return objSqlExec;
        }

        private void BuildTables(DataTableCollection SqlResult, string Header)
        {
            foreach(DataTable table in SqlResult)
            {
                int TotalRows = table.Rows.Count;
                if(TotalRows == 0)
                {
                    MessageBox.Show("Table contains no data");
                    continue;
                }

                BindingSource Source = new BindingSource();
                Source.DataSource = table;

                DataGridView OutGrid = new DataGridView();
                OutGrid.DataSource = Source;
                OutGrid.ReadOnly = true;
                OutGrid.AutoSize = true;
                OutGrid.ColumnHeadersVisible = true;
                OutGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                OutGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                OutGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(OutGrid_CellFormatting);
                OutGrid.DataError += new DataGridViewDataErrorEventHandler(OutGrid_DataError);

                Form tableForm = new Form();
                tableForm.Size = new Size(1024, 768);
                tableForm.AutoScroll = true;
                tableForm.Text = Header;
                tableForm.Controls.Add(OutGrid);
                tableForm.Show();
            }
        }

        private void OutGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if ((e.Value.ToString()) == "System.Byte")
                {
                    byte[] array = (byte[])e.Value;
                    e.Value = BitConverter.ToString(array);
                    e.FormattingApplied = true;
                }
            }
            else
                e.FormattingApplied = false;
        }

        private void OutGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Console.WriteLine("Exception = {0}", e.Exception);
        }

        private void SqlAuthCheckBox_Click(object sender, EventArgs e)
        {
            SqlUserTextBox.Enabled = SqlAuthCheckBox.Checked;
            SqlPswdTextBox.Enabled = SqlAuthCheckBox.Checked;
        }

        private void SqlCodeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SqlCodeGroupBox.Visible = SqlCodeCheckBox.Checked;
            SqlFileCheckBox.Checked = !SqlCodeCheckBox.Checked;
        }

        private void SqlFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SqlFileGroupBox.Visible = SqlFileCheckBox.Checked;
            SqlCodeCheckBox.Checked = !SqlFileCheckBox.Checked;
        }
    }
}
