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
    public partial class dstudentForm : Form
    {
        public dstudentForm()
        {
            InitializeComponent();
        }
        private void getAllStudentList(string keyword = "", string val = "")
        {
            sqlCnn.Open();
            if (keyword == "id")
            {
                sqlCmd.CommandText = "select * from dstudent where id='" + val + "'";
            }
            else if (keyword == "fullname")
            {
                sqlCmd.CommandText = "select * from dstudent where fullname like'%" + val + "%'";
            }
            else
            {
                sqlCmd.CommandText = "select * from dstudent order by mark desc";
                //sqlCmd.CommandText = "select * from dstudent order by id desc";
            }
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            lvwStudent.Items.Clear();
            while (dataReader.Read() == true)
            {
                ListViewItem listItem = lvwStudent.Items.Add(dataReader.GetInt32(0).ToString()); //id
                listItem.SubItems.Add(dataReader.GetString(1));//fullname
                listItem.SubItems.Add(dataReader.GetDouble(2).ToString()); //mark               
                listItem.SubItems.Add(dataReader.GetString(3)); //sex               

           listItem.SubItems.Add(dataReader.GetString(4)); //note
            }
            sqlCnn.Close();

            sqlCnn.Open();
            sqlCmd.CommandText = "select count(id) from dstudent";
            dataReader = sqlCmd.ExecuteReader();
            if (dataReader.Read() == true)
            {
                toTal.Text = dataReader.GetInt32(0).ToString();
            }

            sqlCnn.Close();
        }

        //private bool check_value(string s)
        //{
        //    sqlCnn.Open();
        //    sqlCmd.CommandText = "select id from dstudent";
        //    SqlDataReader dataReader = sqlCmd.ExecuteReader();
        //    while (dataReader.Read() == true)
        //    {
        //        if (s.Trim() == dataReader.GetString(0).Trim())
        //        {
        //            sqlCnn.Close();
        //            return false;
        //        }
        //    }
        //    sqlCnn.Close();
        //    return true;
        //}
        //private void btnSave_Click(object sender, EventArgs e)
        //{

        //    if (txtId.Text.Trim() != "")
        //    {
        //        int sex = 0;
        //        if (radMale.Checked)
        //            sex = 0;
        //        else if (radFemale.Checked)
        //            sex = 1;
        //        else
        //            sex = 2;
        //        if (check_value(txtId.Text) == false)
        //        {
        //            if (txtFullName.Text.Trim() == "")
        //            {
        //                MessageBox.Show("fullname is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                sqlCnn.Close();
        //                return;
        //            }
        //            else if (txtMark.Text.Trim() == "")
        //            {
        //                MessageBox.Show("Mark is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                sqlCnn.Close();
        //                return;
        //            }

        //            else
        //            {

        //                sqlCnn.Open();
        //                sqlCmd.CommandText = "update dstudent set fullname='" + txtFullName.Text + "'," +
        //                        "mark='" + txtMark.Text.Trim() + "'," +
        //                        "gender='" + sex + "'," +
        //                        "note='" + txtNote.Text.Trim() + "'" +
        //                        "where id='" + txtId.Text.Trim() + "'";
        //                sqlCnn.Close();
        //            }
        //        }
        //        else
        //        {
        //            if (txtFullName.Text.Trim() == "")
        //            {
        //                MessageBox.Show("fullname is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                sqlCnn.Close();
        //                return;
        //            }
        //            else if (txtMark.Text.Trim() == "")
        //            {
        //                MessageBox.Show("Mark is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                sqlCnn.Close();
        //                return;
        //            }
        //            else
        //            {
        //                sqlCnn.Open();
        //                sqlCmd.CommandText = "insert into dstudent (id, fullname, mark, gender, note) values ('" + txtId.Text.Trim() + "'," +
        //                "'" + txtFullName.Text.Trim() + "','" + txtMark.Text.Trim() + "','" + sex + "','" + txtNote.Text.Trim() + "')";
        //                sqlCnn.Close();
        //            }
        //        }
        //    }
        //    sqlCnn.Open();
        //    int i = sqlCmd.ExecuteNonQuery();
        //    if (i == 1)
        //    {
        //        MessageBox.Show("Save successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Save unsuccessfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    sqlCnn.Close();
        //    getAllStudentList();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sex = "Male";
            if (radMale.Checked)
                sex = "Male";
            else if (radFemale.Checked)
                sex = "Female";
            else
                sex = "Other";

            if (txtId.Text.Trim() == "")
            {
                if (txtFullName.Text.Trim() == "")
                {
                    MessageBox.Show("fullname is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtMark.Text.Trim() == "")
                {
                    MessageBox.Show("Mark is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (radMale.Checked == false && radFemale.Checked == false && radOther.Checked == false)
                {
                    MessageBox.Show("sex is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    sqlCnn.Open();
                    sqlCmd.CommandText = "insert into dstudent (fullname, mark, gender, note) values" +
                        " ( N'" + txtFullName.Text.Trim() + "','" + txtMark.Text.Trim() + "','" + sex + "',N'" + txtNote.Text.Trim() + "')";
                    sqlCnn.Close();

                }
            }
            else
            {
                if (txtFullName.Text.Trim() == "")
                {
                    MessageBox.Show("fullname is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtMark.Text.Trim() == "")
                {
                    MessageBox.Show("Mark is not null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    sqlCnn.Open();
                    sqlCmd.CommandText = "update dstudent set fullname=N'" + txtFullName.Text + "'," +
                            "mark='" + txtMark.Text.Trim() + "'," +
                            "gender='" + sex + "'," +
                            "note=N'" + txtNote.Text.Trim() + "'" +
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
            getAllStudentList();
        }

        private void dstudentForm_Load(object sender, EventArgs e)
        {
            getAllStudentList();
        }

        private void lvwStudent_DoubleClick(object sender, EventArgs e)
        {

            string did = lvwStudent.SelectedItems[0].Text;
            if (did.Trim() != "")
            {
                sqlCnn.Open();
                sqlCmd.CommandText = "select * from dstudent where id='" + did + "'";
                SqlDataReader dataReader = sqlCmd.ExecuteReader();
                if (dataReader.Read() == true)
                {
                    txtId.Text = dataReader.GetInt32(0).ToString();
                    txtFullName.Text = dataReader.GetString(1);
                    txtMark.Text = dataReader.GetDouble(2).ToString();
                    txtNote.Text = dataReader.GetString(4);
                    if (dataReader.GetString(3).Trim() == "Male")
                        radMale.Checked = true;
                    else if (dataReader.GetString(3).Trim() == "Female")
                        radFemale.Checked = true;
                    else
                        radOther.Checked = true;
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
                    getAllStudentList("id", txtSearch.Text);
                else
                {
                    MessageBox.Show("Input number, please", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


            }
            else if (cbxSearch.Text == "fullname")
            {
                getAllStudentList("fullname", txtSearch.Text);
            }
            else
            {
                getAllStudentList();
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
            if (txtId.Text.Trim() == "")
            {
                MessageBox.Show("input id, please!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                sqlCnn.Open();
                sqlCmd.CommandText = "delete from dstudent where id='" + txtId.Text.Trim() + "'";
                sqlCnn.Close();
            }
            DialogResult dialog = MessageBox.Show("are you delete ?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                sqlCnn.Open();
                int i = sqlCmd.ExecuteNonQuery();
                sqlCnn.Close();
                if (i == 1)
                {
                    MessageBox.Show("Delete successfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete unsuccessfully!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            getAllStudentList();
        }


    }
}
