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
    class Creator
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Производитель.Наименование, Страна.Название AS НазваниеСтраны " +
                    "FROM Производитель, Страна WHERE Производитель.ID_Страны = Страна.ID";
        private string name;
        private int idCountry;
        public Creator() { }
        public Creator(string name, int idCountry)
        {
            this.name = name;
            this.idCountry = idCountry;
        }

        public string SelectShow
        {
            get { return selectShow; }         
        }
        public void InsertCreator(string name, int stran)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {     
                conn.Open();

                string sqlExpression = "INSERT INTO Производитель (Наименование, ID_Страны) VALUES (@name, @stran)";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                command.Parameters.Add(nameParam);
                MySqlParameter stranParam = new MySqlParameter("@stran", stran);
                command.Parameters.Add(stranParam);

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
        public void UpdateCreator(int id, string name, int stran)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {         
                conn.Open();
                string sqlExpression = "UPDATE Производитель SET Наименование = @name, ID_Страны = @stran WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                command.Parameters.Add(nameParam);
                MySqlParameter stranParam = new MySqlParameter("@stran", stran);
                command.Parameters.Add(stranParam);

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
