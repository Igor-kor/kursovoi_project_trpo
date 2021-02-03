using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
           /* using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT * FROM Clinic WHERE  Date >= @start AND  Date <= @end ;";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@start", dateTimePicker1.Value.Date);
                    command.Parameters.AddWithValue("@end", dateTimePicker2.Value.Date);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }*/
        }
    }
}
