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
    public partial class AddEvent : Form
    {
        public AddEvent()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                String query = "INSERT INTO Events([Date], [Time], [Event_name]) VALUES (?,?,?)";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                { 
                    command.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);                  
                    command.Parameters.AddWithValue("@time", dateTimePicker2.Value.TimeOfDay);
                    command.Parameters.AddWithValue("@event_name", richTextBox1.Text);
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

        private void AddEvent_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
        }
    }
}
