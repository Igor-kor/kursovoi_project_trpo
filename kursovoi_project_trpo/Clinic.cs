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

        public void Clinic_Load(object sender, EventArgs e)
        {
            ReloadTable();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                comboBox1.Items.Add(column.Name);
            }
            comboBox1.SelectedIndex = 0;
            comboBox4.Items.AddRange(ListNames().ToArray());
            comboBox2.Items.AddRange(ListNames().ToArray());
            comboBox3.Items.AddRange(CDepartment.ListNumberOtd().ToArray());
            textBox4.Text = CDisease.GetNameById( Convert.ToInt32( FindMinDay()));
            radioButton1.Checked = true;
        }

        private void ReloadTable()
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {

                string query = @"SELECT A.Код, C.NumberDepartment, B.NameDisease, A.DaysHealth, A.PriceDay, A.PriceMedicoment 
                                FROM Clinic A, Disease B, Department C,
                                A LEFT JOIN B ON A.Name = B.Код,
                                A LEFT JOIN C ON A.NumberOtd = C.Код ;";
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

        public List<string> ListNames()
        {
            return CDisease.GetListName();
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
            Form form = new ViewOtd(CDepartment.getId(comboBox3.SelectedItem.ToString()));
            form.Show();

        }

        public string FindMinDay()
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
                    if (dtRecord.Rows.Count == 0)
                    {
                        idLow = -1;
                        connection.Close();
                        return "-1";
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
                    command.Parameters.AddWithValue("@NumberOtd",CDisease.getId( comboBox4.SelectedItem.ToString()));
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
                    if (CountItem > 0)
                        textBox2.Text = "" + (sredStoimost / CountItem);
                    connection.Close();
                }
            }
        }

        public void button7_Click(object sender, EventArgs e)
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
            textBox4.Text = CDisease.GetNameById(Convert.ToInt32(FindMinDay()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    String query = "DELETE FROM Clinic WHERE Код = @id";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells[0].Value);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                        DataTable dtRecord = new DataTable();
                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Произошла ошибка при удалении данных!");
                        else
                        {
                            sqlDataAdap.Fill(dtRecord);
                            MessageBox.Show("Удаление прошло успешно!");
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Выберите пожалуйста одну полную строку");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new AddClinic();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "UPDATE Clinic SET PriceDay =  PriceDay + ( PriceDay / 100 * " + numericUpDown1.Value + " )  WHERE Name = @id;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", CDisease.getId( comboBox2.SelectedItem.ToString()));
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);

                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
               Form form = new EditClinic(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                form.Show();

            }
            else
            {
                MessageBox.Show("Выберите пожалуйста одну полную строку");
            }
      
        }
    }
}
