using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovoi_project_trpo
{
    public partial class AddAddres : Form
    {
        public AddAddres()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                String query = "INSERT INTO Adress([First_name], [Second_name], [Birthday], [Company], [Phone_number]) VALUES (?,?,?,?,?)";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@first_name",textBox1.Text);
                    command.Parameters.AddWithValue("@second_name", textBox2.Text);
                    command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);
                    command.Parameters.AddWithValue("@company", textBox4.Text);
                    command.Parameters.AddWithValue("@phone_number", textBox5.Text);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Произошла ошибка при добавлении данных!");
                    else MessageBox.Show("Добавление прошло успешно!");

                    connection.Close();
                }
            } 
            this.Close();
        }
    }
}
