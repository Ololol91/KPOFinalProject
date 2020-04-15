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
    class Country
    {
        private string connStr = "server=localhost;user=root;database=warehouse;password=root;";

        private string selectShow = "SELECT Страна.Название FROM Страна";
        private string name;
        public Country() { }
        public Country(string nm)
        {
            this.name = nm;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertCountry(string name)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sqlExpression = "INSERT INTO Страна (Название) VALUES (@name)";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                // создаем параметр для имени
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);

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
        public void UpdateCountry(string name, int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {                
                conn.Open();
                string sqlExpression = "UPDATE Страна SET Название = @name WHERE ID = @id";
          
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter idParam = new MySqlParameter("@id", id);
                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);

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
