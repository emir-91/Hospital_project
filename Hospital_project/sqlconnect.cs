using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hospital_project
{
    class sqlconnect
    {
        public SqlConnection connecting()
        {
            SqlConnection conect = new SqlConnection("Data Source=LAPTOP-EMIR\\SQLEXPRESS;Initial Catalog=Hospital project;Integrated Security=True");
            conect.Open();
            return conect;
        }
    }
}
