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

        public bool Auth(string login, string pass)
        {
            var myConn = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString);
            myConn.Open();

            var query = "Select * From Users Where username = '" + login+ "' and password = '" + pass + "'";

            var sda = new OleDbDataAdapter(query, myConn);

            var dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                return true;
               
            }
            return false;
           
        }
        private void button1_Click(object sender, EventArgs e)
        { 
            if(Auth(textBox1.Text.Trim(), textBox2.Text.Trim()))
            {
                MainForm f = new MainForm();
                this.SetVisibleCore(false);
                f.FormClosed  += new FormClosedEventHandler(frm_FormClosed); 
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }
        private void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
