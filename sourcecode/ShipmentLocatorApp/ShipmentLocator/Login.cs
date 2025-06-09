using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
//using static elysium.crypt.crypto;

namespace ShipmentLocator
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLogin())
                {
                    this.Close();
                }
                else
                {
                    throw new Exception("Login failed");
                }                
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateLogin()
        {
            var blnLogin = false;
            try
            {
                var password = txtPassword.Text; // The password is "password"

                // This encrypted password would be read 
                // from a database based on the username
                var storedEncrPassw = ReadEncryptedValueFromDatabase;

                if (ValidateEncryptedData(password, storedEncrPassw))
                {
                    blnLogin = true;
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
            return blnLogin;
        }

        /// <summary>
        /// Encrypt any value by typing: var encrPassword = EncryptData(password);
        /// </summary>
        private string ReadEncryptedValueFromDatabase => "/goZ6Fmv1HQdjLk/JOxEb5KtE9RYCX+kLBpDN2UUwUc=:yiy7bmLTg3Gm5jcy41dNQcww0gGSeu2m0FHnK4/reNA=";

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!ValidateLogin())
            {
                // Cancel the Closing event from closing the form.
                e.Cancel = true;
            }
        }

        private static bool ValidateEncryptedData(string clearTextValueToValidate, string encryptedDatabaseValue)
        {
            try
            {
                var arrValues = encryptedDatabaseValue.Split(':');
                var encryptedDbValue = arrValues[0];
                var salt = arrValues[1];

                var saltedValue = Encoding.UTF8.GetBytes(salt + clearTextValueToValidate);

                using (var hashstr = SHA256.Create()) // Modern, cross-platform
                {
                    var hash = hashstr.ComputeHash(saltedValue);
                    var enteredValueToValidate = Convert.ToBase64String(hash);
                    return encryptedDbValue.Equals(enteredValueToValidate);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
