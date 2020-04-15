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
    class Client
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Клиент.Фамилия, Клиент.Имя, Клиент.Отчество, Клиент.Телефон, Клиент.Адрес FROM Клиент";
        private string fam;
        private string name;
        private string otch;
        private string tel;
        private string adres;
        public Client() { }
        public Client(string fam, string name, string otch, string tel, string adres)
        {
            this.fam = fam;
            this.name = name;
            this.otch = otch;
            this.tel = tel;
            this.adres = adres;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertClient(string fam, string name, string otch, string tel, string adres)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sqlExpression = "INSERT INTO Клиент (Фамилия, Имя, Отчество, Телефон, Адрес) "
                            + "VALUES (@fam, @name, @otch, @tel, @adres)";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                // создаем параметр для имени
                MySqlParameter famParam = new MySqlParameter("@fam", fam);
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter otchParam = new MySqlParameter("@otch", otch);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter adresParam = new MySqlParameter("@adres", adres);
                // добавляем параметр к команде
                command.Parameters.Add(famParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(otchParam);
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
        public void UpdateClient(int id, string fam, string name, string otch, string tel, string adres)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sqlExpression = "UPDATE Клиент SET Фамилия = @fam, Имя = @name, "
                                + "Отчество = @otch, Телефон = @tel, Адрес = @adres WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter famParam = new MySqlParameter("@fam", fam);
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter otchParam = new MySqlParameter("@otch", otch);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter adresParam = new MySqlParameter("@adres", adres);
                MySqlParameter idParam = new MySqlParameter("@id", id);

                command.Parameters.Add(idParam);
                command.Parameters.Add(famParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(otchParam);
                command.Parameters.Add(telParam);
                command.Parameters.Add(adresParam);

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
