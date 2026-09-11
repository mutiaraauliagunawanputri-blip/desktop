using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace apk_spmb2
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                Jurusan form = new Jurusan();
                form.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void chartStatistik_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                Jalur_Pendaftaran form = new Jalur_Pendaftaran();
                form.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            {
                Tahun_Ajaran form = new Tahun_Ajaran();
                form.Show();
            }
        }
    }
}
