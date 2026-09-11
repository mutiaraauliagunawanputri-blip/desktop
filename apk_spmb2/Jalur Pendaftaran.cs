using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace apk_spmb2
{
    public partial class Jalur_Pendaftaran : Form
    {
        public Jalur_Pendaftaran()
        {
            InitializeComponent();
        }


        // =========================
        // FORM LOAD
        // =========================
        private void JalurPendaftaran_Load(object sender, EventArgs e)
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
                        "SELECT * FROM jalur_pendaftaran"
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
            txtidjalur.Clear();
            txtnamajalur.Clear();
            txtketerangan.Clear();

            cmbstatus.SelectedIndex = -1;

            txtnamajalur.Focus();
        }


        // =========================
        // SIMPAN DATA
        // =========================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtnamajalur.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Nama jalur wajib diisi!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtnamajalur.Focus();
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
                    INSERT INTO jalur_pendaftaran
                    (nama_jalur, keterangan, status)
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
                        txtnamajalur.Text.Trim()
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
                    "Data berhasil disimpan!",
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


                if (row.Cells["id_jalur"].Value != null)
                {
                    txtidjalur.Text =
                        row.Cells["id_jalur"]
                        .Value
                        .ToString();
                }


                if (row.Cells["nama_jalur"].Value != null)
                {
                    txtnamajalur.Text =
                        row.Cells["nama_jalur"]
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
            if (txtidjalur.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Pilih data yang ingin diedit!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }


            if (txtnamajalur.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Nama jalur wajib diisi!",
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
                    UPDATE jalur_pendaftaran
                    SET
                        nama_jalur = @nama,
                        keterangan = @keterangan,
                        status = @status
                    WHERE id_jalur = @id
                ";


                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@id",
                        txtidjalur.Text.Trim()
                    );

                    cmd.Parameters.AddWithValue(
                        "@nama",
                        txtnamajalur.Text.Trim()
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
                    "Data berhasil diedit!",
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
            if (txtidjalur.Text.Trim() == "")
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
                    "Yakin ingin menghapus data ini?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );


            if (hasil == DialogResult.No)
                return;


            try
            {
                db.konek();

                string sql = @"
                    DELETE FROM jalur_pendaftaran
                    WHERE id_jalur = @id
                ";


                using (MySqlCommand cmd =
                    new MySqlCommand(
                        sql,
                        db.koneksi
                    ))
                {
                    cmd.Parameters.AddWithValue(
                        "@id",
                        txtidjalur.Text.Trim()
                    );

                    cmd.ExecuteNonQuery();
                }


                MessageBox.Show(
                    "Data berhasil dihapus!",
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