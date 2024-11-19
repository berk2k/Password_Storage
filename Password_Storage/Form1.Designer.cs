namespace Password_Storage
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbAccounts;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvPasswords;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvPasswords = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbAccounts = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasswords)).BeginInit();
            this.SuspendLayout();

            // txtAccount
            this.txtAccount.Location = new System.Drawing.Point(12, 12);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.PlaceholderText = "Account Name";
            this.txtAccount.Size = new System.Drawing.Size(200, 23);
            this.txtAccount.TabIndex = 0;

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(12, 41);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderText = "Username";
            this.txtUsername.Size = new System.Drawing.Size(200, 23);
            this.txtUsername.TabIndex = 1;

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(12, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            this.txtPassword.TabIndex = 2;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(12, 99);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnLoad
            this.btnLoad.Location = new System.Drawing.Point(12, 128);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load Passwords";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // dgvPasswords
            this.dgvPasswords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPasswords.Location = new System.Drawing.Point(12, 200);
            this.dgvPasswords.Name = "dgvPasswords";
            this.dgvPasswords.RowTemplate.Height = 25;
            this.dgvPasswords.Size = new System.Drawing.Size(360, 150);
            this.dgvPasswords.TabIndex = 5;

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(220, 99);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // cmbAccounts
            this.cmbAccounts.FormattingEnabled = true;
            this.cmbAccounts.Location = new System.Drawing.Point(220, 12);
            this.cmbAccounts.Name = "cmbAccounts";
            this.cmbAccounts.Size = new System.Drawing.Size(152, 23);
            this.cmbAccounts.TabIndex = 7;

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(220, 41);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(152, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbAccounts);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvPasswords);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtAccount);
            this.Name = "Form1";
            this.Text = "Password Storage";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasswords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
