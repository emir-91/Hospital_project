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
    public partial class FrmSecretaryLogin : Form
    {
        public FrmSecretaryLogin()
        {
            InitializeComponent();
        }

        sqlconnect connect = new sqlconnect();

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From Tbl_Secretary Where SecretaryTC=@p1 and SecretaryPassword=@p2", connect.connecting());
            command.Parameters.AddWithValue("@p1", MskTC.Text);
            command.Parameters.AddWithValue("@p2", TxtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmSecretaryDetail fs = new FrmSecretaryDetail();
                fs.TCnumber = MskTC.Text;
                fs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong password or TC");
            }
            connect.connecting().Close();
        }
    }
}
