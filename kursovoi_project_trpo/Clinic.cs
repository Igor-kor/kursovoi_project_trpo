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
    public partial class Clinic : Form
    {
        int idLow = -1;
        public Clinic()
        {
            InitializeComponent();
        }

        private void Clinic_Load(object sender, EventArgs e)
        {
            ReloadTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                comboBox1.Items.Add(column.Name);
            }
            comboBox1.SelectedIndex = 0;
            comboBox4.Items.AddRange(ListNames().ToArray());
            comboBox2.Items.AddRange(ListNames().ToArray());
            comboBox3.Items.AddRange(ListNumberOtd().ToArray());
            textBox4.Text = FindMinDay();
            radioButton1.Checked = true;
        }

        private void ReloadTable()
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Clinic;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }

        private List<string> ListNames()
        {
            List<string> names = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT Name FROM Clinic;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        if(!names.Contains(row[0].ToString()))names.Add(row[0].ToString());
                    }
                    connection.Close();
                }
            }
            return names;
        }

        private List<string> ListNumberOtd()
        {
            List<string> names = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT NumberOtd FROM Clinic;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        if (!names.Contains(row[0].ToString())) names.Add(row[0].ToString());
                    }
                    connection.Close();
                }
            }
            return names;
        }

        private void SortTable()
        {
            dataGridView1.Sort(dataGridView1.Columns[comboBox1.SelectedIndex], radioButton1.Checked ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortTable();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            SortTable();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SortTable();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new ViewOtd(Convert.ToInt32(comboBox3.SelectedItem));
            form.Show();
        }

        private string FindMinDay()
        {
            string name;
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Clinic ;";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {                   
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dtRecord.DefaultView.Sort = "PriceDay asc";
                    dtRecord = dtRecord.DefaultView.ToTable();
                    if(dtRecord.Rows.Count == 0)
                    {
                        idLow = -1;
                        connection.Close();
                        return "Не найдено";
                    }
                    name = dtRecord.Rows[0][1].ToString();
                    idLow = Convert.ToInt32(dtRecord.Rows[0][0].ToString());
                    connection.Close();
                }
            }
            return name;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Clinic Where Name = @Name;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumberOtd", comboBox4.SelectedItem);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    textBox3.Text = "";
                    int sredStoimost = 0;
                    int CountItem = 0;
                    textBox1.Text = "";
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        CountItem++;
                        sredStoimost += Convert.ToInt32(row[4].ToString());
                        textBox3.Text += (Convert.ToInt32(row[3].ToString()) * Convert.ToInt32(row[4].ToString()) + Convert.ToInt32(row[5].ToString())) + ", ";
                        textBox1.Text += Convert.ToInt32(row[5].ToString()) + ", ";
                    }
                    textBox2.Text =""+ (sredStoimost / CountItem);
                    connection.Close();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (idLow == -1) return;
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "DELETE FROM Clinic WHERE Код = @id;";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idLow);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command); 
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                        MessageBox.Show("Произошла ошибка при удалении данных!");
                    else
                    { 
                        this.clinicTableAdapter1.Fill(this.databaseDataSet1.Clinic);
                        
                        MessageBox.Show("Удаление прошло успешно!");
                    }  
                    connection.Close();
                }
            }
            textBox4.Text = FindMinDay();
        }
    }
}
