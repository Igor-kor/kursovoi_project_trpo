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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myConn = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString);
            myConn.Open();

            var query = "Select * From Users Where username = '" + textBox1.Text.Trim() + "' and password = '" + textBox2.Text.Trim() + "'";

            var sda = new OleDbDataAdapter(query, myConn);

            var dt = new DataTable();

            sda.Fill(dt);

            if(dt.Rows.Count == 1)
            {
                MainForm f = new MainForm();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }
    }
}
