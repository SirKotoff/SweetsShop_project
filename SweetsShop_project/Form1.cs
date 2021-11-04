using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SweetsShop_project
{
    public partial class Form1 : Form
    {

        string connectionString = ConfigurationManager.ConnectionStrings["SweetsShop_project.Properties.Settings.SS_DB_Chart_connection"].ConnectionString;




        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {




            SqlConnection sq = new SqlConnection(connectionString);
            string query = "Select * from Auth Where login = '" + textBox1.Text.Trim() + "' and password ='" + textBox2.Text.Trim() + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sq);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Main m = new Main();
                this.Hide();
                m.Show();

            }
            else
            {
                MessageBox.Show("Ошибка авторизации");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
