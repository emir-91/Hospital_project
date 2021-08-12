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
    public partial class FrmPatientLogin : Form
    {
        public FrmPatientLogin()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        private void LnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatientRegister frRegister = new FrmPatientRegister();
            frRegister.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Patient Where PatientTC=@p1 and PatientPassword=@p2", connect.connecting());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmPatientDetail frmPatientDetail = new FrmPatientDetail();
                frmPatientDetail.tc = MskTC.Text;
                frmPatientDetail.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong TC or password");
            }
            connect.connecting().Close();
        }

        private void FrmPatientLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
