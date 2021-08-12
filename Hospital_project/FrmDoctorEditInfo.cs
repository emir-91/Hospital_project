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

namespace Hospital_project
{
    public partial class FrmDoctorEditInfo : Form
    {
        public FrmDoctorEditInfo()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();
        public string TCNO;

        private void FrmDoctorEditInfo_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCNO;
            SqlCommand command = new SqlCommand("Select * From Tbl_Doctors Where DoctorTC=@p1", connect.connecting());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                TxtName.Text = dr[1].ToString();
                TxtSurName.Text = dr[2].ToString();
                CmbBranch.Text = dr[3].ToString();
                TxtPassword.Text = dr[5].ToString();
            }
            connect.connecting().Close();
        }

        private void BtnEditInfo_Click(object sender, EventArgs e)
        {
            SqlCommand command2 = new SqlCommand("update Tbl_Doctors set DoctorName=@p1,DoctorSurname=@p2,DoctorBranch=@p3,DoctorPassword=@p4 Where DoctorTC=@p5", connect.connecting());
            command2.Parameters.AddWithValue("@p1", TxtName.Text);
            command2.Parameters.AddWithValue("@p2", TxtSurName.Text);
            command2.Parameters.AddWithValue("@p3", CmbBranch.Text);
            command2.Parameters.AddWithValue("@p4", TxtPassword.Text);
            command2.Parameters.AddWithValue("@p5", MskTC.Text);
            command2.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Your information has been updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
