﻿using System;
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
    public partial class Addres : Form
    {
        public Addres()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Adress WHERE " +
                    (checkBox1.Checked ? "second_name = @last_name AND " : "second_name = second_name AND ") +
                    (checkBox2.Checked ? "Birthday = @birthday AND " : "Birthday = Birthday AND ") +
                    (checkBox3.Checked ? "Company = @company " : "Company = Company") + " ;"; 

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (checkBox1.Checked) command.Parameters.AddWithValue("@last_name", textBox1.Text);
                    if (checkBox2.Checked) command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);
                    if (checkBox3.Checked) command.Parameters.AddWithValue("@company", textBox2.Text);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command); 
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord); 
                    dataGridView1.DataSource = dtRecord; 
                    connection.Close();
                }
            }
        }

        private void Addres_Load(object sender, EventArgs e)
        {
            ReloadTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReloadTable();
        }


        private void  ReloadTable()
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Adress;";

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

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new AddAddres();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Form form = new EditAddres(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                form.Show();
            }
            else
            {
                MessageBox.Show("Выберите одну строку");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    String query = "DELETE FROM Adress WHERE Id = @id";

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
    }
}
