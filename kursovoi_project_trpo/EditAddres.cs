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
    public partial class EditAddres : Form
    {
        int id = -1;
        public EditAddres(int id)
        {
            this.id = id;
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
                String query = "UPDATE Adress SET First_name = @first_name, Second_name = @second_name, Birthday = @birthday, Company = @company, Phone_number = @phone_number WHERE Id = @id";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@first_name", textBox1.Text);
                    command.Parameters.AddWithValue("@second_name", textBox2.Text);
                    command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);
                    command.Parameters.AddWithValue("@company", textBox4.Text);
                    command.Parameters.AddWithValue("@phone_number", textBox5.Text);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Произошла ошибка при изменении данных!");
                    else MessageBox.Show("Изменение прошло успешно!");

                    connection.Close();
                }
            }
            this.Close();
        }

        private void EditAddres_Load(object sender, EventArgs e)
        {
            textBox3.Text = id.ToString();

            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Adress WHERE Id = @id ;";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    textBox1.Text = dtRecord.Rows[0].ItemArray[1].ToString();
                    textBox2.Text = dtRecord.Rows[0].ItemArray[2].ToString();
                    dateTimePicker1.Value = (DateTime)dtRecord.Rows[0].ItemArray[3]; 
                    textBox4.Text = dtRecord.Rows[0].ItemArray[4].ToString();
                    textBox5.Text = dtRecord.Rows[0].ItemArray[5].ToString(); 
                    connection.Close();
                }
            }

          
        }
    }
}
