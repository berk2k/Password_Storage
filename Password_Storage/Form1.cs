using System;
using System.Data;
using System.Windows.Forms;

namespace Password_Storage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            string account = txtAccount.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Account and Password fields cannot be empty");
                return;
            }

            
            SavePassword(account, username, password);
            MessageBox.Show("Password saved successfully!");
            ClearFields();
        }

        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            
            LoadPasswords();
        }

        
        private void SavePassword(string account, string username, string password)
        {
            // Daha sonra veritaban� kayd� veya API �a�r�s� ile �ifreleri kaydedece�iz
        }

        // �ifreleri y�kleme i�lemi
        private void LoadPasswords()
        {
            // Daha sonra veritaban�ndan veya API'den �ifreleri �ekece�iz
        }

        // Alanlar� temizleme
        private void ClearFields()
        {
            txtAccount.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
