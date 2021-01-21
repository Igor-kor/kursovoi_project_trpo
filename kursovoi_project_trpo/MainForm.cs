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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Events WHERE Date = @date";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", DateTime.Today);
                    connection.Open(); 
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new AddEvent();
            form.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Events WHERE Date = @date";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", DateTime.Today);
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
            Form form = new Addres();
            form.Show();
        }

        private void адреснаяКнигаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Addres();
            form.Show();
        }

        private void событияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Events();
            form.Show();
        }

        private void клиникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Clinic();
            form.Show();
        }
    }
}
