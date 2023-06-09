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
using Achievement_Management_System.Class;
using Achievement_Management_System.College;
using Achievement_Management_System.Course;
using Achievement_Management_System.Grade;
using Achievement_Management_System.Major;
using Achievement_Management_System.Person;
using Achievement_Management_System.Select;
using Achievement_Management_System.USER;

namespace Achievement_Management_System
{
    public partial class Main_page : Form
    {
        public Main_page()
        {
            InitializeComponent();
        }

        public string strIdentified = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Now_Time.Text = "时间为：" + DateTime.Now.ToString();
        }

        private void Main_page_Load(object sender, EventArgs e)
        {
            //系统权限的赋予

            this.Text = "                                                                                                                 学校信息管理系统主页面(" + strIdentified + "端)";
            if (strIdentified == "学生") 
            {
                this.添加学院ToolStripMenuItem.Visible = false;
                this.添加专业ToolStripMenuItem.Visible = false;
                this.添加班级ToolStripMenuItem.Visible=false;
                this.添加课程ToolStripMenuItem.Visible=false;
                this.添加学生成绩ToolStripMenuItem.Visible = false;
                this.添加个人信息ToolStripMenuItem.Visible=false;

            }
            else if(strIdentified == "老师") 
            {
                this.添加学院ToolStripMenuItem.Visible = false;
                this.添加专业ToolStripMenuItem.Visible = false;
                this.添加班级ToolStripMenuItem.Visible = false;
                this.添加课程ToolStripMenuItem.Visible = false;
            }
           
        }

        private void 学院管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 添加学院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollegeAddUpdate collegeAdd = new CollegeAddUpdate();
            collegeAdd.MdiParent = this;
            collegeAdd.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 查看学院情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_College view_College = new View_College();
            view_College.MdiParent = this;
;
            if (strIdentified == "学生" || strIdentified=="老师")       //限制权限
            {
                view_College.btnUpdate.Visible = false;
                view_College.btnDel.Visible = false;
            }

            view_College.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 添加专业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MajorAddUpdate majorAdd = new MajorAddUpdate();
  
            majorAdd.MdiParent = this;

            majorAdd.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 查看专业情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Major view_Major = new View_Major();
 
            view_Major.MdiParent = this;

            if (strIdentified == "学生" || strIdentified == "老师")   //限制权限
            {
                view_Major.btnDel.Visible = false;
                view_Major.btnUpdate.Visible = false;
            }

            view_Major.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 添加班级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassAddUpdate classAdd = new ClassAddUpdate();

            classAdd.MdiParent = this;

            classAdd.Show();
            LayoutMdi(MdiLayout.Cascade);

        }

        private void 查看班级情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Class view_Class = new View_Class();

            view_Class.MdiParent = this;

            if (strIdentified == "学生" || strIdentified == "老师")   //限制权限
            {
                view_Class.btnUpdate.Visible=false;
                view_Class.btnDel.Visible=false;
            }

            view_Class.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 添加个人信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            personAddUpdate personAdd= new personAddUpdate();

            personAdd.MdiParent = this;

     

            personAdd.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 查看个人信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Person view_Person = new View_Person();

            if (strIdentified == "学生")           //限制权限
            {
                view_Person.btnUpdate.Visible=false;
                view_Person.btnDel.Visible=false;
            }

            view_Person.MdiParent = this;   
            view_Person.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 添加课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CourseAddUpdate courseAdd= new CourseAddUpdate();

            courseAdd.MdiParent = this;
            courseAdd.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 查看课程信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Course view_Course = new View_Course();

            if (strIdentified == "学生" || strIdentified == "老师")      //限制权限
            {
                view_Course.btnUpdate.Visible = false;
                view_Course.btnDel.Visible = false;
            }

            view_Course.MdiParent = this;
            view_Course.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 添加学生成绩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GradeAdd gradeAdd= new GradeAdd();

            gradeAdd.MdiParent = this;
            gradeAdd.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 成绩学院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Grade grade_College = new View_Grade();
            
            grade_College.strSchema = "college";            //成绩识别条件

            string strConn = "Data Source=DESKTOP-SK9ALMG;Initial Catalog = Management_System; Integrated Security = True";

            using (SqlConnection con = new SqlConnection(strConn))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                };

                SqlCommand cmd = new SqlCommand("SELECT Sname FROM College", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                grade_College.cmbCollege.DisplayMember = "Sname";
                grade_College.cmbCollege.ValueMember = "college_id";
                grade_College.cmbCollege.DataSource = ds.Tables[0].DefaultView;
            }

            grade_College.MdiParent=this;

            if (strIdentified == "学生")               //限制权限
            {
                grade_College.btnUpdate.Visible = false;
                grade_College.btnDel.Visible = false;
            }

            grade_College.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 成绩专业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Grade grade_College = new View_Grade();

            string strConn = "Data Source=DESKTOP-SK9ALMG;Initial Catalog = Management_System; Integrated Security = True";
            
            grade_College.strSchema = "major";           //成绩识别条件

            using (SqlConnection con = new SqlConnection(strConn))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                };

                SqlCommand cmd = new SqlCommand("SELECT Cname FROM Major", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                grade_College.cmbCollege.DisplayMember = "Cname";
                grade_College.cmbCollege.ValueMember = "major_id";
                grade_College.cmbCollege.DataSource = ds.Tables[0].DefaultView;
            }
            
            grade_College.MdiParent = this;

            if (strIdentified == "学生")          //限制系统权限
            {
                grade_College.btnUpdate.Visible = false;
                grade_College.btnDel.Visible = false;
            }

            grade_College.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 成绩班级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Grade grade_College = new View_Grade();

            string strConn = "Data Source=DESKTOP-SK9ALMG;Initial Catalog = Management_System; Integrated Security = True";

            grade_College.strSchema = "class";      //成绩识别条件

            using (SqlConnection con = new SqlConnection(strConn))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                };

                SqlCommand cmd = new SqlCommand("SELECT Class_name FROM Class", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                grade_College.cmbCollege.DisplayMember = "class_name";
                grade_College.cmbCollege.ValueMember = "class_id";
                grade_College.cmbCollege.DataSource = ds.Tables[0].DefaultView;
            }
            
            grade_College.MdiParent= this;

            if (strIdentified == "学生")          //限制系统权限
            {
                grade_College.btnUpdate.Visible = false;
                grade_College.btnDel.Visible = false;
            }

            grade_College.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Select_Information select_Information = new Select_Information();
           
            select_Information.MdiParent = this;
            select_Information.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 关于系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Systems about_Systems = new About_Systems();
            about_Systems.MdiParent = this;
            about_Systems.Show();
            LayoutMdi(MdiLayout.Cascade);
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
