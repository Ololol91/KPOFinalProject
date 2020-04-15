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
    class Sending
    {
        private string connStr = "server=localhost;user=root;database=warehouse;password=root;";

        private string selectShow = "SELECT Отправка.ID AS НомерОтправки, Отправка.ID_Товара AS АртикулТовара, Отправка.ID_Заказа " +
                    "AS НомерЗаказа, Поставщик.Наименование AS Поставщик, Сотрудник.Фамилия AS Сотрудник, " +
                    "Отправка.Дата_Поставки, Отправка.Дата_Отправки FROM Отправка, Поставщик, Сотрудник " +
                    "Where Отправка.ID_Поставщика = Поставщик.ID AND Отправка.ID_Сотрудника = Сотрудник.ID";
        private int tov;
        private int zak;
        private int pos;
        private int sotr;
        private string dp;
        private string dot;
   
        public Sending() { }
        public Sending(int tov, int zak, int pos, int sotr, string dp, string dot)
        {
            this.tov = tov;
            this.zak = zak;
            this.pos = pos;
            this.sotr = sotr;
            this.dp = dp;
            this.dot = dot;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertSending(int tov, int zak, int pos, int sotr, string dp, string dot)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {             
                conn.Open();

                string sqlExpression = "INSERT INTO Отправка (ID_Товара, ID_Заказа, ID_Поставщика, ID_Сотрудника, Дата_Поставки, Дата_Отправки) "
                           + "VALUES (@tov, @zak, @pos, @sotr, @dp, @dot)";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                // создаем параметр для имени
                MySqlParameter tovParam = new MySqlParameter("@tov", tov);
                MySqlParameter zakParam = new MySqlParameter("@zak", zak);
                MySqlParameter posParam = new MySqlParameter("@pos", pos);
                MySqlParameter sotrParam = new MySqlParameter("@sotr", sotr);
                MySqlParameter dpParam = new MySqlParameter("@dp", dp);
                MySqlParameter dotParam = new MySqlParameter("@dot", dot);
                // добавляем параметр к команде
                command.Parameters.Add(tovParam);
                command.Parameters.Add(zakParam);
                command.Parameters.Add(posParam);
                command.Parameters.Add(sotrParam);
                command.Parameters.Add(dpParam);
                command.Parameters.Add(dotParam);

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
        public void UpdateSending(int id, int tov, int zak, int pos, int sotr, string dp, string dot)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {                 
                conn.Open();
                string sqlExpression = "UPDATE Отправка SET ID_Товара = @tov, ID_Заказа = @zak, ID_Поставщика = @pos, "
                                    + "ID_Сотрудника = @sotr, Дата_Поставки = @dp, Дата_Отправки = @dot WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter tovParam = new MySqlParameter("@tov", tov);
                MySqlParameter zakParam = new MySqlParameter("@zak", zak);
                MySqlParameter posParam = new MySqlParameter("@pos", pos);
                MySqlParameter sotrParam = new MySqlParameter("@sotr", sotr);
                MySqlParameter dpParam = new MySqlParameter("@dp", dp);
                MySqlParameter dotParam = new MySqlParameter("@dot", dot);
                MySqlParameter idParam = new MySqlParameter("@id", id);

                command.Parameters.Add(idParam);
                command.Parameters.Add(tovParam);
                command.Parameters.Add(zakParam);
                command.Parameters.Add(posParam);
                command.Parameters.Add(sotrParam);
                command.Parameters.Add(dpParam);
                command.Parameters.Add(dotParam);

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
