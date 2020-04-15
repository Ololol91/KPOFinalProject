using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using GemBox.Spreadsheet;

namespace ConstrPS
{
    public partial class Dialog : Form
    {
        Form1 form1;
        ClassController controll;
        List<string> CmbBox = new List<string>();
        List<string> Tables = new List<string>();
        public Dialog(Form1 frm1)
        {
            InitializeComponent();
            form1 = frm1;
            controll = new ClassController(this);
            CmbBox = controll.InitTreeView();
            InitCheckBoxList();
           
        }

        public void InitTablesList()
        {

            if(checkedListBox1.CheckedItems.Count != 0)
            {
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    Tables.Add(checkedListBox1.CheckedItems[i].ToString());
                }
            }
            foreach(string s in Tables)
            {
                textBox1.Text += s + " ";
            }
        }
        public void InitCheckBoxList()
        {
            try
            {
                foreach (string s in CmbBox)
                    checkedListBox1.Items.Add(s);
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InitTablesList();
             DataSet dataSet = new DataSet();
             SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
             foreach(string table in Tables)
             {
                if (table == "Test")
                {
                    Test tst = new Test();
                    string sql = tst.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }

                if (table == "Тип")
                {
                    Type tp = new Type();
                    string sql = tp.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Поставщик")
                {
                    Provider pr = new Provider();
                    string sql = pr.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Страна")
                {
                    Country c = new Country();
                    string sql = c.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Клиент")
                {
                    Client cl = new Client();
                    string sql = cl.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Должность")
                {
                    Post ps = new Post();
                    string sql = ps.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Производитель")
                {
                    Creator cr = new Creator();
                    string sql = cr.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Товар")
                {
                    Product p = new Product();
                    string sql = p.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Заказ")
                {
                    Order or = new Order();
                    string sql = or.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
                if (table == "Сотрудник")
                {
                    Employee em = new Employee();
                    string sql = em.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }

                if (table == "Отправка")
                {
                    Sending send = new Sending();
                    string sql = send.SelectShow;
                    dataSet.Tables.Add(controll.InitDT(sql));
                }
            }
            
             // Create new ExcelFile.
             ExcelFile workbook2 = new ExcelFile();

             // Imports all tables from DataSet to new file.
             foreach (DataTable dataTable in dataSet.Tables)
             {
                 // Add new worksheet to the file.
                 ExcelWorksheet worksheet = workbook2.Worksheets.Add(dataTable.TableName);
                 worksheet.Cells[0, 0].Value = "Таблица: " + dataTable.TableName;
                 CellRange mrgdRange = worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, dataTable.Columns.Count - 1);
                 mrgdRange.Merged = true;
                 CellStyle cs = new CellStyle();
                 cs.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                 cs.VerticalAlignment = VerticalAlignmentStyle.Center;
                 cs.Font.Weight = ExcelFont.BoldWeight;
                 cs.Font.Size = 16 * 18;
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
             workbook2.Save(@"C:\Users\WorkUser\Desktop\DataSet.xls");
        }
    }
}
