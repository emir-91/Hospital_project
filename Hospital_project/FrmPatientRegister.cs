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
    public partial class FrmPatientRegister : Form
    {
        public FrmPatientRegister()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Patient(PatientName,PatientSurname,PatientTC,PatientPhone,PatientPassword,PatientSex) values (@p1,@p2,@p3,@p4,@p5,@p6)", connect.connecting());
            command.Parameters.AddWithValue("@p1", TxtName.Text);
            command.Parameters.AddWithValue("@p2", TxtSurName.Text);
            command.Parameters.AddWithValue("@p3", MskTC.Text);
            command.Parameters.AddWithValue("@p4", MskPhone.Text);
            command.Parameters.AddWithValue("@p5", TxtPassword.Text);
            command.Parameters.AddWithValue("@p6", CmbGender.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Your registration has been completed, your password: " + TxtPassword.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmPatientRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
