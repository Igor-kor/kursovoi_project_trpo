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
    public partial class EditAddres : Form
    {
        int id = -1;
        public EditAddres(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // делаем запрос на сохранения данных с текущим id
            MessageBox.Show("Изменения сохранены!");
            this.Close();
        }

        private void EditAddres_Load(object sender, EventArgs e)
        {
            textBox3.Text = id.ToString();
            // делаем запрос и выводим все даннык
        }
    }
}
