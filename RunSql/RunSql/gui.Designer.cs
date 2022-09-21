namespace RunSql
{
    partial class gui
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gui));
            this.SqlServerMainGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlAuthCheckBox = new System.Windows.Forms.CheckBox();
            this.SqlGetDbsButton = new System.Windows.Forms.Button();
            this.SqlPswdGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlPswdTextBox = new System.Windows.Forms.TextBox();
            this.SqlUserGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlUserTextBox = new System.Windows.Forms.TextBox();
            this.SqlDatabaseGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlDataComboBox = new System.Windows.Forms.ComboBox();
            this.SqlPortGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlPortTextBox = new System.Windows.Forms.TextBox();
            this.SqlServerGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlServerTextBox = new System.Windows.Forms.TextBox();
            this.SqlExecMainGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlCodeGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlCodeTextBox = new System.Windows.Forms.TextBox();
            this.SqlFileGroupBox = new System.Windows.Forms.GroupBox();
            this.SqlFileButton = new System.Windows.Forms.Button();
            this.SqlFileTextBox = new System.Windows.Forms.TextBox();
            this.SqlFileCheckBox = new System.Windows.Forms.CheckBox();
            this.SqlCodeCheckBox = new System.Windows.Forms.CheckBox();
            this.Exec = new System.Windows.Forms.Button();
            this.SqlServerMainGroupBox.SuspendLayout();
            this.SqlPswdGroupBox.SuspendLayout();
            this.SqlUserGroupBox.SuspendLayout();
            this.SqlDatabaseGroupBox.SuspendLayout();
            this.SqlPortGroupBox.SuspendLayout();
            this.SqlServerGroupBox.SuspendLayout();
            this.SqlExecMainGroupBox.SuspendLayout();
            this.SqlCodeGroupBox.SuspendLayout();
            this.SqlFileGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SqlServerMainGroupBox
            // 
            this.SqlServerMainGroupBox.Controls.Add(this.SqlAuthCheckBox);
            this.SqlServerMainGroupBox.Controls.Add(this.SqlGetDbsButton);
            this.SqlServerMainGroupBox.Controls.Add(this.SqlPswdGroupBox);
            this.SqlServerMainGroupBox.Controls.Add(this.SqlUserGroupBox);
            this.SqlServerMainGroupBox.Controls.Add(this.SqlDatabaseGroupBox);
            this.SqlServerMainGroupBox.Controls.Add(this.SqlPortGroupBox);
            this.SqlServerMainGroupBox.Controls.Add(this.SqlServerGroupBox);
            this.SqlServerMainGroupBox.Location = new System.Drawing.Point(10, 10);
            this.SqlServerMainGroupBox.Name = "SqlServerMainGroupBox";
            this.SqlServerMainGroupBox.Size = new System.Drawing.Size(470, 140);
            this.SqlServerMainGroupBox.TabIndex = 5;
            this.SqlServerMainGroupBox.TabStop = false;
            this.SqlServerMainGroupBox.Text = "Microsoft SQL Server";
            // 
            // SqlAuthCheckBox
            // 
            this.SqlAuthCheckBox.AutoSize = true;
            this.SqlAuthCheckBox.Checked = true;
            this.SqlAuthCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SqlAuthCheckBox.Location = new System.Drawing.Point(270, 110);
            this.SqlAuthCheckBox.Name = "SqlAuthCheckBox";
            this.SqlAuthCheckBox.Size = new System.Drawing.Size(178, 17);
            this.SqlAuthCheckBox.TabIndex = 6;
            this.SqlAuthCheckBox.Text = "Use SQL Server authentification";
            this.SqlAuthCheckBox.UseMnemonic = false;
            this.SqlAuthCheckBox.UseVisualStyleBackColor = true;
            this.SqlAuthCheckBox.Click += new System.EventHandler(this.SqlAuthCheckBox_Click);
            // 
            // SqlGetDbsButton
            // 
            this.SqlGetDbsButton.Location = new System.Drawing.Point(10, 107);
            this.SqlGetDbsButton.Name = "SqlGetDbsButton";
            this.SqlGetDbsButton.Size = new System.Drawing.Size(100, 23);
            this.SqlGetDbsButton.TabIndex = 5;
            this.SqlGetDbsButton.Text = "Get Databases";
            this.SqlGetDbsButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SqlGetDbsButton.UseVisualStyleBackColor = true;
            this.SqlGetDbsButton.Click += new System.EventHandler(this.SqlGetDbsButton_Click);
            // 
            // SqlPswdGroupBox
            // 
            this.SqlPswdGroupBox.Controls.Add(this.SqlPswdTextBox);
            this.SqlPswdGroupBox.Location = new System.Drawing.Point(270, 60);
            this.SqlPswdGroupBox.Name = "SqlPswdGroupBox";
            this.SqlPswdGroupBox.Size = new System.Drawing.Size(190, 40);
            this.SqlPswdGroupBox.TabIndex = 4;
            this.SqlPswdGroupBox.TabStop = false;
            this.SqlPswdGroupBox.Text = "SQL Server User Passowrd";
            // 
            // SqlPswdTextBox
            // 
            this.SqlPswdTextBox.Location = new System.Drawing.Point(7, 15);
            this.SqlPswdTextBox.Name = "SqlPswdTextBox";
            this.SqlPswdTextBox.PasswordChar = '*';
            this.SqlPswdTextBox.Size = new System.Drawing.Size(175, 20);
            this.SqlPswdTextBox.TabIndex = 0;
            this.SqlPswdTextBox.Text = "password";
            // 
            // SqlUserGroupBox
            // 
            this.SqlUserGroupBox.Controls.Add(this.SqlUserTextBox);
            this.SqlUserGroupBox.Location = new System.Drawing.Point(270, 15);
            this.SqlUserGroupBox.Name = "SqlUserGroupBox";
            this.SqlUserGroupBox.Size = new System.Drawing.Size(190, 40);
            this.SqlUserGroupBox.TabIndex = 3;
            this.SqlUserGroupBox.TabStop = false;
            this.SqlUserGroupBox.Text = "SQL Server User Name";
            this.SqlUserGroupBox.UseWaitCursor = true;
            // 
            // SqlUserTextBox
            // 
            this.SqlUserTextBox.Location = new System.Drawing.Point(7, 15);
            this.SqlUserTextBox.Name = "SqlUserTextBox";
            this.SqlUserTextBox.Size = new System.Drawing.Size(175, 20);
            this.SqlUserTextBox.TabIndex = 0;
            this.SqlUserTextBox.Text = "sa";
            this.SqlUserTextBox.UseWaitCursor = true;
            // 
            // SqlDatabaseGroupBox
            // 
            this.SqlDatabaseGroupBox.Controls.Add(this.SqlDataComboBox);
            this.SqlDatabaseGroupBox.Location = new System.Drawing.Point(12, 60);
            this.SqlDatabaseGroupBox.Name = "SqlDatabaseGroupBox";
            this.SqlDatabaseGroupBox.Size = new System.Drawing.Size(244, 40);
            this.SqlDatabaseGroupBox.TabIndex = 2;
            this.SqlDatabaseGroupBox.TabStop = false;
            this.SqlDatabaseGroupBox.Text = "Database Name";
            // 
            // SqlDataComboBox
            // 
            this.SqlDataComboBox.FormattingEnabled = true;
            this.SqlDataComboBox.Location = new System.Drawing.Point(7, 15);
            this.SqlDataComboBox.Name = "SqlDataComboBox";
            this.SqlDataComboBox.Size = new System.Drawing.Size(229, 21);
            this.SqlDataComboBox.TabIndex = 0;
            // 
            // SqlPortGroupBox
            // 
            this.SqlPortGroupBox.Controls.Add(this.SqlPortTextBox);
            this.SqlPortGroupBox.Location = new System.Drawing.Point(190, 15);
            this.SqlPortGroupBox.Name = "SqlPortGroupBox";
            this.SqlPortGroupBox.Size = new System.Drawing.Size(65, 40);
            this.SqlPortGroupBox.TabIndex = 1;
            this.SqlPortGroupBox.TabStop = false;
            this.SqlPortGroupBox.Text = "SQL Port";
            // 
            // SqlPortTextBox
            // 
            this.SqlPortTextBox.Location = new System.Drawing.Point(7, 15);
            this.SqlPortTextBox.Name = "SqlPortTextBox";
            this.SqlPortTextBox.Size = new System.Drawing.Size(50, 20);
            this.SqlPortTextBox.TabIndex = 0;
            this.SqlPortTextBox.Text = "1433";
            // 
            // SqlServerGroupBox
            // 
            this.SqlServerGroupBox.Controls.Add(this.SqlServerTextBox);
            this.SqlServerGroupBox.Location = new System.Drawing.Point(12, 15);
            this.SqlServerGroupBox.Name = "SqlServerGroupBox";
            this.SqlServerGroupBox.Size = new System.Drawing.Size(165, 40);
            this.SqlServerGroupBox.TabIndex = 0;
            this.SqlServerGroupBox.TabStop = false;
            this.SqlServerGroupBox.Text = "SQL Server Instance Name";
            // 
            // SqlServerTextBox
            // 
            this.SqlServerTextBox.Location = new System.Drawing.Point(7, 15);
            this.SqlServerTextBox.Name = "SqlServerTextBox";
            this.SqlServerTextBox.Size = new System.Drawing.Size(150, 20);
            this.SqlServerTextBox.TabIndex = 0;
            this.SqlServerTextBox.Text = "SQLSRV";
            // 
            // SqlExecMainGroupBox
            // 
            this.SqlExecMainGroupBox.Controls.Add(this.SqlCodeGroupBox);
            this.SqlExecMainGroupBox.Controls.Add(this.SqlFileGroupBox);
            this.SqlExecMainGroupBox.Controls.Add(this.SqlFileCheckBox);
            this.SqlExecMainGroupBox.Controls.Add(this.SqlCodeCheckBox);
            this.SqlExecMainGroupBox.Location = new System.Drawing.Point(12, 160);
            this.SqlExecMainGroupBox.Name = "SqlExecMainGroupBox";
            this.SqlExecMainGroupBox.Size = new System.Drawing.Size(470, 360);
            this.SqlExecMainGroupBox.TabIndex = 6;
            this.SqlExecMainGroupBox.TabStop = false;
            this.SqlExecMainGroupBox.Text = "T-SQL code execution";
            // 
            // SqlCodeGroupBox
            // 
            this.SqlCodeGroupBox.Controls.Add(this.SqlCodeTextBox);
            this.SqlCodeGroupBox.Location = new System.Drawing.Point(12, 60);
            this.SqlCodeGroupBox.Name = "SqlCodeGroupBox";
            this.SqlCodeGroupBox.Size = new System.Drawing.Size(450, 290);
            this.SqlCodeGroupBox.TabIndex = 3;
            this.SqlCodeGroupBox.TabStop = false;
            this.SqlCodeGroupBox.Text = "T-SQL Code";
            // 
            // SqlCodeTextBox
            // 
            this.SqlCodeTextBox.Location = new System.Drawing.Point(7, 20);
            this.SqlCodeTextBox.Multiline = true;
            this.SqlCodeTextBox.Name = "SqlCodeTextBox";
            this.SqlCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SqlCodeTextBox.Size = new System.Drawing.Size(435, 250);
            this.SqlCodeTextBox.TabIndex = 0;
            // 
            // SqlFileGroupBox
            // 
            this.SqlFileGroupBox.Controls.Add(this.SqlFileButton);
            this.SqlFileGroupBox.Controls.Add(this.SqlFileTextBox);
            this.SqlFileGroupBox.Location = new System.Drawing.Point(10, 70);
            this.SqlFileGroupBox.Name = "SqlFileGroupBox";
            this.SqlFileGroupBox.Size = new System.Drawing.Size(450, 80);
            this.SqlFileGroupBox.TabIndex = 2;
            this.SqlFileGroupBox.TabStop = false;
            this.SqlFileGroupBox.Text = "T-SQL batch file";
            this.SqlFileGroupBox.Visible = false;
            // 
            // SqlFileButton
            // 
            this.SqlFileButton.Location = new System.Drawing.Point(360, 19);
            this.SqlFileButton.Name = "SqlFileButton";
            this.SqlFileButton.Size = new System.Drawing.Size(70, 23);
            this.SqlFileButton.TabIndex = 1;
            this.SqlFileButton.Text = "Browse...";
            this.SqlFileButton.UseVisualStyleBackColor = true;
            this.SqlFileButton.Click += new System.EventHandler(this.SqlFileButton_Click);
            // 
            // SqlFileTextBox
            // 
            this.SqlFileTextBox.Location = new System.Drawing.Point(7, 20);
            this.SqlFileTextBox.Name = "SqlFileTextBox";
            this.SqlFileTextBox.Size = new System.Drawing.Size(345, 20);
            this.SqlFileTextBox.TabIndex = 0;
            // 
            // SqlFileCheckBox
            // 
            this.SqlFileCheckBox.AutoSize = true;
            this.SqlFileCheckBox.Location = new System.Drawing.Point(10, 40);
            this.SqlFileCheckBox.Name = "SqlFileCheckBox";
            this.SqlFileCheckBox.Size = new System.Drawing.Size(145, 17);
            this.SqlFileCheckBox.TabIndex = 1;
            this.SqlFileCheckBox.Text = "Execute T-SQL batch file";
            this.SqlFileCheckBox.UseVisualStyleBackColor = true;
            this.SqlFileCheckBox.CheckedChanged += new System.EventHandler(this.SqlFileCheckBox_CheckedChanged);
            // 
            // SqlCodeCheckBox
            // 
            this.SqlCodeCheckBox.AutoSize = true;
            this.SqlCodeCheckBox.Checked = true;
            this.SqlCodeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SqlCodeCheckBox.Location = new System.Drawing.Point(10, 20);
            this.SqlCodeCheckBox.Name = "SqlCodeCheckBox";
            this.SqlCodeCheckBox.Size = new System.Drawing.Size(126, 17);
            this.SqlCodeCheckBox.TabIndex = 0;
            this.SqlCodeCheckBox.Text = "Execute T-SQL code";
            this.SqlCodeCheckBox.UseVisualStyleBackColor = true;
            this.SqlCodeCheckBox.CheckedChanged += new System.EventHandler(this.SqlCodeCheckBox_CheckedChanged);
            // 
            // Exec
            // 
            this.Exec.Location = new System.Drawing.Point(361, 530);
            this.Exec.Name = "Exec";
            this.Exec.Size = new System.Drawing.Size(100, 23);
            this.Exec.TabIndex = 8;
            this.Exec.Text = "Execute";
            this.Exec.UseVisualStyleBackColor = true;
            this.Exec.UseWaitCursor = true;
            this.Exec.Click += new System.EventHandler(this.Exec_Click);
            // 
            // gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 561);
            this.Controls.Add(this.Exec);
            this.Controls.Add(this.SqlExecMainGroupBox);
            this.Controls.Add(this.SqlServerMainGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "gui";
            this.Text = "RunSQL 6.0.0. Beta";
            this.SqlServerMainGroupBox.ResumeLayout(false);
            this.SqlServerMainGroupBox.PerformLayout();
            this.SqlPswdGroupBox.ResumeLayout(false);
            this.SqlPswdGroupBox.PerformLayout();
            this.SqlUserGroupBox.ResumeLayout(false);
            this.SqlUserGroupBox.PerformLayout();
            this.SqlDatabaseGroupBox.ResumeLayout(false);
            this.SqlPortGroupBox.ResumeLayout(false);
            this.SqlPortGroupBox.PerformLayout();
            this.SqlServerGroupBox.ResumeLayout(false);
            this.SqlServerGroupBox.PerformLayout();
            this.SqlExecMainGroupBox.ResumeLayout(false);
            this.SqlExecMainGroupBox.PerformLayout();
            this.SqlCodeGroupBox.ResumeLayout(false);
            this.SqlCodeGroupBox.PerformLayout();
            this.SqlFileGroupBox.ResumeLayout(false);
            this.SqlFileGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private void newSqlAuthCheckBox_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.GroupBox SqlServerMainGroupBox;
        private System.Windows.Forms.GroupBox SqlServerGroupBox;
        private System.Windows.Forms.TextBox SqlServerTextBox;
        private System.Windows.Forms.GroupBox SqlPortGroupBox;
        private System.Windows.Forms.GroupBox SqlUserGroupBox;
        private System.Windows.Forms.TextBox SqlUserTextBox;
        private System.Windows.Forms.GroupBox SqlDatabaseGroupBox;
        private System.Windows.Forms.ComboBox SqlDataComboBox;
        private System.Windows.Forms.TextBox SqlPortTextBox;
        private System.Windows.Forms.CheckBox SqlAuthCheckBox;
        private System.Windows.Forms.Button SqlGetDbsButton;
        private System.Windows.Forms.GroupBox SqlPswdGroupBox;
        private System.Windows.Forms.TextBox SqlPswdTextBox;
        private System.Windows.Forms.GroupBox SqlExecMainGroupBox;
        private System.Windows.Forms.GroupBox SqlFileGroupBox;
        private System.Windows.Forms.Button SqlFileButton;
        private System.Windows.Forms.TextBox SqlFileTextBox;
        private System.Windows.Forms.CheckBox SqlFileCheckBox;
        private System.Windows.Forms.CheckBox SqlCodeCheckBox;
        private System.Windows.Forms.GroupBox SqlCodeGroupBox;
        private System.Windows.Forms.Button Exec;
        private System.Windows.Forms.TextBox SqlCodeTextBox;
    }
}

