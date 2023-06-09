﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Achievement_Management_System.Grade
{
    public partial class Change_Grade_Information : Form
    {
        public static string strConn = "Data Source=DESKTOP-SK9ALMG;Initial Catalog = Management_System; Integrated Security = True";

        //传递view_grade中的值，然后进行修改
        public string strSchema = "";

        public string strGaeID = "";
        public string strID = "";
        public string strCueName = "";
        public string strSdtID = "";
        public string strSdtName = "";
        public string strGrade = "";
        public Change_Grade_Information()
        {
            InitializeComponent();
        }

        private void Change_Grade_Information_Load(object sender, EventArgs e)
        {
            if (strSchema == "college") 
            {
                this.lblID.Text = "学院编号：";
            }else if(strSchema == "major") 
            {
                this.lblID.Text = "专业编号：";
            }
            else 
            {
                this.lblID.Text = "班级编号：";
            }
            this.lblGaeID.Text=strGaeID.Trim();
            this.txtID.Text= strID.Trim();
            this.txtCueName.Text = strCueName.Trim();
            this.txtSdtID.Text = strSdtID.Trim();
            this.txtSdtName.Text = strSdtName.Trim();
            this.txtCueGrade.Text = strGrade.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtID.Text.Trim()=="" ||this.txtCueName.Text.Trim() == "" || this.txtSdtID.Text.Trim() == "" || this.txtSdtName.Text.Trim() == "" || this.txtCueGrade.Text.Trim() == "")
            {
                MessageBox.Show("请输入要修改成绩的完整信息!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    };

                    try
                    {
                        bool flag = true;

                        SqlCommand cmd = new SqlCommand("SELECT * FROM student WHERE student_id='" + this.txtSdtID.Text.Trim() + "';", con);
                        if (cmd.ExecuteScalar() == null)
                        {
                            MessageBox.Show("不存在该学生！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            flag = false;
                        };                 

                        cmd.CommandText = "SELECT * FROM student WHERE Sname='" + this.txtSdtName.Text.Trim() + "';";
                        if (cmd.ExecuteScalar() == null)
                        {
                            MessageBox.Show("不存在该学生！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            flag = false;
                        };

                        cmd.CommandText = "SELECT * FROM Course WHERE cue_name='" + this.txtCueName.Text.Trim() + "';";
                        if (cmd.ExecuteScalar() == null)
                        {
                            MessageBox.Show("不存在该课程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            flag = false;
                        };

                        //判断添加的信息的上级信息是否存在


                        if (flag)
                        {
                            if (strSchema == "college")                 //按学院显示成绩的修改
                            {
                                cmd.CommandText = "SELECT * FROM College WHERE college_id='" + this.txtID.Text.Trim() + "';";
                                if (cmd.ExecuteScalar() == null)
                                {
                                    MessageBox.Show("不存在该学院！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else 
                                {
                                    string sql1 = "UPDATE College SET college_id='" + this.txtID.Text.Trim() + "' WHERE college_id='" + strID + "';";
                                    cmd.CommandText = sql1;

                                    string sql2 = "UPDATE Course SET cue_name='" + this.txtCueName.Text.Trim() + "' WHERE cue_name='" + strCueName + "';";
                                    string sql3 = "UPDATE student SET student_id='" + this.txtSdtID.Text.Trim() + "' WHERE student_id='" + strSdtID + "';";
                                    string sql4 = "UPDATE student SET Sname='" + this.txtSdtName.Text.Trim() + "' WHERE Sname='" + strSdtName + "';";
                                    string sql5 = "UPDATE Grade SET cue_grade='" + this.txtCueGrade.Text.Trim() + "' WHERE Grade_id='" + strGaeID + "';";

                                    cmd.CommandText = sql2;
                                    cmd.CommandText = sql3;
                                    cmd.CommandText = sql4;
                                    cmd.CommandText = sql5;

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("修改个人成绩信息成功！，请点击“开始查询”更新成绩信息。", "提示", MessageBoxButtons.OK);
                                }
                            }
                            else if (strSchema == "major")                 //按专业显示成绩的修改
                            {
                                cmd.CommandText = "SELECT * FROM Major WHERE major_id='" + this.txtID.Text.Trim() + "';";
                                if (cmd.ExecuteScalar() == null)
                                {
                                    MessageBox.Show("不存在该专业！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else 
                                {
                                    string sql1 = "UPDATE Major SET major_id='" + this.txtID.Text.Trim() + "' WHERE major_id='" + strID + "';";
                                    cmd.CommandText = sql1;

                                    string sql2 = "UPDATE Course SET cue_name='" + this.txtCueName.Text.Trim() + "' WHERE cue_name='" + strCueName + "';";
                                    string sql3 = "UPDATE student SET student_id='" + this.txtSdtID.Text.Trim() + "' WHERE student_id='" + strSdtID + "';";
                                    string sql4 = "UPDATE student SET Sname='" + this.txtSdtName.Text.Trim() + "' WHERE Sname='" + strSdtName + "';";
                                    string sql5 = "UPDATE Grade SET cue_grade='" + this.txtCueGrade.Text.Trim() + "' WHERE Grade_id='" + strGaeID + "';";                                   

                                    cmd.CommandText = sql2;
                                    cmd.CommandText = sql3;
                                    cmd.CommandText = sql4;
                                    cmd.CommandText = sql5;

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("修改个人成绩信息成功！，请点击“开始查询”更新成绩信息。", "提示", MessageBoxButtons.OK);
                                }
                            }
                            else                  //按班级显示成绩的修改
                            {
                                cmd.CommandText = "SELECT * FROM Class WHERE class_id='" + this.txtID.Text.Trim() + "';";
                                if (cmd.ExecuteScalar() == null)
                                {
                                    MessageBox.Show("不存在该班级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else 
                                {
                                    string sql1 = "UPDATE Class SET class_id='" + this.txtID.Text.Trim() + "' WHERE class_id='" + strID + "';";
                                    cmd.CommandText = sql1;

                                    string sql2 = "UPDATE Course SET cue_name='" + this.txtCueName.Text.Trim() + "' WHERE cue_name='" + strCueName + "';";
                                    string sql3 = "UPDATE student SET student_id='" + this.txtSdtID.Text.Trim() + "' WHERE student_id='" + strSdtID + "';";
                                    string sql4 = "UPDATE student SET Sname='" + this.txtSdtName.Text.Trim() + "' WHERE Sname='" + strSdtName + "';";
                                    string sql5 = "UPDATE Grade SET cue_grade='" + this.txtCueGrade.Text.Trim() + "' WHERE Grade_id='" + strGaeID + "';";                                 

                                    cmd.CommandText = sql2;
                                    cmd.CommandText = sql3;
                                    cmd.CommandText = sql4;
                                    cmd.CommandText = sql5;                                 

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("修改个人成绩信息成功！，请点击“开始查询”更新成绩信息。", "提示", MessageBoxButtons.OK);
                                }
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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
