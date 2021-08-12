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
    public partial class FrmPatientDetail : Form
    {
        public FrmPatientDetail()
        {
            InitializeComponent();
        }

        public string tc;

        sqlconnect connect = new sqlconnect();
        private void FrmPatientDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //Bring name and surname
            SqlCommand command = new SqlCommand("Select PatientName,PatientSurname From Tbl_Patient Where PatientTC=@p1", connect.connecting());
            command.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                LblNameSurname.Text = dr[0] + " " + dr[1];
            }
            connect.connecting().Close();

            //Appointment history
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Appointments Where PatientTC=" + tc, connect.connecting());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Bring branches
            SqlCommand command2 = new SqlCommand("Select BranchName From Tbl_Branches", connect.connecting());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBranch.Items.Add(dr2[0]);
            }
        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoctor.Items.Clear();
            SqlCommand command3 = new SqlCommand("Select DoctorName,DoctorSurname From Tbl_Doctors Where DoctorBranch=@p1", connect.connecting());
            command3.Parameters.AddWithValue("@p1", CmbBranch.Text);
            SqlDataReader dr3 = command3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoctor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            connect.connecting().Close();
        }

        private void CmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Appointments Where AppointmentBranch='" + CmbBranch.Text + "'" + "and AppointmentDoctor='" + CmbDoctor.Text + "'and AppointmentStatus=0", connect.connecting());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkEditInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEditInformation fr = new FrmEditInformation();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView2.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView2.Rows[choose].Cells[0].Value.ToString();
        }

        private void BtnMakeAppointment_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_Appointments set AppointmentStatus=1,PatientTC=@p1,PatientComplaint=@p2 Where AppointmentId=@p3", connect.connecting());
            command.Parameters.AddWithValue("@p1", LblTC.Text);
            command.Parameters.AddWithValue("@p2", RchComplaint.Text);
            command.Parameters.AddWithValue("@p3", TxtId.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Appointment made", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
