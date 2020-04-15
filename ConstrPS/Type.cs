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
    class Type
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Тип.Наименование, Тип.Описание FROM Тип";
        private string name;
        private string desc;
        public Type() { }
        public Type(string name, string desc)
        {
            this.name = name;
            this.desc = desc;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertType(string name, string opis)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {      
                conn.Open();

                string sqlExpression = "INSERT INTO Тип (Наименование, Описание) VALUES (@name, @opis)";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                command.Parameters.Add(nameParam);

                MySqlParameter opisParam = new MySqlParameter("@opis", opis);
                command.Parameters.Add(opisParam);

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
        public void UpdateType(int id, string name, string opis)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {                
                conn.Open();
                string sqlExpression = "UPDATE Тип SET Наименование = @name, Описание = @opis WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                command.Parameters.Add(nameParam);

                MySqlParameter opisParam = new MySqlParameter("@opis", opis);
                command.Parameters.Add(opisParam);

                MySqlParameter idParam = new MySqlParameter("@id", id);
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
