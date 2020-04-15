using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ConstrPS
{
    class ClassController
    {
        private static string connStr = "server=localhost;user=root;database=warehouse;password=root;";
        MySqlConnection connection = new MySqlConnection(connStr);

        static private int pageSize = 1;

        private Form1 form1;
        private UpdateTable form2;
            
        private MySqlDataAdapter adapter;
        private DataSet ds;

        public ClassController(UpdateTable form2)
        {
            this.form2 = form2;
        }
        public ClassController(Form1 form1)
        {
            this.form1 = form1;
        }
        public int GetSizeTable(string table)
        {
            int num = 0;
            try
            {
                connection.Open();

                string sql = "SHOW TABLE STATUS FROM warehouse WHERE Name = @table;";

                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlParameter tableParam = new MySqlParameter("@table", table);
                command.Parameters.Add(tableParam);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    num = Convert.ToInt32(reader[4]) / pageSize;
                }

                reader.Close();
                return num;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return 0;
            }
            finally
            {
                connection.Close();              
            }            
        }
        public bool DeleteFromTable(string table, string id)
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();           
                command.Connection = connection;
                command.CommandText = "DELETE FROM " + table + " Where ID = " + id + "";
                command.ExecuteNonQuery();              
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show("Возможно, вы не выбрали запись из таблицы.\n\nИли выбранная запись используется в другой таблице.", "Ошибка!");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
     
        public DataSet ShowData(string sql)
        {
            try
            { 
                connection.Open();
 
                adapter = new MySqlDataAdapter(sql, connection);

                ds = new DataSet();

                adapter.Fill(ds);

                return ds;          
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataSet NextAndBackButton(string sql)
        {
            try
            {
                connection.Open();

                adapter = new MySqlDataAdapter(sql, connection);

                ds = new DataSet();

                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<string> InitTreeView()
        {
            List<string> TreeView = new List<string>();
            
            try
            {                
                string sql = "SHOW TABLES";
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TreeView.Add(reader.GetString(0));
                }
                
                return TreeView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }

        }
        public List<string> InitBaseNumbers()
        {
            try
            {
                List<string> BaseNumbers = new List<string>();

                connection.Open();

                string query = "SELECT Телефон FROM Клиент";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseNumbers.Add(reader.GetString(0));
                }

                reader.Close();               

                return BaseNumbers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Dictionary<int, string> InitNameStran()
        {
            Dictionary<int, string> NameStran = new Dictionary<int, string>();
            

            try
            {
                connection.Open();

                string query = "SELECT Страна.ID, Страна.Название FROM Страна";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NameStran.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();

                return NameStran;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Dictionary<int, string> InitNameType()
        {
            Dictionary<int, string> NameType = new Dictionary<int, string>();
            
            try
            {
                connection.Open();

                string query = "SELECT Тип.ID, Тип.Наименование FROM Тип";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NameType.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();

                return NameType;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Dictionary<int, string> InitNameProizv()
        {
            Dictionary<int, string> NameProizv = new Dictionary<int, string>();
            
            try
            {
                connection.Open();

                string query = "SELECT Производитель.ID, Производитель.Наименование FROM Производитель";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NameProizv.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();

                return NameProizv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Dictionary<int, string> InitNumberClient()
        {            
            Dictionary<int, string> NumberClient = new Dictionary<int, string>();

            try
            {
                connection.Open();

                string query = "SELECT Клиент.ID, Клиент.Телефон FROM Клиент";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NumberClient.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();

                return NumberClient;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }          
        }

        public Dictionary<int, string> InitNameDolzh()
        {
            Dictionary<int, string> NameDolzh = new Dictionary<int, string>();
           
            try
            {
                connection.Open();

                string query = "SELECT Должность.ID, Должность.Наименование FROM Должность";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NameDolzh.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();

                return NameDolzh;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<int> InitArticul()
        {
            List<int> Articul = new List<int>();

            try
            {
                connection.Open();

                string query = "SELECT Товар.ID FROM Товар";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Articul.Add(Convert.ToInt32(reader[0]));
                }

                reader.Close();
                Articul.Sort();
                return Articul;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }       
        }
        public List<int> InitKodZakaz()
        {
            List<int> KodZakaz = new List<int>();
            
            try
            {
                connection.Open();

                string query = "SELECT Заказ.ID FROM Заказ";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    KodZakaz.Add(Convert.ToInt32(reader[0]));
                }

                reader.Close();
                KodZakaz.Sort();
                return KodZakaz;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public Dictionary<int, string> InitNamePostav()
        {
            Dictionary<int, string> NamePostav = new Dictionary<int, string>();

            try
            {
                connection.Open();

                string query = "SELECT Поставщик.ID, Поставщик.Наименование FROM Поставщик";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NamePostav.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();

                return NamePostav;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }

        }
        public Dictionary<int, string> InitNameSotrud()
        {
            Dictionary<int, string> NameSotrud = new Dictionary<int, string>();

            try
            {
                connection.Open();

                string query = "SELECT Сотрудник.ID, Сотрудник.Фамилия FROM Сотрудник";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NameSotrud.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
                }

                reader.Close();
                
                return NameSotrud;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД!");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
