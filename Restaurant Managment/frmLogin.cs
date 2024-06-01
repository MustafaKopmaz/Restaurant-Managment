using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Managment
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (MainClass.isValidUser(txtUser.Text,txtPass.Text) == false)
            {
                guna2MessageDialog1.Show("Kullanıcı adı veya şifre yanlış");
                return;
            }
            else
            {
                this.Hide();
                frmMain frmMain = new frmMain();
                frmMain.Show();
            }
        }
    }
}
