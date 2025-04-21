using System;
using System.Linq;
using System.Windows.Forms;
using FinancialCrm.Models; // Entity Framework namespace

namespace FinancialCrm
{
    public partial class FrmLogin : Form
    {
        FinancialCrmDbEntities db = new FinancialCrmDbEntities();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Kullanıcı doğrulama işlemi
            var user = db.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {
                MessageBox.Show("Giriş İşlemi Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ana forma geçiş
                FrmBanks mainForm = new FrmBanks();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
    }
}