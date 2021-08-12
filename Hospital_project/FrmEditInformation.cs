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
    public partial class FrmEditInformation : Form
    {
        public FrmEditInformation()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        public string TCno;

        private void FrmEditInformation_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;
            SqlCommand command = new SqlCommand("Select * From Tbl_Patient Where PatientTC=@p1", connect.connecting());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                TxtName.Text = dr[1].ToString();
                TxtSurName.Text = dr[2].ToString();
                MskPhone.Text = dr[4].ToString();
                TxtPassword.Text = dr[5].ToString();
                CmbGender.Text = dr[6].ToString();
            }
            connect.connecting().Close();
        }

        private void BtnEditInfo_Click(object sender, EventArgs e)
        {
            SqlCommand command2 = new SqlCommand("update Tbl_Patient set PatientName=@p1,PatientSurname=@p2,PatientPhone=@p3,PatientPassword=@p4,PatientSex=@p5 Where PatientTC=@p6", connect.connecting());
            command2.Parameters.AddWithValue("@p1", TxtName.Text);
            command2.Parameters.AddWithValue("@p2", TxtSurName.Text);
            command2.Parameters.AddWithValue("@p3", MskPhone.Text);
            command2.Parameters.AddWithValue("@p4", TxtPassword.Text);
            command2.Parameters.AddWithValue("@p5", CmbGender.Text);
            command2.Parameters.AddWithValue("@p6", MskTC.Text);
            command2.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Your information has been updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
