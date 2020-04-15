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
    class Employee
    {
        private string connStr = "server=localhost;user=root;database=warehouse;password=root;";

        private string selectShow = "SELECT Сотрудник.Фамилия, Сотрудник.Имя, Сотрудник.Отчество, Сотрудник.Телефон, " +
                    "Должность.Наименование AS Должность FROM Сотрудник, Должность WHERE Сотрудник.ID_Должности = Должность.ID";
        private string fam;
        private string name;
        private string otch;
        private string tel;
        private int dolzh;
        public Employee() { }
        public Employee(string fam, string name, string otch, string tel, int dolzh)
        {
            this.fam = fam;
            this.name = name;
            this.otch = otch;
            this.tel = tel;
            this.dolzh = dolzh;
        }
        public string SelectShow
        {
            get { return selectShow; }
        }
        public void InsertEmployee(string fam, string name, string otch, string tel, int dolzh)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sqlExpression = "INSERT INTO Сотрудник (Фамилия, Имя, Отчество, Телефон, ID_Должности) "
                            + "VALUES (@fam, @name, @otch, @tel, @dolzh)";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);
                // создаем параметр для имени
                MySqlParameter famParam = new MySqlParameter("@fam", fam);
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter otchParam = new MySqlParameter("@otch", otch);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter dolzhParam = new MySqlParameter("@dolzh", dolzh);
                // добавляем параметр к команде
                command.Parameters.Add(famParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(otchParam);
                command.Parameters.Add(telParam);
                command.Parameters.Add(dolzhParam);

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
        public void UpdateEmployee(int id, string fam, string name, string otch, string tel, int dolzh)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            { 
                conn.Open();
                string sqlExpression = "UPDATE Сотрудник SET Фамилия = @fam, Имя = @name, "
                                        + "Отчество = @otch, Телефон = @tel, ID_Должности = @dolzh WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter famParam = new MySqlParameter("@fam", fam);
                MySqlParameter nameParam = new MySqlParameter("@name", name);
                MySqlParameter otchParam = new MySqlParameter("@otch", otch);
                MySqlParameter telParam = new MySqlParameter("@tel", tel);
                MySqlParameter dolzhParam = new MySqlParameter("@dolzh", dolzh);
                MySqlParameter idParam = new MySqlParameter("@id", id);

                command.Parameters.Add(idParam);
                command.Parameters.Add(famParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(otchParam);
                command.Parameters.Add(telParam);
                command.Parameters.Add(dolzhParam);

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
