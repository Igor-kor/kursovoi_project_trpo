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
    public partial class AddClinic : Form
    {
        public AddClinic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                String query = "INSERT INTO Clinic([Name], [NumberOtd], [DaysHealth], [PriceDay], [PriceMedicoment]) VALUES (?,?,?,?,?)";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", 1);
                    command.Parameters.AddWithValue("@numberOtd", 1);
                    command.Parameters.AddWithValue("@daysHealth", numericUpDown1.Value);
                    command.Parameters.AddWithValue("@priceDay",numericUpDown2.Value);
                    command.Parameters.AddWithValue("@priceMedicoment", numericUpDown3.Value);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Произошла ошибка при добавлении данных!");
                    else MessageBox.Show("Добавление прошло успешно!");

                    connection.Close();
                }
            }
        }
    }
}
