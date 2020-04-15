using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ConstrPS
{
    class Provider
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Поставщик.Наименование, Поставщик.Адрес, Поставщик.Телефон FROM Поставщик";
        private string name;      
        private string adres;
        private string tel;
        public Provider() { }
        public Provider(string name, string tel, string adres)
        {
            this.name = name;
            this.tel = tel;
            this.adres = adres;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertProvider(string name, string tel, string adres)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sqlExpression = "INSERT INTO Поставщик (Наименование, Адрес, Телефон) "
                           + "VALUES (@name, @adres, @tel)";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                // создаем параметр для имени
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter adresParam = new MySqlParameter("@adres", adres);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                command.Parameters.Add(telParam);
                command.Parameters.Add(adresParam);

                command.ExecuteNonQuery();         
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления записи!\n\nПроверьте подключение!");
            }
            finally
            {
                conn.Close();
            }

        }
        public void UpdateProvider(int id, string name, string tel, string adres)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            { 
                conn.Open();
                string sqlExpression = "UPDATE Поставщик  SET Наименование = @name, Адрес = @adres, "
                               + "Телефон = @tel WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter adresParam = new MySqlParameter("@adres", adres);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter idParam = new MySqlParameter("@id", id);

                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(adresParam);
                command.Parameters.Add(telParam);

                command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения записи!\n\nПроверьте подключение!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
