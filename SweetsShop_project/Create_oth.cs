using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;

namespace SweetsShop_project
{
    public partial class Create_oth : Form
    {
        public int count = 0;


        string connectionString = ConfigurationManager.ConnectionStrings["SweetsShop_project.Properties.Settings.SS_DB_Chart_connection"].ConnectionString;

        public Create_oth()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            chart1.Series[0].Points.Add(20);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Close();
            main.Show();
        }

        private void Create_oth_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sS_DBDataSet2.Сотрудники". При необходимости она может быть перемещена или удалена.
            this.сотрудникиTableAdapter.Fill(this.sS_DBDataSet2.Сотрудники);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sS_DBDataSet.prodaza". При необходимости она может быть перемещена или удалена.
            this.prodazaTableAdapter.Fill(this.sS_DBDataSet.prodaza);

        }

        private void ProdazaBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }



        private void Button1_Click(object sender, EventArgs e)
        {


            DateTime dt = DateTime.Now;
            string sqldt = dt.ToString("yyyy-MM-dd");
            SqlConnection sq = new SqlConnection(connectionString);
            string query = String.Format("Select count from priomka WHERE date between '2021-04-11' and @sqldt");
            string query1 = String.Format("Select count from prodaza WHERE date between '2021-04-11' and @sqldt");
            string query2 = String.Format("Select price from priomka WHERE date between '2021-04-11' and @sqldt");
            string query3 = String.Format("Select price from prodaza WHERE date between '2021-04-11' and @sqldt");
            //  string query4 = String.Format("Select count from priomka WHERE date between '2021-04-11' and @sqldt");
            sq.Open();


            using (SqlCommand sc = new SqlCommand(query, sq))
            {
                sc.Parameters.Add("@sqldt", SqlDbType.Char, 10).Value = sqldt;
                SqlDataReader reader = sc.ExecuteReader();
                object id = null;
                int sum = 0;
                while (reader.Read())
                {
                    id = reader.GetValue(0);
                    sum += (int)id;

                }
                chart1.Series[0].Points.Add(Convert.ToDouble(sum));
                reader.Close();
                sc.ExecuteNonQuery();
            }

            using (SqlCommand sc = new SqlCommand(query1, sq))
            {
                sc.Parameters.Add("@sqldt", SqlDbType.Char, 10).Value = sqldt;
                SqlDataReader reader = sc.ExecuteReader();
                object id = null;
                int sum = 0;
                while (reader.Read())
                {
                    id = reader.GetValue(0);
                    sum += (int)id;

                }
                chart1.Series[1].Points.Add(Convert.ToDouble(sum));
                reader.Close();
                sc.ExecuteNonQuery();
            }

            using (SqlCommand sc = new SqlCommand(query2, sq))
            {
                sc.Parameters.Add("@sqldt", SqlDbType.Char, 10).Value = sqldt;
                SqlDataReader reader = sc.ExecuteReader();
                object id = null;
                int sum = 0;
                while (reader.Read())
                {
                    id = reader.GetValue(0);
                    sum += (int)id;

                }
                chart1.Series[2].Points.Add(Convert.ToDouble(sum));
                reader.Close();
                sc.ExecuteNonQuery();
            }

            using (SqlCommand sc = new SqlCommand(query3, sq))
            {
                sc.Parameters.Add("@sqldt", SqlDbType.Char, 10).Value = sqldt;
                SqlDataReader reader = sc.ExecuteReader();
                object id = null;
                int sum = 0;
                while (reader.Read())
                {
                    id = reader.GetValue(0);
                    sum += (int)id;

                }
                chart1.Series[3].Points.Add(Convert.ToDouble(sum));
                reader.Close();
                sc.ExecuteNonQuery();
            }





        }

        private void Button2_Click(object sender, EventArgs e)
        {

            SqlConnection sq = new SqlConnection(connectionString);
            string query = String.Format("Select count from prodaza");


            sq.Open();

            SqlCommand sc = new SqlCommand(query, sq);
            SqlDataReader reader = sc.ExecuteReader();
            object id = null;
            while (reader.Read())
            {
                id = reader.GetValue(0);


            }

            chart1.Series[0].Points.Add(Convert.ToDouble(id));

            reader.Close();


            sc.ExecuteNonQuery();



        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
        }


    }
}
