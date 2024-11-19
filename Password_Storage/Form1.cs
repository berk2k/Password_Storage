using Newtonsoft.Json;
using PasswordStorage.SharedModels;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Password_Storage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadAccounts();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            string selectedAccount = cmbAccounts.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedAccount))
            {
                
                SearchPasswordByAccountNameAsync(selectedAccount);
            }
            else
            {
                MessageBox.Show("Please select an account.");
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (dgvPasswords.SelectedRows.Count > 0)
            {
                // Get the account name from the selected row
                string accountName = dgvPasswords.SelectedRows[0].Cells["AccountName"].Value.ToString();

                // Confirm the deletion with the user
                var confirmResult = MessageBox.Show($"Are you sure you want to delete the account {accountName}?",
                                                    "Confirm Delete",
                                                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Call API to delete by account name
                    DeletePasswordByAccountName(accountName);
                    MessageBox.Show("Password deleted successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
        private void cmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedAccount = cmbAccounts.SelectedItem.ToString();
            SearchPasswordByAccountNameAsync(selectedAccount);
        }

        
        private async void SearchPasswordByAccountNameAsync(string accountName)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7162/api/Password/search/{accountName}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var passwords = JsonConvert.DeserializeObject<List<PasswordDto>>(result);

                dgvPasswords.DataSource = passwords;
            }
            else
            {
                MessageBox.Show("Failed to load passwords for the selected account.");
            }
        }

        // Method to call the API and delete the password by account name
        private async void DeletePasswordByAccountName(string accountName)
        {
            // Your logic to send a DELETE request to the API endpoint
            var client = new HttpClient();
            var response = await client.DeleteAsync($"https://localhost:7162/api/Password/delete/{accountName}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Password deleted successfully!");
                LoadPasswords(); // Refresh the grid after deletion
            }
            else
            {
                MessageBox.Show("Error deleting password");
            }
        }

        private async void LoadAccounts()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7162/api/Password/accounts");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var accounts = JsonConvert.DeserializeObject<List<string>>(result);

                cmbAccounts.DataSource = accounts; 
            }
            else
            {
                MessageBox.Show("Failed to load accounts.");
            }
        }





        private async void SavePassword(string account, string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7162/");
                var passwordData = new
                {
                    AccountName = account,
                    Username = username,
                    Password = password
                };

                
                var content = new StringContent(JsonConvert.SerializeObject(passwordData), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Password", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Password saved successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to save the password.");
                }
            }
        }


        private async void LoadPasswords()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7162"); 
                var response = await client.GetAsync("/api/Password");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var passwords = JsonConvert.DeserializeObject<List<PasswordDto>>(result);

                    
                    dgvPasswords.DataSource = passwords;
                }
                else
                {
                    MessageBox.Show("Failed to load passwords.");
                }
            }
        }


        
        private void ClearFields()
        {
            txtAccount.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
