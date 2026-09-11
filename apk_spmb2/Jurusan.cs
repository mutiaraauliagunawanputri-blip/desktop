using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace apk_spmb2
{
    public partial class Jurusan : Form
    {
        public Jurusan()
        {
            InitializeComponent();
        }

        // =========================
        // FORM LOAD
        // =========================
        private void Jurusan_Load(object sender, EventArgs e)
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
                        "SELECT * FROM jurusan"
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
            txtidjurusan.Clear();
            txtnamajurusan.Clear();
            txtketerangan.Clear();

            cmbstatus.SelectedIndex = -1;

            txtnamajurusan.Focus();
        }

        // =========================
        // SIMPAN
        // =========================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtnamajurusan.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Nama jurusan wajib diisi!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtnamajurusan.Focus();
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
                    INSERT INTO jurusan
                    (nama_jurusan, keterangan, status)
                    VALUES
                    (@nama, @keterangan, @status)
                ";

                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@nama",
                        txtnamajurusan.Text.Trim()
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
                    "Data jurusan berhasil disimpan!",
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
                    "Gagal menyimpan data!\n\n" +
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

                if (row.Cells["id_jurusan"].Value != null)
                {
                    txtidjurusan.Text =
                        row.Cells["id_jurusan"]
                        .Value
                        .ToString();
                }

                if (row.Cells["nama_jurusan"].Value != null)
                {
                    txtnamajurusan.Text =
                        row.Cells["nama_jurusan"]
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
        // EDIT
        // =========================
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtidjurusan.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Pilih data yang ingin diedit!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (txtnamajurusan.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Nama jurusan wajib diisi!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

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

                return;
            }

            try
            {
                db.konek();

                string sql = @"
                    UPDATE jurusan
                    SET
                        nama_jurusan = @nama,
                        keterangan = @keterangan,
                        status = @status
                    WHERE id_jurusan = @id
                ";

                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@id",
                        txtidjurusan.Text.Trim()
                    );

                    cmd.Parameters.AddWithValue(
                        "@nama",
                        txtnamajurusan.Text.Trim()
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
                    "Data jurusan berhasil diedit!",
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
        // HAPUS
        // =========================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtidjurusan.Text.Trim() == "")
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
                    "Yakin ingin menghapus data jurusan ini?",
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
                    DELETE FROM jurusan
                    WHERE id_jurusan = @id
                ";

                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@id",
                        txtidjurusan.Text.Trim()
                    );

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Data jurusan berhasil dihapus!",
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