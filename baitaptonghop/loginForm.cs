using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace baitaptonghop
{
    public partial class loginForm : Form
    {
        public duser loginUser;
        public loginForm()
        {
            InitializeComponent();
            loginUser = new duser();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUser.Text.Trim() == "")
            {
                MessageBox.Show("Username is not null!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             if(txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Password is not null!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            sqlCnn.Open();
            sqlCmd.CommandText = "select * from duser where username = '" + txtUser.Text.Trim() + "'" +
                "and password = '" + txtPassword.Text.Trim() + "'";
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            if (dataReader.Read())
            {
                loginUser.id = dataReader.GetInt32(0);
                loginUser.fullname = dataReader.GetString(1);
                loginUser.username = dataReader.GetString(2);
                loginUser.password = dataReader.GetString(3);
                loginUser.birthdate = dataReader.GetDateTime(4);
                loginUser.note = dataReader.GetString(5);
                MessageBox.Show("login successfully!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                loginUser.id = -1;
                MessageBox.Show("Username or password is incorrect!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            sqlCnn.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                this.Close();
        }
    }
}
