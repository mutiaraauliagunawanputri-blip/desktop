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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM user WHERE username=@username AND password=@password";

            db.perintah = new MySqlCommand(query, db.koneksi);

            db.perintah.Parameters.AddWithValue("@username", txtuser.Text);
            db.perintah.Parameters.AddWithValue("@password", txtpass.Text);

            db.koneksi.Open();

            MySqlDataReader reader = db.perintah.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Login berhasil!");

                dashboard dashboard = new dashboard();
                dashboard.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau password salah!");
            }

            reader.Close();
            db.koneksi.Close();
        }
    }
   }


