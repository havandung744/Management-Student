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
    public partial class duserForm : Form
    {
        public duserForm()
        {
            InitializeComponent();
        }

        private void getAllUserList(string keyword = "", string val = "")
        {
            sqlCnn.Open();
            if (keyword == "id")
            {
                sqlCmd.CommandText = "select * from duser where id='" + val + "'";
            }
            else if (keyword == "fullname")
            {
                sqlCmd.CommandText = "select * from duser where fullname like'%" + val + "%'";
            }
            else
            {
                sqlCmd.CommandText = "select * from duser";
            }
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            lvwUser.Items.Clear();
            while (dataReader.Read() == true)
            {
                ListViewItem listItem = lvwUser.Items.Add(dataReader.GetInt32(0).ToString()); //id
                listItem.SubItems.Add(dataReader.GetString(1)); //fullname
                listItem.SubItems.Add(dataReader.GetString(2)); //username
                listItem.SubItems.Add(dataReader.GetDateTime(4).ToShortDateString()); //birthdate
            }
            sqlCnn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim() == "")
            {
                sqlCnn.Open();
                sqlCmd.CommandText = "select * from duser where username ='" + txtUser.Text + "'";
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                if (dataReader.Read() == true)
                {
                    MessageBox.Show("username is ton tai!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else if (txtFullName.Text.Trim() == "")
                {
                    MessageBox.Show("fullname is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else if (txtUser.Text.Trim() == "")
                {
                    MessageBox.Show("username is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("passsword is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else
                {
                    sqlCmd.CommandText = "insert into duser (fullname, username, password, birthdate, note) values ('" + txtFullName.Text.Trim() + "'," +
                    "'" + txtUser.Text.Trim() + "','" + txtPassword.Text.Trim() + "','" + txtDatebirth.Text.Trim() + "','" + txtNote.Text.Trim() + "')";
                    //dataReader.Close();
                    sqlCnn.Close();
                }
            }
            else
            {
                sqlCnn.Open();
                if (txtFullName.Text.Trim() == "")
                {
                    MessageBox.Show("fullname is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else if (txtUser.Text.Trim() == "")
                {

                    MessageBox.Show("username is not null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("passsword is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sqlCnn.Close();
                    return;
                }
                else
                {
                    sqlCmd.CommandText = "update duser set fullname='" + txtFullName.Text + "'," +
                        "username='" + txtUser.Text.Trim() + "'," +
                        "password='" + txtPassword.Text.Trim() + "'," +
                        "birthdate='" + txtDatebirth.Text.Trim() + "'," +
                        "note='" + txtNote.Text.Trim() + "'" +
                        "where id='" + txtId.Text.Trim() + "'";
                    sqlCnn.Close();
                }

            }
            sqlCnn.Open();
            int i = sqlCmd.ExecuteNonQuery();
            if (i == 1)
            {
                MessageBox.Show("Save successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save unsuccessfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlCnn.Close();
            getAllUserList();
        }

        private void duserForm_Load(object sender, EventArgs e)
        {
            getAllUserList();
            txtDatebirth.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void lvwUser_DoubleClick(object sender, EventArgs e)
        {
            string did = lvwUser.SelectedItems[0].Text;
            if (did.Trim() != "")
            {
                sqlCnn.Open();
                sqlCmd.CommandText = "select * from duser where id='" + did + "'";
                SqlDataReader dataReader = sqlCmd.ExecuteReader();
                if (dataReader.Read() == true)
                {
                    txtId.Text = dataReader.GetInt32(0).ToString();
                    txtFullName.Text = dataReader.GetString(1);
                    txtUser.Text = dataReader.GetString(2);
                    txtPassword.Text = dataReader.GetString(3);
                    txtDatebirth.Text = dataReader.GetDateTime(4).ToShortDateString();
                    txtNote.Text = dataReader.GetString(5);
                }
                sqlCnn.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbxSearch.Text == "id")
            {
                int temp;
                try
                {
                    temp = 0;
                    int number = int.Parse(txtSearch.Text);
                }
                catch
                {
                    temp = 1;
                }
                if (temp == 0)
                    getAllUserList("id", txtSearch.Text);
                else
                {
                    MessageBox.Show("Input number, please", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


            }
            else if (cbxSearch.Text == "fullname")
            {
                getAllUserList("fullname", txtSearch.Text);
            }
            else
            {
                getAllUserList();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlCnn.Open();
            if (txtId.Text.Trim() == "")
            {
                MessageBox.Show("input id, please!");
                sqlCnn.Close();
                return;
            }
            else
            {
                sqlCmd.CommandText = "delete from duser where id='" + txtId.Text.Trim() + "'";
            }
            DialogResult dialog = MessageBox.Show("are you delete ?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int i = sqlCmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Delete successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete unsuccessfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            sqlCnn.Close();
            getAllUserList();
        }

        private void lab_DateBirth_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //txtDatebirth.Text = monthCalendar1.SelectionStart.ToShortDateString();
            //monthCalendar1.Visible =false;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDatebirth.Text = monthCalendar1.SelectionStart.ToShortDateString();
            monthCalendar1.Visible = false;
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                txtUser.Enabled = false;
            }
            else
            {
                txtUser.Enabled = true;
            }
        }
    }
}
