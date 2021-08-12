﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hospital_project
{
    public partial class FrmAnnouncements : Form
    {
        public FrmAnnouncements()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        private void FrmAnnouncements_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Announcements", connect.connecting());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
