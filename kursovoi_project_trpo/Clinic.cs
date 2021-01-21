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
    }
}
