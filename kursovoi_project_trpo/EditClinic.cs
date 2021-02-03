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
    public partial class EditClinic : Form
    {
        int id = 0;
        public EditClinic(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void EditClinic_Load(object sender, EventArgs e)
        {
            textBox1.Text = id.ToString();
            comboBox1.Items.AddRange(CDisease.GetListName().ToArray());
            comboBox2.Items.AddRange(CDepartment.ListNumberOtd().ToArray());
          
             using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
             {
                 string query = "SELECT * FROM Clinic WHERE Код = @id ;";
                 using (OleDbCommand command = new OleDbCommand(query, connection))
                 {
                     command.Parameters.AddWithValue("@id",id); 
                     connection.Open();
                     OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                     DataTable dtRecord = new DataTable();
                     sqlDataAdap.Fill(dtRecord);
                    comboBox1.SelectedItem = CDisease.GetNameById( Convert.ToInt32(dtRecord.Rows[0].ItemArray[1]));
                    comboBox2.SelectedItem = CDepartment.GetNameById( Convert.ToInt32(dtRecord.Rows[0].ItemArray[2]));
                    numericUpDown1.Value =  Convert.ToInt32(dtRecord.Rows[0].ItemArray[3]);
                    numericUpDown2.Value =  Convert.ToInt32(dtRecord.Rows[0].ItemArray[4]);
                    numericUpDown3.Value =  Convert.ToInt32(dtRecord.Rows[0].ItemArray[5]);
                    //dataGridView1.DataSource = dtRecord;
                    connection.Close();
                 }
             } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                String query = "UPDATE Clinic SET Name = @name, NumberOtd = @numberOtd, DaysHealth= @daysHealth, PriceDay= @priceDay, PriceMedicoment= @priceMedicoment WHERE Код = @id;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", CDisease.getId( comboBox1.SelectedItem.ToString()));
                    command.Parameters.AddWithValue("@numberOtd",CDepartment.getId(comboBox2.SelectedItem.ToString()));
                    command.Parameters.AddWithValue("@daysHealth", numericUpDown1.Value);
                    command.Parameters.AddWithValue("@priceDay", numericUpDown2.Value);
                    command.Parameters.AddWithValue("@priceMedicoment", numericUpDown3.Value); 
                    command.Parameters.AddWithValue("@id", id);
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
