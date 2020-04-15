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
    class Order
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Заказ.ID AS НомерЗаказа, Клиент.Телефон AS НомерКлиента," +
                    "Заказ.Дата_Заказа FROM Заказ, Клиент WHERE Заказ.ID_Клиента = Клиент.ID";
        private int numb;
        private string date;
        public Order() { }
        public Order(int numb, string date)
        {
            this.numb = numb;
            this.date = date;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertOrder(int numb, string date)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {             
                conn.Open();

                string sqlExpression = "INSERT INTO Заказ (ID_Клиента, Дата_Заказа) VALUES (@numb, @date)";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter numbParam = new MySqlParameter("@numb", numb);
                command.Parameters.Add(numbParam);
                MySqlParameter dateParam = new MySqlParameter("@date", date);
                command.Parameters.Add(dateParam);

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
        public void UpdateOrder(int id, int numb, string date)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {                
                conn.Open();
                string sqlExpression = "UPDATE Заказ SET ID_Клиента = @numb, Дата_Заказа = @date "
                                + " WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter numbParam = new MySqlParameter("@numb", numb);
                command.Parameters.Add(numbParam);
                MySqlParameter dateParam = new MySqlParameter("@date", date);
                command.Parameters.Add(dateParam);
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
