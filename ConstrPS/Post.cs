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
    class Post
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Должность.Наименование, Должность.Зарплата, Должность.Обязанности FROM Должность";
        private string name;
        private string zarp;
        private string obyaz;
        public Post() { }
        public Post(string name, string zarp, string obyaz)
        {
            this.name = name;
            this.zarp = zarp;
            this.obyaz = obyaz;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertPost(string name, string zarp, string obyaz)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {          
                conn.Open();

                string sqlExpression = "INSERT INTO Должность (Наименование, Зарплата, Обязанности) "
                           + "VALUES (@name, @zarp, @obyaz)";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter zarpParam = new MySqlParameter("@zarp", zarp);
                MySqlParameter obyazParam = new MySqlParameter("@obyaz", obyaz);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                command.Parameters.Add(zarpParam);
                command.Parameters.Add(obyazParam);

                command.ExecuteNonQuery();
                conn.Close();
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
        public void UpdatePost(int id, string name, string zarp, string obyaz)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {              
                conn.Open();
                string sqlExpression = "UPDATE Должность SET Наименование = @name, Зарплата = @zarp, "
                                + "Обязанности = @obyaz WHERE ID = @id";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter zarpParam = new MySqlParameter("@zarp", zarp);
                MySqlParameter obyazParam = new MySqlParameter("@obyaz", obyaz);
                MySqlParameter idParam = new MySqlParameter("@id", id);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                command.Parameters.Add(zarpParam);
                command.Parameters.Add(obyazParam);        
                command.Parameters.Add(idParam);

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
