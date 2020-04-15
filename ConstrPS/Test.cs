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
    class Test
    {
        static private string connStr = "server=localhost;user=root;database=sclad;password=;";
        MySqlConnection conn = new MySqlConnection(connStr);
        static private int pageSize = 1;

        private string selectShow = "SELECT * FROM Test LIMIT " + pageSize + " OFFSET 1";
        public Test() { }
        public string SelectShow
        {
            get { return selectShow; }
        }

        public string GetSqlTest(int pageNumber)
        {
            return "SELECT * FROM Test LIMIT " + pageSize + " OFFSET " + pageNumber;
        }

        public void InsertTest(string name, string adress, string pasport, string tel)
        {            
            try
            {
                conn.Open();

                string sqlExpression = "INSERT INTO Test (Name, Adress, Pasport, Number) "
                            + "VALUES (@name, @adress, @pasp, @tel)";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                // создаем параметр для имени
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter adressParam = new MySqlParameter("@adress", adress);
                MySqlParameter pasportParam = new MySqlParameter("@pasp", pasport);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);

                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                command.Parameters.Add(pasportParam);
                command.Parameters.Add(telParam);
                command.Parameters.Add(adressParam);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Ошибка добавления записи!\n\nПроверьте подключение!");
            }
            finally
            {
                conn.Close();
            }

        }
        public void UpdateTest(int id, string fam, string name, string otch, string tel)
        {
            try
            {
                conn.Open();
                string sqlExpression = "UPDATE Test SET Name = @fam, Adress = @name, "
                                + "Pasport = @otch, Number = @tel WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter famParam = new MySqlParameter("@fam", fam);
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter otchParam = new MySqlParameter("@otch", otch);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter idParam = new MySqlParameter("@id", id);

                command.Parameters.Add(idParam);
                command.Parameters.Add(famParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(otchParam);
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
