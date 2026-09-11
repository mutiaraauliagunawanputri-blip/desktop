using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace apk_spmb2
{
    public partial class Tahun_Ajaran : Form
    {
        public Tahun_Ajaran()
        {
            InitializeComponent();
        }

        // =========================
        // FORM LOAD
        // =========================
        private void Tahun_Ajaran_Load(object sender, EventArgs e)
        {
            cmbstatus.Items.Clear();

            cmbstatus.Items.Add("Aktif");
            cmbstatus.Items.Add("Tidak Aktif");

            tampilData();
            kosongkan();
        }

        // =========================
        // MENAMPILKAN DATA
        // =========================
        private void tampilData()
        {
            try
            {
                dataGridView1.DataSource =
                    db.tampilData(
                        "SELECT * FROM tahun_ajaran"
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal menampilkan data!\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // KOSONGKAN FORM
        // =========================
        private void kosongkan()
        {
            txtidtahun.Clear();
            txttahunajaran.Clear();
            txtketerangan.Clear();

            cmbstatus.SelectedIndex = -1;

            txttahunajaran.Focus();
        }

        // =========================
        // KLIK DATA GRIDVIEW
        // =========================
        private void dataGridView1_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row =
                    dataGridView1.Rows[e.RowIndex];

                if (row.Cells["id_tahun"].Value != null)
                {
                    txtidtahun.Text =
                        row.Cells["id_tahun"]
                        .Value
                        .ToString();
                }

                if (row.Cells["tahun_ajaran"].Value != null)
                {
                    txttahunajaran.Text =
                        row.Cells["tahun_ajaran"]
                        .Value
                        .ToString();
                }

                if (row.Cells["keterangan"].Value != null)
                {
                    txtketerangan.Text =
                        row.Cells["keterangan"]
                        .Value
                        .ToString();
                }

                if (row.Cells["status"].Value != null)
                {
                    cmbstatus.Text =
                        row.Cells["status"]
                        .Value
                        .ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal mengambil data!\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // EDIT DATA
        // =========================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtidtahun.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Pilih data yang ingin diedit!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (txttahunajaran.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Tahun ajaran wajib diisi!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txttahunajaran.Focus();
                return;
            }

            if (cmbstatus.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Status wajib dipilih!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cmbstatus.Focus();
                return;
            }

            try
            {
                db.konek();

                string sql = @"
                    UPDATE tahun_ajaran
                    SET
                        tahun_ajaran = @tahun,
                        keterangan = @keterangan,
                        status = @status
                    WHERE id_tahun = @id
                ";

                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@id",
                        txtidtahun.Text.Trim()
                    );

                    cmd.Parameters.AddWithValue(
                        "@tahun",
                        txttahunajaran.Text.Trim()
                    );

                    cmd.Parameters.AddWithValue(
                        "@keterangan",
                        txtketerangan.Text.Trim()
                    );

                    cmd.Parameters.AddWithValue(
                        "@status",
                        cmbstatus.Text.Trim()
                    );

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Data tahun ajaran berhasil diedit!",
                    "Berhasil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                tampilData();
                kosongkan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal mengedit data!\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.tutup();
            }
        }

        // =========================
        // HAPUS DATA
        // =========================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtidtahun.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Pilih data yang ingin dihapus!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult hasil =
                MessageBox.Show(
                    "Yakin ingin menghapus data tahun ajaran ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (hasil == DialogResult.No)
                return;

            try
            {
                db.konek();

                string sql = @"
                    DELETE FROM tahun_ajaran
                    WHERE id_tahun = @id
                ";

                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@id",
                        txtidtahun.Text.Trim()
                    );

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Data tahun ajaran berhasil dihapus!",
                    "Berhasil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                tampilData();
                kosongkan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal menghapus data!\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                db.tutup();
            }
        }
    }
}