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
    public partial class FrmBranch : Form
    {
        public FrmBranch()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        private void FrmBranch_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branches ", connect.connecting());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Branches (BranchName) values (@b1)", connect.connecting());
            command.Parameters.AddWithValue("@b1", TxtBranch.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Branch added.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView1.Rows[choose].Cells[0].Value.ToString();
            TxtBranch.Text = dataGridView1.Rows[choose].Cells[1].Value.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("delete From Tbl_Branches Where BranchId=@b1", connect.connecting());
            command.Parameters.AddWithValue("@b1", TxtId.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Barnch deleted.");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Tbl_Branches set BranchName=@p1 Where BranchId=@p2", connect.connecting());
            command.Parameters.AddWithValue("@p1", TxtBranch.Text);
            command.Parameters.AddWithValue("@p2", TxtId.Text);
            command.ExecuteNonQuery();
            connect.connecting().Close();
            MessageBox.Show("Branch updated");
        }
    }
}
