using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaptonghop
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }


        
        private void mnuFileLogin_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Login";
            loginForm login = new loginForm();
            login.ShowDialog();
            //MessageBox.Show(login.loginUser.username);

            if (login.loginUser.id > 0)
            {
                mnuManageStudent.Enabled = true;
                mnuManageUser.Enabled = true;
                mnuFileLogin.Visible = false;
                mnuFileLogout.Visible = true;
                toolStripStatusLabel.Text = "";
                toolStripStatusLabel1.Text = login.loginUser.fullname;
            }


        }

        private void mnuFileLogout_Click(object sender, EventArgs e)
        {
            mnuManageStudent.Enabled = false;
            mnuManageUser.Enabled = false;
            mnuFileLogin.Visible = true;
            mnuFileLogout.Visible = false;
            toolStripStatusLabel1.Text = "";
        }

        private void mnuFileLogin_OwnerChanged(object sender, EventArgs e)
        {
           
        }

        private void mnuManageUser_Click(object sender, EventArgs e)
        {
            duserForm duserForm = new duserForm();
            duserForm.ShowDialog();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you exit?","Exit",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                this.Close();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void mnuManageStudent_Click(object sender, EventArgs e)
        {
            dstudentForm dstudentForm = new dstudentForm();
            dstudentForm.ShowDialog();
        }
    }
}
