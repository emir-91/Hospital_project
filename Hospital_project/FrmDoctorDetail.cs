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
    public partial class FrmDoctorDetail : Form
    {
        public FrmDoctorDetail()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        public string TC;
        private void FrmDoctorDetail_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            //Name and surname
            SqlCommand command = new SqlCommand("Select DoctorName,DoctorSurname From Tbl_Doctors Where DoctorTC=@p1", connect.connecting());
            command.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                LblNameSurname.Text = dr[0] + " " + dr[1];
            }
            connect.connecting().Close();

            //Appointments
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Appointments Where AppointmentDoctor='" + LblNameSurname.Text + "'", connect.connecting());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            FrmDoctorEditInfo frdei = new FrmDoctorEditInfo();
            frdei.TCNO = LblTC.Text;
            frdei.Show();
        }

        private void BtnAnnouncements_Click(object sender, EventArgs e)
        {
            FrmAnnouncements fran = new FrmAnnouncements();
            fran.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();     
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            RchComplaint.Text = dataGridView1.Rows[choose].Cells[7].Value.ToString();
        }
    }
}
