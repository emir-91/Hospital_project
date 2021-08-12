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
    public partial class FrmSecretaryDetail : Form
    {
        public FrmSecretaryDetail()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        public string TCnumber;

        private void FrmSecretaryDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumber;

            //Name and surname
            SqlCommand command1 = new SqlCommand("Select SecretaryNameSurname From Tbl_Secretary Where SecretaryTC=@p1", connect.connecting());
            command1.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                LblNameSurname.Text = dr1[0].ToString();
            }
            connect.connecting().Close();

            //Bring branches to data grid
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branches ", connect.connecting());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Bring doctors list
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoctorName+' '+DoctorSurname) as 'Doctors',DoctorBranch from Tbl_Doctors", connect.connecting());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Bring branches to comobox
            SqlCommand command2 = new SqlCommand("Select BranchName From Tbl_Branches", connect.connecting());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBranch.Items.Add(dr2[0]);
            }
            connect.connecting().Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SqlCommand commandSave = new SqlCommand("insert into Tbl_Appointments(AppointmentDate,AppointmentHour,AppointmentBranch,AppointmentDoctor) values (@r1,@r2,@r3,@r4)", connect.connecting());
            commandSave.Parameters.AddWithValue("@r1", MskDate.Text);
            commandSave.Parameters.AddWithValue("@r2", MskHour.Text);
            commandSave.Parameters.AddWithValue("@r3", CmbBranch.Text);
            commandSave.Parameters.AddWithValue("@r4", CmbDoctor.Text);
            commandSave.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Appointment created");
        }

        private void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoctor.Items.Clear();
            SqlCommand command = new SqlCommand("Select DoctorName,DoctorSurname From Tbl_Doctors Where DoctorBranch=@p1", connect.connecting());
            command.Parameters.AddWithValue("@p1", CmbBranch.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                CmbDoctor.Items.Add(dr[0] + " " + dr[1]);
            }
            connect.connecting().Close();

        }

        private void BtnAnnouncementCreate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Announcements (Announcement) values (@d1)", connect.connecting());
            command.Parameters.AddWithValue("@d1", RchAnnouncement.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Announcement created.");
        }

        private void BtnDoctorPanel_Click(object sender, EventArgs e)
        {
            FrmDoctorPanel frdp = new FrmDoctorPanel();
            frdp.Show();
        }

        private void BtnBranchPanel_Click(object sender, EventArgs e)
        {
            FrmBranch frb = new FrmBranch();
            frb.Show();
        }

        private void BtnAppointmentList_Click(object sender, EventArgs e)
        {
            FrmAppointmentList frmAppointment = new FrmAppointmentList();
            frmAppointment.Show();
        }

        private void BtnAnnouncements_Click(object sender, EventArgs e)
        {
            FrmAnnouncements frAnn = new FrmAnnouncements();
            frAnn.Show();
        }
    }
}
