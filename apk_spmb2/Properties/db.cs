using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace apk_spmb2
{
    public class db
    {
        // =========================
        // KONEKSI DATABASE
        // =========================
        public static MySqlConnection koneksi =
            new MySqlConnection(
                "Server=127.0.0.1;User ID=root;Password=;Database=db_spmb;"
            );

        // =========================
        // DATASET
        // =========================
        public static DataSet ds = new DataSet();

        public static MySqlDataAdapter da;

        public static MySqlCommand perintah;


        // =========================
        // MEMBUKA KONEKSI
        // =========================
        public static void konek()
        {
            if (koneksi.State == ConnectionState.Closed)
            {
                koneksi.Open();
            }
        }


        // =========================
        // MENUTUP KONEKSI
        // =========================
        public static void tutup()
        {
            if (koneksi.State == ConnectionState.Open)
            {
                koneksi.Close();
            }
        }


        // =========================
        // MENAMPILKAN DATA
        // =========================
        public static DataTable tampilData(string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                konek();

                using (MySqlDataAdapter adapter =
                    new MySqlDataAdapter(sql, koneksi))
                {
                    adapter.Fill(dt);
                }
            }
            finally
            {
                tutup();
            }

            return dt;
        }


        // =========================
        // CRUD
        // =========================
        public static void crud(string sqlnya)
        {
            Console.WriteLine(sqlnya);

            try
            {
                konek();

                ds.Tables.Clear();

                perintah =
                    new MySqlCommand(
                        sqlnya,
                        koneksi
                    );

                da =
                    new MySqlDataAdapter(
                        perintah
                    );

                da.Fill(ds);
            }
            finally
            {
                tutup();
            }
        }
    }
}