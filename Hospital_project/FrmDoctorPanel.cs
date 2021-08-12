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
    public partial class FrmDoctorPanel : Form
    {
        public FrmDoctorPanel()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        private void FrmDoctorPanel_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Doctors", connect.connecting());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Bring branches to comobox
            SqlCommand command2 = new SqlCommand("Select BranchName From Tbl_Branches", connect.connecting());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBranch.Items.Add(dr2[0]);
            }
            connect.connecting().Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Doctors (DoctorName,DoctorSurname,DoctorBranch,DoctorTC,DoctorPassword) values (@d1,@d2,@d3,@d4,@d5)", connect.connecting());
            command.Parameters.AddWithValue("@d1", TxtName.Text);
            command.Parameters.AddWithValue("@d2", TxtSurname.Text);
            command.Parameters.AddWithValue("@d3", CmbBranch.Text);
            command.Parameters.AddWithValue("@d4", MskTC.Text);
            command.Parameters.AddWithValue("@d5", TxtPassword.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Doctor added.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            TxtName.Text = dataGridView1.Rows[choose].Cells[1].Value.ToString();
            TxtSurname.Text = dataGridView1.Rows[choose].Cells[2].Value.ToString();
            CmbBranch.Text = dataGridView1.Rows[choose].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[choose].Cells[4].Value.ToString();
            TxtPassword.Text = dataGridView1.Rows[choose].Cells[5].Value.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete From Tbl_Doctors Where DoctorTC=@p1", connect.connecting());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Doctor deleted", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Tbl_Doctors set DoctorName=@d1,DoctorSurname=@d2,DoctorBranch=@d3,DoctorPassword=@d5 Where DoctorTC=@d4", connect.connecting());
            command.Parameters.AddWithValue("@d1", TxtName.Text);
            command.Parameters.AddWithValue("@d2", TxtSurname.Text);
            command.Parameters.AddWithValue("@d3", CmbBranch.Text);
            command.Parameters.AddWithValue("@d4", MskTC.Text);
            command.Parameters.AddWithValue("@d5", TxtPassword.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Doctor updated.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
