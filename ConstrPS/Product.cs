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
    class Product
    {
        private string connStr = "server=localhost;user=root;database=sclad;password=;";

        private string selectShow = "SELECT Товар.ID AS Артикул, Товар.Наименование, Тип.Описание AS ОписаниеТовара, " +
                    "Производитель.Наименование AS Производитель FROM Товар, Тип, Производитель WHERE Товар.ID_Типа = Тип.ID " +
                    "AND Товар.ID_Производителя = Производитель.ID";
        private string name;
        private int type;
        private int creator;
        public Product() { }
        public Product(string name, int type, int creator)
        {
            this.name = name;
            this.type = type;
            this.creator = creator;
        }

        public string SelectShow
        {
            get { return selectShow; }
        }

        public void InsertProduct(string name, int type, int proizv)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {               
                conn.Open();

                string sqlExpression = "INSERT INTO Товар (Наименование, ID_Типа, ID_Производителя) VALUES (@name, @type, @proizv)";
                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                command.Parameters.Add(nameParam);
                MySqlParameter typeParam = new MySqlParameter("@type", type);
                command.Parameters.Add(typeParam);
                MySqlParameter proizvParam = new MySqlParameter("@proizv", proizv);
                command.Parameters.Add(proizvParam);

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
        public void UpdateProduct(int id, string name, int type, int proizv)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {        
                conn.Open();
                string sqlExpression = "UPDATE Товар SET Наименование = @name, ID_Типа = @type, ID_Производителя = @proizv WHERE ID = @id";

                MySqlCommand command = new MySqlCommand(sqlExpression, conn);

                MySqlParameter nameParam = new MySqlParameter("@name", name);
                command.Parameters.Add(nameParam);

                MySqlParameter typeParam = new MySqlParameter("@type", type);
                command.Parameters.Add(typeParam);

                MySqlParameter proizvParam = new MySqlParameter("@proizv", proizv);
                command.Parameters.Add(proizvParam);

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
