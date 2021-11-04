using System;
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
using System.IO;
using Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Configuration;

namespace SweetsShop_project
{
    public partial class Prodaza : Form
    {
        private object table1BindingSource;
        string connectionString = ConfigurationManager.ConnectionStrings["SweetsShop_project.Properties.Settings.SS_DB_Chart_connection"].ConnectionString;


        public Prodaza()
        {
            InitializeComponent();

        }


        public void Update()
        {
            SqlConnection sq = new SqlConnection();
            SqlCommand sc = new SqlCommand();
            // string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\SweetsShop_project\SweetsShop_project\SS_DB.mdf;Integrated Security=True";
            SqlDataAdapter adapter = new SqlDataAdapter();
            System.Data.DataTable dt = new System.Data.DataTable();
            sq.ConnectionString = connectionString;
            sc.Connection = sq;
            sc.CommandText = "SELECT* FROM prodaza";
            adapter.SelectCommand = sc;
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }


        public void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        public void Delete()
        {
            DataRow row = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView).Row;

            using (SqlConnection sq = new SqlConnection(connectionString))
            {
                sq.Open();

                using (SqlCommand sc = new SqlCommand("DELETE FROM prodaza WHERE id=" + row["id"], sq))
                {
                    sc.ExecuteNonQuery();
                }



                Update();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {



            string name = textBox1.Text.Trim().ToString();
            string count = textBox3.Text.Trim().ToString();
            string price = textBox2.Text.Trim().ToString();



            SqlConnection sq = new SqlConnection(connectionString);
            string query = String.Format("INSERT INTO prodaza" + "(name, count, price,date) VALUES (@name, @count, @price,@date)");
            sq.Open();




            //////


            SqlDataAdapter adapter = new SqlDataAdapter(query, sq);
            SqlCommandBuilder sqlcb = new SqlCommandBuilder(adapter);


            //////
            ///



            DateTime dt = DateTime.Now;
            string sqldt = dt.ToString("yyyy-MM-dd");
            using (SqlCommand sc = new SqlCommand(query, sq))
            {

                sc.Parameters.Add("@name", SqlDbType.Char, 30).Value = name;
                sc.Parameters.AddWithValue("@count", count);
                sc.Parameters.AddWithValue("@price", price);
                sc.Parameters.Add("@date", SqlDbType.Char, 30).Value = sqldt;

                sc.ExecuteNonQuery();
            }

            Update();
            Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Close();
            main.Show();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Prodaza_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sS_DBDataSet1.prodaza". При необходимости она может быть перемещена или удалена.
            this.prodazaTableAdapter.Fill(this.sS_DBDataSet1.prodaza);

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void СохранениеВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter myWritet = new StreamWriter(myStream);
                    try
                    {
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                myWritet.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "  ");
                            }
                            myWritet.WriteLine();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myWritet.Close();
                    }

                    myStream.Close();
                }
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {



            string c = comboBox1.Text;
            switch (c)
            {
                case "name": table2BindingSource.Filter = String.Format("name LIKE '%{0}%'", textBox4.Text); break;
                case "price": table2BindingSource.Filter = String.Format("price LIKE '%{0}%'", textBox4.Text); break;

                case "": table2BindingSource.Filter = ""; break;
            }

        }

        private void СохранениеВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            Microsoft.Office.Interop.Excel.Range rangeWidth1 = ExcelWorkSheet.Range["A1", System.Type.Missing];
            rangeWidth1.EntireColumn.ColumnWidth = 10;
            Microsoft.Office.Interop.Excel.Range rangeWidth2 = ExcelWorkSheet.Range["B1", System.Type.Missing];
            rangeWidth2.EntireColumn.ColumnWidth = 16;
            Microsoft.Office.Interop.Excel.Range rangeWidth3 = ExcelWorkSheet.Range["C1", System.Type.Missing];
            rangeWidth3.EntireColumn.ColumnWidth = 16;

            //Вызываем созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;

        }

        private void СохранениеВPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {


            string fileName = "";
            //Объект документа пдф
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream("pdfTables.pdf", FileMode.Create));

            //Открываем документ
            doc.Open();

            //Определение шрифта необходимо для сохранения кириллического текста
            //Иначе мы не увидим кириллический текст
            //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            //Обход по всем таблицам датасета (хотя в данном случае мы можем опустить
            //Так как в нашей бд только одна таблица)
            for (int i = 0; i < sS_DBDataSet1.Tables.Count; i++)
            {
                //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
                PdfPTable table = new PdfPTable(sS_DBDataSet1.Tables[i].Columns.Count);

                //Добавим в таблицу общий заголовок
                PdfPCell cell = new PdfPCell(new Phrase("БД " + fileName + ", таблица №" + (i + 1), font));

                cell.Colspan = sS_DBDataSet1.Tables[i].Columns.Count;
                cell.HorizontalAlignment = 1;
                //Убираем границу первой ячейки, чтобы балы как заголовок
                cell.Border = 0;
                table.AddCell(cell);

                //Сначала добавляем заголовки таблицы
                for (int j = 0; j < sS_DBDataSet1.Tables[i].Columns.Count; j++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(sS_DBDataSet1.Tables[i].Columns[j].ColumnName, font)));
                    //Фоновый цвет (необязательно, просто сделаем по красивее)
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                //Добавляем все остальные ячейки
                for (int j = 0; j < sS_DBDataSet1.Tables[i].Rows.Count; j++)
                {
                    for (int k = 0; k < sS_DBDataSet1.Tables[i].Columns.Count; k++)
                    {
                        table.AddCell(new Phrase(sS_DBDataSet1.Tables[i].Rows[j][k].ToString(), font));
                    }
                }
                //Добавляем таблицу в документ
                doc.Add(table);
            }
            //Закрываем документ
            doc.Close();

            MessageBox.Show("Pdf-документ сохранен");
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.prodazaTableAdapter.FillBy(this.sS_DBDataSet1.prodaza);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
