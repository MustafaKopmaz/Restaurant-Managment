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

namespace Restaurant_Managment.Model
{
    public partial class frmTableSelect : Form
    {
        public frmTableSelect()
        {
            InitializeComponent();
        }


        private void frmTableSelect_Load(object sender, EventArgs e)
        {
            string qry = "Select*From tables";
            SqlCommand cmd = new SqlCommand(qry,MainClass.con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.Text = row["tablename"].ToString();
                b.Width = 150;
                b.Height = 50;
                b.FillColor = Color.FromArgb(227, 227, 227);
                b.HoverState.FillColor = Color.FromArgb(185, 209, 234);

                //Olay için Tıklama 
                b.Click += new EventHandler(b_Click);
                flowLayoutPanel1.Controls.Add(b);


            }
        }

        public string TableName;
        

        private void b_Click(object sender, EventArgs e)
        {
            TableName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }

    }
}
