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
                    command.Parameters.AddWithValue("@name", CDisease.getId(comboBox1.SelectedItem.ToString()));
                    command.Parameters.AddWithValue("@numberOtd", CDepartment.getId(comboBox2.SelectedItem.ToString()));
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

        private void AddClinic_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(CDisease.GetListName().ToArray());
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.AddRange(CDepartment.ListNumberOtd().ToArray());
            comboBox2.SelectedIndex = 0;
        }
    }
}
