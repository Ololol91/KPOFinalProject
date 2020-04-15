using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using GemBox.Spreadsheet;

namespace ConstrPS
{
    public partial class Form1 : Form
    {
        Point last;

        static private int pageSize = 1;

        private int pageNumber = 0;

        ClassController controll;

        public bool checkUT = false;

        string table = "";

        DataSet ds;

        

        List<string> TreeView = new List<string>();

        public Form1()
        {
            InitializeComponent();

            controll = new ClassController(this);
            TreeView = controll.InitTreeView();
            InitTreeViewNodes();
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
            label3.Text = "";
            label5.Text = "";

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }


        public void GetSizeDB()
        {
            label5.Text = controll.GetSizeTable(table).ToString();
        }
        private void InitTreeViewNodes()
        {
            try
            {
                foreach (string s in TreeView)
                    treeView1.Nodes.Add(s);
            }
            catch{}
        }
        public void ShowData()
        {
            textBox1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label3.Text = "";

            table = treeView1.SelectedNode.ToString();
            string[] parts = table.Split(' ');
            table = parts[1];

           
            label4.Text = table;
            label5.Text = controll.GetSizeTable(table).ToString();

            string sql = "";
            if (table == "Test")
            {
                button2.Visible = true;
                button3.Visible = true;
                textBox1.Visible = true;
                label3.Text = ((pageNumber / pageSize) + 1).ToString();
                
                Test tst = new Test();
                sql = tst.SelectShow;
            }
            if (table == "тип")
            {
                Type tp = new Type();
                sql = tp.SelectShow;
            }
            if (table == "поставщик")
            {
                Provider pr = new Provider();
                sql = pr.SelectShow;
            }
            if (Equals(table, "страна"))
            {
                Country c = new Country();
                sql = c.SelectShow;
            }
            if (table == "клиент")
            {
                Client cl = new Client();
                sql = cl.SelectShow;
            }
            if (table == "должность")
            {
                Post ps = new Post();
                sql = ps.SelectShow;
            }
            if (table == "производитель")
            {
                Creator cr = new Creator();
                sql = cr.SelectShow;
            }
            if (table == "товар")
            {
                Product p = new Product();
                sql = p.SelectShow;
            }
            if (table == "заказ")
            {
                Order or= new Order();
                sql = or.SelectShow;
            }
            if (table == "сотрудник")
            {
                Employee em = new Employee();
                sql = em.SelectShow;
            }

            if (table == "отправка")
            {
                Sending send = new Sending();
                sql = send.SelectShow;
            }
            
            ds = controll.ShowData(sql);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                return;
            }

            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                last = MousePosition;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point cur = MousePosition;
                int dx = cur.X - last.X;
                int dy = cur.Y - last.Y;
                Point loc = new Point(Location.X + dx, Location.Y + dy);
                Location = loc;
                last = cur;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {     
            if (checkUT == false)
            {
                checkUT = true;
                UpdateTable nf = new UpdateTable(table, this);
                nf.Show();
            }
                
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            button1.Visible = true;
            label4.Visible = true;
            ShowData();
        }

        public string GetSql()
        {
            return "SELECT * FROM " + table + " LIMIT 3 OFFSET " + pageNumber;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ds.Tables[0].Rows.Count < pageSize) return;

            pageNumber += pageSize;
            string sql = "";
            if (table == "Test")
            {
                Test tst = new Test();
                sql = tst.GetSqlTest(pageNumber);
            }
            else
            {
                sql = GetSql();
            }

            ds = controll.NextAndBackButton(sql);
            // Отображаем данные
            dataGridView1.DataSource = ds.Tables[0];

            if (table == "Производитель" || table == "Клиент" || table == "Должность" || table == "Сотрудник"
                || table == "Страна" || table == "Тип" || table == "Поставщик")
                dataGridView1.Columns[0].Visible = false;
            else
                dataGridView1.Columns[0].Visible = true;

            label3.Text = ((pageNumber / pageSize) + 1).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pageNumber == 0) return;

            pageNumber -= pageSize;

            string sql = "";
            if (table == "Test")
            {
                Test tst = new Test();
                sql = tst.GetSqlTest(pageNumber);
            }
            else
            {
                sql = GetSql();
            }

            ds = controll.NextAndBackButton(sql);

            dataGridView1.DataSource = ds.Tables[0];

            if (table == "Производитель" || table == "Клиент" || table == "Должность" || table == "Сотрудник"
                || table == "Страна" || table == "Тип" || table == "Поставщик")
                dataGridView1.Columns[0].Visible = false;
            else
                dataGridView1.Columns[0].Visible = true;

            label3.Text = ((pageNumber / pageSize) + 1).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Test "
                + "WHERE Name LIKE \"" + textBox1.Text + "%\"";
            if(textBox1.Text.Length > 0)
            {
                ds = controll.ShowData(sql);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                ShowData();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Объект документа пдф
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream(@"D:\у4еба\РИС\3 курс\Конструирование ПО\ConstrPS\pdfTables.pdf", FileMode.Create));

            //Открываем документ
            doc.Open();

            DataSet MyDataSet = new DataSet();

            if (table == "Test")
            {
                Test tst = new Test();
                string sql = tst.SelectShow;
                MyDataSet = controll.ShowData(sql);
            }

            if (table == "Тип")
            {
                Type tp = new Type();
                string sql = tp.SelectShow;
                MyDataSet = controll.ShowData(sql);
            }

            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);

            //Создаем объект таблицы и передаем в нее число столбцов таблицы из датасета
            PdfPTable tble = new PdfPTable(MyDataSet.Tables[0].Columns.Count);

            //Добавим в таблицу общий заголовок
            PdfPCell cell = new PdfPCell(new Phrase("Таблица: " + table.ToString() + "\n\n", new iTextSharp.text.Font(baseFont, 16,
                iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));

            

            cell.Colspan = MyDataSet.Tables[0].Columns.Count;
            cell.HorizontalAlignment = 1;
            //Убираем границу первой ячейки, чтобы балы как заголовок
            cell.Border = 0;
            tble.AddCell(cell);

            //Сначала добавляем заголовки таблицы
            for (int j = 0; j < MyDataSet.Tables[0].Columns.Count; j++)
            {
                cell = new PdfPCell(new Phrase(new Phrase(MyDataSet.Tables[0].Columns[j].ColumnName, font)));

                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.Padding = 4;
                tble.AddCell(cell);
            }

            //Добавляем все остальные ячейки
            for (int j = 0; j < MyDataSet.Tables[0].Rows.Count; j++)
            {
                for (int k = 0; k < MyDataSet.Tables[0].Columns.Count; k++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(MyDataSet.Tables[0].Rows[j][k].ToString(), font)));
                    cell.Padding = 4;
                    tble.AddCell(cell);
                }
            }
            //Добавляем таблицу в документ
            doc.Add(tble);
            //Закрываем документ
            doc.Close();

            MessageBox.Show("Pdf-документ сохранен");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            if (table == "Test")
            {
                Test tst = new Test();
                string sql = tst.SelectShow;
                dataSet = controll.ShowData(sql);
            }

            if (table == "Тип")
            {
                Type tp = new Type();
                string sql = tp.SelectShow;
                dataSet = controll.ShowData(sql);
            }

            // Create new ExcelFile.
            ExcelFile workbook2 = new ExcelFile();

            // Imports all tables from DataSet to new file.
            foreach (DataTable dataTable in dataSet.Tables)
            {
                // Add new worksheet to the file.
                ExcelWorksheet worksheet = workbook2.Worksheets.Add(dataTable.TableName);
                worksheet.Cells[0, 0].Value = "Таблица: " + table.ToString();
                CellRange mrgdRange = worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, dataTable.Columns.Count-1);
                mrgdRange.Merged = true;
                CellStyle cs = new CellStyle();
                cs.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                cs.VerticalAlignment = VerticalAlignmentStyle.Center;
                cs.Font.Weight = ExcelFont.BoldWeight;
                cs.Font.Size = 16*18;
                mrgdRange.Style = cs;
                // Insert the data from DataTable to the worksheet starting at cell "A1".
               // worksheet.InsertDataTable(dataTable,
                 //   new InsertDataTableOptions("A2") { ColumnHeaders = true });
                //Добавляем все остальные ячейки

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i].Value = dataTable.Columns[i].ColumnName;
                    worksheet.Cells[1, i].Style.Font.Size = 16 * 16;
                    worksheet.Cells[1, i].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                    worksheet.Cells[1, i].Style.VerticalAlignment = VerticalAlignmentStyle.Center;                    
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    worksheet.Columns[i].Width = 30 * 200;
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j].Value = dataTable.Rows[i][j];
                        //_excelHeader++;
                    }

                }
            }

            // Save the file to XLS format.
            workbook2.Save(@"D:\у4еба\РИС\3 курс\Конструирование ПО\ConstrPS\DataSet.xls");

        }
    }
}
