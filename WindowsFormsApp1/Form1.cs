using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox2.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox3.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
        }
        private void LoadData()
        {
            server = "localhost";
            database = "gavrilov_db";
            uid = "root";
            password = "123456";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            connection.Open();
            List<string[]> data = new List<string[]>();

            string sql = "SELECT ID,name,address FROM Inform_firm";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, connection);
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT

                // MessageBox.Show(reader[0].ToString() + " " + reader[1].ToString()+ " " + reader[2].ToString() , "Заголовок сообщения", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД https://vscode.ru/prog-lessons/mysql-c-sharp.html
            connection.Close();
            foreach (string[] s in data) { 
                dataGridView1.Rows.Add(s);
        }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = "localhost";
            database = "gavrilov_db";
            uid = "root";
            password = "123456";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "Insert into Inform_firm (ID,name,address) "
                                 + " values (@ID,@name,@address) ";

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Создать объект @id.
            MySqlParameter gradeParam = new MySqlParameter("@ID", MySqlDbType.Int64);
            gradeParam.Value = dataGridView1[0, dataGridView1.CurrentRow.Index].Value;
            cmd.Parameters.Add(gradeParam);

            // Добавить параметр @name (Написать кратко).
            MySqlParameter highSalaryParam = cmd.Parameters.Add("@name", MySqlDbType.String);
            highSalaryParam.Value = dataGridView1[1, dataGridView1.CurrentRow.Index].Value;

            // Добавить параметр @address (Написать кратко).
            cmd.Parameters.Add("@address", MySqlDbType.String).Value = dataGridView1[2, dataGridView1.CurrentRow.Index].Value;

            // Выполнить Command (использованная для  delete, insert, update).
            try
            {
                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            server = "localhost";
            database = "gavrilov_db";
            uid = "root";
            password = "123456";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = "delete from Inform_firm  "
                                 + "where ID=@ID ";

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            // Создать объект @id.
            MySqlParameter gradeParam = new MySqlParameter("@ID", MySqlDbType.Int64);
            gradeParam.Value = dataGridView1[0, dataGridView1.CurrentRow.Index].Value;
            cmd.Parameters.Add(gradeParam);

            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
