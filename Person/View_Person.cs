﻿using Achievement_Management_System.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Achievement_Management_System.Person
{

    public partial class View_Person : Form
    {
        public static string strConn = "Data Source=DESKTOP-SK9ALMG;Initial Catalog = Management_System; Integrated Security = True";

        public View_Person()
        {
            InitializeComponent();
        }
        private void View_Person_Load(object sender, EventArgs e)
        {
            showinf();
        }


        private void dgvPerson_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void showinf() 
        {
            using(SqlConnection con=new SqlConnection(strConn)) 
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                };

                try
                {
                    string sql = "SELECT student_id AS 学号,Sname AS 姓名,gender AS 性别,age AS 年龄,adress AS 家庭地址,phone AS 电话号码,class_id AS 班级ID FROM student ORDER BY student_id ASC";
                    SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();
                    ds.Clear();
                    adp.Fill(ds, "person");
                    this.dgvPerson.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误：" + ex.Message, "提示错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(this.dgvPerson.CurrentCell != null) 
            {
                personAddUpdate personAddUpdate = new personAddUpdate();
                personAddUpdate.Text = "                                     修改个人信息";
                personAddUpdate.lblSdtID.Text= this.dgvPerson[0, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                personAddUpdate.txtSdtName.Text= this.dgvPerson[1, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                personAddUpdate.cmbGender.Text= this.dgvPerson[2, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                personAddUpdate.txtAge.Text= this.dgvPerson[3, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                personAddUpdate.txtAdress.Text= this.dgvPerson[4, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                personAddUpdate.txtPhone.Text= this.dgvPerson[5, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                personAddUpdate.txtClassId.Text= this.dgvPerson[6, this.dgvPerson.CurrentCell.RowIndex].Value.ToString();
                
                personAddUpdate.strSchema = "Update";
                personAddUpdate.strSdtID = true;
                personAddUpdate.txtSdtId.Visible = false;

                personAddUpdate.Owner = this;
                personAddUpdate.StartPosition = FormStartPosition.CenterScreen;
                personAddUpdate.ShowDialog();
                if(personAddUpdate.DialogResult == DialogResult.OK) 
                {
                    showinf();
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(strConn))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                };

                try
                {
                    if (this.dgvPerson.CurrentCell != null)
                    {
                        string sql = "SELECT * FROM student s INNER JOIN Grade g ON s.student_id=g.student_id WHERE s.student_id='" +
                                this.dgvPerson[0, this.dgvPerson.CurrentCell.RowIndex].Value.ToString().Trim() + "';";

                        SqlCommand cmd = new SqlCommand(sql, con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            MessageBox.Show("删除学生:" + this.dgvPerson[1, this.dgvPerson.CurrentCell.RowIndex].Value.ToString().Trim() + "失败,请先删除与此学生相关的成绩！", "错误提示",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dr.Close();
                            sql = "DELETE FROM student WHERE student_id='" + this.dgvPerson[0, this.dgvPerson.CurrentCell.RowIndex].Value.ToString().Trim() + "';";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("删除学生:" + this.dgvPerson[1, this.dgvPerson.CurrentCell.RowIndex].Value.ToString().Trim() + "成功!", "提示", MessageBoxButtons.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误：" + ex.Message, "错误提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            showinf();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            showinf();
        }
    }
}
