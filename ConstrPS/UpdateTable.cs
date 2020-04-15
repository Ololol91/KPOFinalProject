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
using System.Data.SqlClient;

namespace ConstrPS
{
    public partial class UpdateTable : Form
    {
        Point last;
        bool checkText = false;

        int pageNumber = 0;
        int but = 0;
        string table = "";
        Form1 form1;

        DataSet ds;

        ClassController controll;

        List<string> BaseNumbers = new List<string>();

        Dictionary<int, string> NameStran = new Dictionary<int, string>();
        Dictionary<int, string> NameType = new Dictionary<int, string>();
        Dictionary<int, string> NameProizv = new Dictionary<int, string>();
        Dictionary<int, string> NameDolzh = new Dictionary<int, string>();
        Dictionary<int, string> NumberClient = new Dictionary<int, string>();

        List<int> Articul = new List<int>();
        List<int> KodZakaz = new List<int>();
        Dictionary<int, string> NamePostav = new Dictionary<int, string>();
        Dictionary<int, string> NameSotrud = new Dictionary<int, string>();

        // Создаем объект DataAdapter
        MySqlDataAdapter adapter;

        public UpdateTable(string tbl, Form1 frm)
        {
            InitializeComponent();
            controll = new ClassController(this);
            BaseNumbers = controll.InitBaseNumbers();
            NameStran = controll.InitNameStran();
            NameType = controll.InitNameType();
            NameProizv = controll.InitNameProizv();
            NameDolzh = controll.InitNameDolzh();
            NumberClient = controll.InitNumberClient();
            Articul = controll.InitArticul();
            KodZakaz = controll.InitKodZakaz();
            NamePostav = controll.InitNamePostav();
            NameSotrud = controll.InitNameSotrud();
                        
            table = tbl;
            label14.Text = tbl;
            form1 = frm;
            StartInitForm();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void InitCmbNameStran()
        {           
            comboBox2.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameStran)
                comboBox2.Items.Add(keyValue.Value.ToString());
            comboBox8.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameStran)
                comboBox8.Items.Add(keyValue.Value.ToString());
        }
        public void InitCmbNameType()
        {
            comboBox2.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameType)
                comboBox2.Items.Add(keyValue.Value.ToString());
            comboBox8.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameType)
                comboBox8.Items.Add(keyValue.Value.ToString());
        }
        public void InitCmbNameProizv()
        {
            comboBox3.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameProizv)
                comboBox3.Items.Add(keyValue.Value.ToString());
            comboBox9.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameProizv)
                comboBox9.Items.Add(keyValue.Value.ToString());
        }

        public void InitCmbNumberClient()
        {
            comboBox1.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NumberClient)
                comboBox1.Items.Add(keyValue.Value.ToString());
            comboBox7.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NumberClient)
                comboBox7.Items.Add(keyValue.Value.ToString());
        }
        public void InitCmbNameDolzh()
        {
            comboBox4.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameDolzh)
                comboBox4.Items.Add(keyValue.Value.ToString());
            comboBox10.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameDolzh)
                comboBox10.Items.Add(keyValue.Value.ToString());
        }

        public void InitCmbArticul()
        {
            comboBox1.Items.Clear();
            foreach (int t in Articul)
                comboBox1.Items.Add(t.ToString());
            comboBox7.Items.Clear();
            foreach (int t in Articul)
                comboBox7.Items.Add(t.ToString());
        }
        public void InitCmbKodZakaz()
        {
            comboBox2.Items.Clear();
            foreach (int t in KodZakaz)
                comboBox2.Items.Add(t.ToString());
            comboBox8.Items.Clear();
            foreach (int t in KodZakaz)
                comboBox8.Items.Add(t.ToString());
        }
        public void InitCmbNamePostav()
        {
            comboBox3.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NamePostav)
                comboBox3.Items.Add(keyValue.Value.ToString());
            comboBox9.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NamePostav)
                comboBox9.Items.Add(keyValue.Value.ToString());
        }
        public void InitCmbNameSotrud()
        {
            comboBox4.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameSotrud)
                comboBox4.Items.Add(keyValue.Value.ToString());
            comboBox10.Items.Clear();
            foreach (KeyValuePair<int, string> keyValue in NameSotrud)
                comboBox10.Items.Add(keyValue.Value.ToString());
        }
        private void button4_Click(object sender, EventArgs e)
        {
            but = 4;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            if(table == "страна")
            {
                textBox1.Visible = true;

                label1.Visible = true;

                label1.Text = "Название страны:";
            }
            if (table == "производитель")
            {
                InitCmbNameStran();

                textBox1.Visible = true;

                comboBox2.Visible = true;

                label1.Visible = true;
                label2.Visible = true;

                label1.Text = "Название производителя:";
                label2.Text = "Выберите страну:";
            }

            if(table == "тип")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;

                label1.Visible = true;
                label2.Visible = true;

                label1.Text = "Наименование типа:";
                label2.Text = "Описание типа товара:";
            }
            if (table == "товар")
            {
                InitCmbNameType();
                InitCmbNameProizv();

                textBox1.Visible = true;

                comboBox2.Visible = true;
                comboBox3.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;

                label1.Text = "Наименование товара:";
                label2.Text = "Тип товара:";
                label3.Text = "Производитель товара:";
            }
            if (table == "клиент")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox5.Visible = true;

                maskedTextBox1.Visible = true;              

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;

                label1.Text = "Фамилия:";
                label2.Text = "Имя:";
                label3.Text = "Отчество:";
                label4.Text = "Телефон:";
                label5.Text = "Адрес:";
            }
            if (table == "заказ")
            {
                InitCmbNumberClient();

                dateTimePicker1.Visible = true;

                comboBox1.Visible = true;

                label1.Visible = true;
                label2.Visible = true;

                label1.Text = "Номер клиента:";
                label2.Text = "Дата заказа(ГГГГ-ММ-ДД):";
            }
            if (table == "поставщик")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;

                maskedTextBox2.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;


                label1.Text = "Наименование:";
                label2.Text = "Адрес:";
                label3.Text = "Телефон:";
            }

            if (table == "должность")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;


                label1.Text = "Наименование:";
                label2.Text = "Зарплата:";
                label3.Text = "Обязанности:";
            }
            if (table == "сотрудник")
            {
                InitCmbNameDolzh();

                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;

                maskedTextBox3.Visible = true;

                comboBox4.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;

                label1.Text = "Фамилия:";
                label2.Text = "Имя:";
                label3.Text = "Отчество:";
                label4.Text = "Должность:";
                label5.Text = "Телефон:";
                
            }

            if (table == "отправка")
            {
                InitCmbArticul();
                InitCmbKodZakaz();
                InitCmbNamePostav();
                InitCmbNameSotrud();

                dateTimePicker2.Visible = true;
                dateTimePicker3.Visible = true;

                comboBox1.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
               
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

                label1.Text = "Артикул товара:";
                label2.Text = "Номер заказа:";
                label3.Text = "Поставщик:";
                label4.Text = "Сотрудник:";
                label5.Text = "Дата поставки(ГГГГ-ММ-ДД):";
                label6.Text = "Дата отправки(ГГГГ-ММ-ДД):";

            }
            if (table == "Test")
            {
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox5.Visible = true;

                maskedTextBox1.Visible = true;

                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;

                label2.Text = "Имя:";
                label3.Text = "Адрес:";
                label4.Text = "Телефон:";
                label5.Text = "Паспорт:";
            }
        }

       

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                checkText = false;

                DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить данную запись в таблицу " + table + "?", "Внимание", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (table == "Test" && textBox2.Text.Length > 0 && textBox3.Text.Length > 0
                        && maskedTextBox1.MaskFull && textBox5.Text.Length > 0)
                    {
                        checkText = true;

                        string name = textBox2.Text.ToString();
                        string adress = textBox3.Text.ToString();

                        string text = maskedTextBox1.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }
                        string pasport = textBox5.Text.ToString();

                        Test t = new Test();
                        t.InsertTest(name, adress, pasport, tel);
                    }

                    if (table == "Страна" && textBox1.Text.Length > 0)
                    {
                        checkText = true;

                        string name = textBox1.Text.ToString();
                        Country c = new Country();
                        c.InsertCountry(name);
                    }
                    if (table == "Производитель" && textBox1.Text.Length > 0 && comboBox2.Text.Length > 0)
                    {
                        checkText = true;

                        string name = textBox1.Text.ToString();
                        int stran = 0;           
                     
                        foreach (KeyValuePair<int, string> kv in NameStran)
                        {
                            if (kv.Value == comboBox2.Text.ToString())
                            {
                                stran = kv.Key;
                            }
                        }
                        Creator c = new Creator();
                        c.InsertCreator(name, stran);
                    }

                    if (table == "Тип" && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                    {
                        checkText = true;

                        string name = textBox1.Text.ToString();
                        string opis = textBox2.Text.ToString();

                        Type t = new Type();
                        t.InsertType(name, opis);
                    }

                    if (table == "Товар" && textBox1.Text.Length > 0 && comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0)
                    {
                        checkText = true;

                        string name = textBox1.Text.ToString();
                        int type = 0;
                        int proizv = 0;

                        foreach (KeyValuePair<int, string> kv in NameType)
                        {
                            if (kv.Value == comboBox2.Text.ToString())
                            {
                                type = kv.Key;
                            }
                        }

                        foreach (KeyValuePair<int, string> kv in NameProizv)
                        {
                            if (kv.Value == comboBox3.Text.ToString())
                            {
                                proizv = kv.Key;
                            }
                        }

                        Product p = new Product();
                        p.InsertProduct(name, type, proizv);
                    }

                    if (table == "Клиент" && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0
                        && maskedTextBox1.MaskFull && textBox5.Text.Length > 0)
                    {
                        checkText = true;

                        string fam = textBox1.Text.ToString();
                        string name = textBox2.Text.ToString();
                        string otch = textBox3.Text.ToString();

                        string text = maskedTextBox1.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }

                        foreach(string t in BaseNumbers)
                        {
                            if(tel == t)
                            {
                                MessageBox.Show("Клиент с таким номером телефона уже существует!");
                                return;
                            }
                        }
                        string adres = textBox5.Text.ToString();

                        Client c = new Client();
                        c.InsertClient(fam, name, otch, tel, adres);
                    }

                    if (table == "Заказ" && comboBox1.Text.Length > 0)
                    {
                        checkText = true;
                       
                        int numb = 0;
                        string date = Parser(dateTimePicker1.Text.ToString());
                      
                        foreach (KeyValuePair<int, string> kv in NumberClient)
                        {
                            if (kv.Value == comboBox1.Text.ToString())
                            {
                                numb = kv.Key;
                            }
                        }
                        Order o = new Order();
                        o.InsertOrder(numb, date);
                    }
                    if (table == "Поставщик" && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && maskedTextBox2.MaskFull)
                    {
                        checkText = true;

                        string name = textBox1.Text.ToString();
                        string adres = textBox2.Text.ToString();

                        string text = maskedTextBox2.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }

                        Provider p = new Provider();
                        p.InsertProvider(name, tel, adres);
                    }
                    if (table == "Должность" && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
                    {
                        checkText = true;
                                       
                        string name = textBox1.Text.ToString();
                        string zarp = textBox2.Text.ToString();
                        string obyaz = textBox3.Text.ToString();

                        Post p = new Post();
                        p.InsertPost(name, zarp, obyaz);
                    }
                    if (table == "Сотрудник" && textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0
                        && comboBox4.Text.Length > 0 && maskedTextBox3.MaskFull)
                    {
                        checkText = true;
                       
                        string fam = textBox1.Text.ToString();
                        string name = textBox2.Text.ToString();
                        string otch = textBox3.Text.ToString();

                        string text = maskedTextBox3.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }

                        int dolzh = 0;
                        foreach (KeyValuePair<int, string> kv in NameDolzh)
                        {
                            if (kv.Value == comboBox4.Text.ToString())
                            {
                                dolzh = kv.Key;
                            }
                        }
                        Employee em = new Employee();
                        em.InsertEmployee(fam, name, otch, tel, dolzh);
                    }

                    if (table == "Отправка" && comboBox1.Text.Length > 0 && comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0
                        && comboBox4.Text.Length > 0)
                    {
                        checkText = true;

                        int tov = 0;
                        int zak = 0;
                        int pos = 0;
                        int sotr = 0;
                        string dp = Parser(dateTimePicker2.Text.ToString());
                        string dot = Parser(dateTimePicker3.Text.ToString());

                        foreach (int key in Articul)
                        {
                            if(key == Convert.ToInt32(comboBox1.Text))
                            {
                                tov = key;
                            }
                        }

                        foreach (int key in KodZakaz)
                        {
                            if (key == Convert.ToInt32(comboBox2.Text))
                            {
                                zak = key;
                            }
                        }

                        foreach (KeyValuePair<int, string> kv in NamePostav)
                        {
                            if (kv.Value == comboBox3.Text.ToString())
                            {
                                pos = kv.Key;
                            }
                        }

                        foreach (KeyValuePair<int, string> kv in NameSotrud)
                        {
                            if (kv.Value == comboBox4.Text.ToString())
                            {
                                sotr = kv.Key;
                            }
                        }

                        Sending s = new Sending();
                        s.InsertSending(tov, zak, pos, sotr, dp, dot);
                    }
                    if(checkText)
                        MessageBox.Show("Запись успешно добавлена!");
                    else
                        MessageBox.Show("Заполните все поля!");

                    form1.ShowData();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка подключения к БД!");
                MessageBox.Show(ex.Message, "Возможно, вы не выбрали запись или некорректно заполнили поля.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить данную запись из таблицы " + table + "?", "Внимание", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    string id = dataGridView1.SelectedCells[0].Value.ToString();

                    if (controll.DeleteFromTable(table, id) == true)
                    {
                        MessageBox.Show("Запись успешно удалена!");

                        form1.ShowData();

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }
                        
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка подключения к БД!");
                MessageBox.Show(ex.Message, "Возможно, вы не выбрали запись или некорректно заполнили поля.");
            }
        }

      
        private void button5_Click(object sender, EventArgs e)
        {
            but = 5;
            ShowTable(but);
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            but = 6;
            ShowTable(but);
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;

            if (table == "Страна")
            {
                textBox7.Visible = true;

                label7.Visible = true;

                label7.Text = "Название страны:";
            }
            if (table == "Производитель")
            {
                InitCmbNameStran();

                textBox7.Visible = true;

                comboBox8.Visible = true;

                label7.Visible = true;
                label8.Visible = true;

                label7.Text = "Название производителя:";
                label8.Text = "Выберите страну:";
            }

            if (table == "Тип")
            {
                textBox7.Visible = true;
                textBox8.Visible = true;

                label7.Visible = true;
                label8.Visible = true;

                label7.Text = "Наименование типа:";
                label8.Text = "Описание типа товара:";
            }
            if (table == "Товар")
            {
                InitCmbNameType();
                InitCmbNameProizv();

                textBox7.Visible = true;

                comboBox8.Visible = true;
                comboBox9.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;

                label7.Text = "Наименование товара:";
                label8.Text = "Тип товара:";
                label9.Text = "Производитель товара:";
            }
            if (table == "Клиент")
            {
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;

                maskedTextBox5.Visible = true;

                textBox11.Visible = true;

                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                label7.Text = "Фамилия:";
                label8.Text = "Имя:";
                label9.Text = "Отчество:";
                label10.Text = "Телефон:";
                label11.Text = "Адрес:";
            }
            if (table == "Заказ")
            {
                InitCmbNumberClient();

                dateTimePicker4.Visible = true;

                comboBox7.Visible = true;

                label7.Visible = true;
                label8.Visible = true;

                label7.Text = "Номер клиента:";
                label8.Text = "Дата заказа(ГГГГ-ММ-ДД):";
            }
            if (table == "Поставщик")
            {
                textBox7.Visible = true;
                textBox8.Visible = true;

                maskedTextBox4.Visible = true;

                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;


                label7.Text = "Наименование:";
                label8.Text = "Адрес:";
                label9.Text = "Телефон:";
            }

            if (table == "Должность")
            {
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;

                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;


                label7.Text = "Наименование:";
                label8.Text = "Зарплата:";
                label9.Text = "Обязанности:";
            }
            if (table == "Сотрудник")
            {
                InitCmbNameDolzh();

                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;

                maskedTextBox6.Visible = true;

                comboBox10.Visible = true;

                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                label7.Text = "Фамилия:";
                label8.Text = "Имя:";
                label9.Text = "Отчество:";
                label10.Text = "Должность:";
                label11.Text = "Телефон:";

            }

            if (table == "Отправка")
            {
                InitCmbArticul();
                InitCmbKodZakaz();
                InitCmbNamePostav();
                InitCmbNameSotrud();

                dateTimePicker5.Visible = true;
                dateTimePicker6.Visible = true;

                comboBox7.Visible = true;
                comboBox8.Visible = true;
                comboBox9.Visible = true;
                comboBox10.Visible = true;

                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;

                label7.Text = "Артикул товара:";
                label8.Text = "Номер заказа:";
                label9.Text = "Поставщик:";
                label10.Text = "Сотрудник:";
                label11.Text = "Дата поставки(ГГГГ-ММ-ДД):";
                label12.Text = "Дата отправки(ГГГГ-ММ-ДД):";

            }

            if (table == "Test")
            {
                textBox8.Visible = true;
                textBox9.Visible = true;
                textBox11.Visible = true;

                maskedTextBox5.Visible = true;

                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                label8.Text = "Имя:";
                label9.Text = "Адрес:";
                label10.Text = "Телефон:";
                label11.Text = "Паспорт:";
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                checkText = false;
                DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить данную запись в таблице " + table + "?", "Внимание", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (table == "Test" && textBox8.Text.Length > 0 && textBox9.Text.Length > 0
                        && maskedTextBox5.MaskFull && textBox11.Text.Length > 0)
                    {
                        checkText = true;

                        string fam = textBox8.Text.ToString();
                        string name = textBox9.Text.ToString();
                        string otch = textBox11.Text.ToString();
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        string text = maskedTextBox5.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }
                        Test c = new Test();
                        c.UpdateTest(id, fam, name, otch, tel);
                    }
                    if (table == "Страна" && textBox7.Text.Length > 0)
                    {
                        checkText = true;
                       
                        string name = textBox7.Text.ToString();
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        Country c = new Country();
                        c.UpdateCountry(name, id);
                    }
                    if (table == "Производитель" && textBox7.Text.Length > 0 && comboBox8.Text.Length > 0)
                    {
                        checkText = true;
                       
                        string name = textBox7.Text.ToString();
                        int stran = 0;
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        foreach (KeyValuePair<int, string> kv in NameStran)
                        {
                            if (kv.Value == comboBox8.Text.ToString())
                            {
                                stran = kv.Key;
                            }
                        }
                        Creator c = new Creator();
                        c.UpdateCreator(id, name, stran);
                    }

                    if (table == "Тип" && textBox7.Text.Length > 0 && textBox8.Text.Length > 0)
                    {
                        checkText = true;
                       
                        string name = textBox7.Text.ToString();
                        string opis = textBox8.Text.ToString();
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        Type t = new Type();
                        t.UpdateType(id, name, opis);
                    }

                    if (table == "Товар" && textBox7.Text.Length > 0 && comboBox8.Text.Length > 0 && comboBox9.Text.Length > 0)
                    {
                       
                        string name = textBox7.Text.ToString();
                        int type = 0;
                        int proizv = 0;
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);     

                        foreach (KeyValuePair<int, string> kv in NameType)
                        {
                            if (kv.Value == comboBox8.Text.ToString())
                            {
                                type = kv.Key;
                            }
                        }
                        foreach (KeyValuePair<int, string> kv in NameProizv)
                        {
                            if (kv.Value == comboBox9.Text.ToString())
                            {
                                proizv = kv.Key;
                            }
                        }

                        Product p = new Product();
                        p.UpdateProduct(id, name, type, proizv);

                    }

                    if (table == "Клиент" && textBox7.Text.Length > 0 && textBox8.Text.Length > 0 && textBox9.Text.Length > 0
                        && maskedTextBox5.MaskFull && textBox11.Text.Length > 0)
                    {
                        checkText = true;

                        string fam = textBox7.Text.ToString();
                        string name = textBox8.Text.ToString();
                        string otch = textBox9.Text.ToString();
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        string text = maskedTextBox5.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }
                        BaseNumbers.Remove(tel);
                        foreach (string t in BaseNumbers)
                        {
                            if (tel == t)
                            {
                                MessageBox.Show("Клиент с таким номером телефона уже существует!");
                                return;
                            }
                        }
                        string adres = textBox11.Text.ToString();

                        Client c = new Client();
                        c.UpdateClient(id, fam, name, otch, tel, adres);
                    }

                    if (table == "Заказ" && comboBox7.Text.Length > 0)
                    {
                        checkText = true;
                        int numb = 0;
                        string date = Parser(dateTimePicker4.Text.ToString());
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        foreach (KeyValuePair<int, string> kv in NumberClient)
                        {
                            if (kv.Value == comboBox7.Text.ToString())
                            {
                                numb = kv.Key;
                            }
                        }
                        Order o = new Order();
                        o.UpdateOrder(id, numb, date);
                    }
                    if (table == "Поставщик" && textBox7.Text.Length > 0 && textBox8.Text.Length > 0 && maskedTextBox5.MaskFull)
                    {
                        checkText = true;

                        string name = textBox7.Text.ToString();
                        string adres = textBox8.Text.ToString();

                        string text = maskedTextBox4.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });
                        for (int i = 0; i < parts.Count(); i++)
                        {
                            tel += parts[i];
                        }
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        Provider p = new Provider();
                        p.UpdateProvider(id, name, tel, adres);
                    }
                    if (table == "Должность" && textBox7.Text.Length > 0 && textBox8.Text.Length > 0 && textBox9.Text.Length > 0)
                    {
                        checkText = true;

                        string name = textBox7.Text.ToString();
                        string zarp = textBox8.Text.ToString();
                        string obyaz = textBox9.Text.ToString();
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        Post p = new Post();
                        p.UpdatePost(id, name, zarp, obyaz);
                    }
                    if (table == "Сотрудник" && textBox7.Text.Length > 0 && textBox8.Text.Length > 0 && textBox9.Text.Length > 0
                        && comboBox10.Text.Length > 0 && maskedTextBox6.MaskFull)
                    {
                        checkText = true;

                        string fam = textBox7.Text.ToString();
                        string name = textBox8.Text.ToString();
                        string otch = textBox9.Text.ToString();

                        string text = maskedTextBox6.Text.ToString();
                        string tel = "";
                        string[] parts = text.Split(new char[] { ' ', '+', '(', ')', '-' });

                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        int dolzh = 0;
                        foreach (KeyValuePair<int, string> kv in NameDolzh)
                        {
                            if (kv.Value == comboBox10.Text.ToString())
                            {
                                dolzh = kv.Key;
                            }
                        }

                        Employee em = new Employee();
                        em.UpdateEmployee(id, fam, name, otch, tel, dolzh);
                    }

                    if (table == "Отправка" && comboBox7.Text.Length > 0 && comboBox8.Text.Length > 0 && comboBox9.Text.Length > 0
                        && comboBox10.Text.Length > 0)
                    {
                        checkText = true;

                        int tov = 0;
                        int zak = 0;
                        int pos = 0;
                        int sotr = 0;
                        string dp = Parser(dateTimePicker5.Text.ToString());
                        string dot = Parser(dateTimePicker6.Text.ToString());
                        int id = Convert.ToInt32(dataGridView2.SelectedCells[0].Value);

                        foreach (int key in Articul)
                        {
                            if (key == Convert.ToInt32(comboBox7.Text))
                            {
                                tov = key;
                            }
                        }

                        foreach (int key in KodZakaz)
                        {
                            if (key == Convert.ToInt32(comboBox8.Text))
                            {
                                zak = key;
                            }
                        }

                        foreach (KeyValuePair<int, string> kv in NamePostav)
                        {
                            if (kv.Value == comboBox9.Text.ToString())
                            {
                                pos = kv.Key;
                            }
                        }

                        foreach (KeyValuePair<int, string> kv in NameSotrud)
                        {
                            if (kv.Value == comboBox10.Text.ToString())
                            {
                                sotr = kv.Key;
                            }
                        }

                        Sending s = new Sending();
                        s.UpdateSending(id, tov, zak, pos, sotr, dp, dot);
                    }
                    if (checkText)
                    {
                        MessageBox.Show("Запись успешно изменена!");
                        ClearText();
                    }
                    else
                    {
                        MessageBox.Show("Заполните пустые поля!");
                    }
                       
                    form1.ShowData();
                    ShowTable(but);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка подключения к БД!");
                MessageBox.Show(ex.Message, "Возможно, вы не выбрали запись или некорректно заполнили поля.");
            }
        }
        private void ShowTable(int var)
        {
            string sql = "";
            if (table == "Test")
            {
                Test ts = new Test();
                sql = ts.SelectShow;
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
            if (table == "страна")
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
                Order or = new Order();
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

            if (var == 5)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else if (var == 6)
            {
                dataGridView2.DataSource = ds.Tables[0];
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            form1.checkUT = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
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

        private void StartInitForm()
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            dateTimePicker3.Visible = false;
            dateTimePicker4.Visible = false;
            dateTimePicker5.Visible = false;
            dateTimePicker6.Visible = false;

            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;

            maskedTextBox1.Visible = false;
            maskedTextBox2.Visible = false;
            maskedTextBox3.Visible = false;
            maskedTextBox4.Visible = false;
            maskedTextBox5.Visible = false;
            maskedTextBox6.Visible = false;

            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            comboBox11.Visible = false;
            comboBox12.Visible = false;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (table == "Страна")
            {
                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
            }
            if (table == "Производитель")
            {
                textBox7.Text= dataGridView2.SelectedCells[1].Value.ToString();

                comboBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();
            }
            if (table == "Тип")
            {
                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
                textBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();

            }
            if (table == "Товар")
            {

                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();

                comboBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();
                comboBox9.Text = dataGridView2.SelectedCells[3].Value.ToString();

            }
            if (table == "Клиент")
            {
                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
                textBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();
                textBox9.Text = dataGridView2.SelectedCells[3].Value.ToString();

                string text = dataGridView2.SelectedCells[4].Value.ToString();
                maskedTextBox5.Text = text.Substring(1);

                textBox11.Text = dataGridView2.SelectedCells[5].Value.ToString();

            }
            if (table == "Заказ")
            {
                dateTimePicker4.Text = dataGridView2.SelectedCells[2].Value.ToString();

                comboBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();

            }
            if (table == "Поставщик")
            {
                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
                textBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();

                string text = dataGridView2.SelectedCells[4].Value.ToString();
                maskedTextBox4.Text = text.Substring(1);

            }
            if (table == "Должность")
            {
                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
                textBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();
                textBox9.Text = dataGridView2.SelectedCells[3].Value.ToString();

            }
            if (table == "Сотрудник")
            {

                textBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
                textBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();
                textBox9.Text = dataGridView2.SelectedCells[3].Value.ToString();

                string text = dataGridView2.SelectedCells[4].Value.ToString();
                maskedTextBox6.Text = text.Substring(1);

                comboBox10.Text = dataGridView2.SelectedCells[5].Value.ToString();
            }

            if (table == "Отправка")
            {
                dateTimePicker5.Text = dataGridView2.SelectedCells[5].Value.ToString();
                dateTimePicker6.Text = dataGridView2.SelectedCells[6].Value.ToString();

                comboBox7.Text = dataGridView2.SelectedCells[1].Value.ToString();
                comboBox8.Text = dataGridView2.SelectedCells[2].Value.ToString();
                comboBox9.Text = dataGridView2.SelectedCells[3].Value.ToString();
                comboBox10.Text = dataGridView2.SelectedCells[4].Value.ToString();
            }

            if (table == "Test")
            {
                textBox8.Text = dataGridView2.SelectedCells[1].Value.ToString();
                textBox9.Text = dataGridView2.SelectedCells[2].Value.ToString();
                textBox11.Text = dataGridView2.SelectedCells[3].Value.ToString();

                maskedTextBox5.Text = dataGridView2.SelectedCells[4].Value.ToString();
            }
        }

       
        private string Parser(string tmp)
        {
            string temp, tempYear = "", tempMounth = "", tempDay = "";
            temp = tmp;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != '.')
                {
                    if (i < 2)
                    {
                        tempDay += temp[i].ToString();
                        continue;
                    }
                    if (i < 5)
                    {
                        tempMounth += temp[i].ToString();
                        continue;
                    }
                    if (i < temp.Length)
                    {
                        tempYear += temp[i].ToString();
                        continue;
                    }

                }
            }
            return tempYear + "-" + tempMounth + "-" + tempDay;
        }
        private void ClearText()
        {
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";

            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            comboBox12.Text = "";
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Test "
                + "WHERE Name LIKE \"" + textBox13.Text + "%\" LIMIT 50 OFFSET 0";
            if (textBox13.Text.Length > 0)
            {
                ds = controll.ShowData(sql);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                ShowTable(but);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (ds.Tables[0].Rows.Count < 50) return;

            pageNumber += 50;
            string sql = "";
            if (table == "Test")
            {
                Test tst = new Test();
                sql = tst.GetSqlTest(pageNumber);
            }

            ds = controll.NextAndBackButton(sql);
            // Отображаем данные
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Visible = true;

            label16.Text = (pageNumber / 50).ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (pageNumber == 0) return;

            pageNumber -= 50;

            string sql = "";
            if (table == "Test")
            {
                Test tst = new Test();
                sql = tst.GetSqlTest(pageNumber);
            }

            ds = controll.NextAndBackButton(sql);

            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Visible = true;

            label16.Text = (pageNumber / 50).ToString();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Test "
               + "WHERE Name LIKE \"" + textBox14.Text + "%\" LIMIT 50 OFFSET 0";
            if (textBox14.Text.Length > 0)
            {
                ds = controll.ShowData(sql);
                dataGridView2.DataSource = ds.Tables[0];
            }
            else
            {
                ShowTable(but);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (ds.Tables[0].Rows.Count < 50) return;

            pageNumber += 50;
            string sql = "";
            if (table == "Test")
            {
                Test tst = new Test();
                sql = tst.GetSqlTest(pageNumber);
            }

            ds = controll.NextAndBackButton(sql);
            // Отображаем данные
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Visible = true;

            label17.Text = (pageNumber / 50).ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (pageNumber == 0) return;

            pageNumber -= 50;

            string sql = "";
            if (table == "Test")
            {
                Test tst = new Test();
                sql = tst.GetSqlTest(pageNumber);
            }

            ds = controll.NextAndBackButton(sql);

            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Visible = true;

            label17.Text = (pageNumber / 50).ToString();
        }
    }
}
