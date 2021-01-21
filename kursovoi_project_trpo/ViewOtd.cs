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
    public partial class ViewOtd : Form
    {
        int NumberOtd = 0;
        public ViewOtd(int NumberOtd = 0)
        {
            this.NumberOtd = NumberOtd;
            InitializeComponent();
        }

        private void ViewOtd_Load(object sender, EventArgs e)
        {
            ReloadTable(NumberOtd);
        }

        private void ReloadTable(int number)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT Name, DaysHealth, PriceDay, PriceMedicoment FROM Clinic WHERE NumberOtd = @NumberOtd ;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumberOtd", number);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }
    }
}
