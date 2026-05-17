using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModulForm
{
    public partial class DerslikYonetimi : Form
    {
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";

        int secilenDerslikID = 0;
        ToolTip bilgiToolTip = new ToolTip();

        public DerslikYonetimi()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += DerslikYonetimi_KeyDown;

            text_drsGuncelle.KeyDown += GuncelleAlan_KeyDown;
            text_kapsGuncelle.KeyDown += GuncelleAlan_KeyDown;

            text_drsGuncelle.Enter += GuncelleAlan_Enter;
            text_kapsGuncelle.Enter += GuncelleAlan_Enter;

            ToolTipleriAyarla();

            Listele();
        }

        private void ToolTipleriAyarla()
        {
            bilgiToolTip.IsBalloon = true;
            bilgiToolTip.ToolTipIcon = ToolTipIcon.Info;
            bilgiToolTip.ToolTipTitle = "Arama";
            bilgiToolTip.AutoPopDelay = 4000;
            bilgiToolTip.InitialDelay = 300;
            bilgiToolTip.ReshowDelay = 100;

            bilgiToolTip.SetToolTip(text_drsGuncelle, "Arama için F4 tuşuna basınız.");
            bilgiToolTip.SetToolTip(text_kapsGuncelle, "Arama için F4 tuşuna basınız.");
            bilgiToolTip.SetToolTip(btn_guncelle, "Önce F4 ile dersliği getir, sonra burada güncelle.");
        }

        private void GuncelleAlan_Enter(object sender, EventArgs e)
        {
            Control kontrol = sender as Control;

            if (kontrol != null)
            {
                bilgiToolTip.Show(
                    "Arama için F4 tuşuna basınız.",
                    kontrol,
                    kontrol.Width / 2,
                    kontrol.Height,
                    3000
                );
            }
        }

        private void DerslikYonetimi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DerslikAraFormunuAc();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GuncelleAlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DerslikAraFormunuAc();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void DerslikAraFormunuAc()
        {
            using (DerslikAraForm araForm = new DerslikAraForm())
            {
                if (araForm.ShowDialog() == DialogResult.OK)
                {
                    secilenDerslikID = araForm.SecilenDerslikID;

                    text_drsGuncelle.Text = araForm.SecilenDerslikAd;
                    text_kapsGuncelle.Text = araForm.SecilenKapasite.ToString();

                    tabControl1.SelectedTab = tabPage3;

                    MessageBox.Show("Derslik bilgisi ana forma getirildi. Bilgileri düzenleyip Güncelle butonuna basabilirsiniz.");
                }
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_drsEkle.Text))
            {
                MessageBox.Show("Derslik adı boş olamaz.");
                return;
            }

            int kapasite;

            if (!int.TryParse(text_drsKapaEkle.Text.Trim(), out kapasite))
            {
                MessageBox.Show("Kapasite sayısal olmalıdır.");
                return;
            }

            if (kapasite <= 0)
            {
                MessageBox.Show("Kapasite 0'dan büyük olmalıdır.");
                return;
            }

            string derslikAd = text_drsEkle.Text.Trim();

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string kontrolSorgu = @"
                        SELECT COUNT(*)
                        FROM Derslik
                        WHERE DerslikAd = @derslikAd";

                    using (SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, baglanti))
                    {
                        kontrolKomut.Parameters.AddWithValue("@derslikAd", derslikAd);

                        int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                        if (kayitSayisi > 0)
                        {
                            MessageBox.Show("Bu derslik zaten kayıtlı.");
                            return;
                        }
                    }

                    string sorgu = @"
                        INSERT INTO Derslik
                        (DerslikAd, Kapasite)
                        VALUES
                        (@derslikAd, @kapasite)";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@derslikAd", derslikAd);
                        komut.Parameters.AddWithValue("@kapasite", kapasite);

                        int sonuc = komut.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show("Derslik başarıyla eklendi.");

                            text_drsEkle.Clear();
                            text_drsKapaEkle.Clear();

                            Listele();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme sırasında hata oluştu: " + ex.Message);
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_dersSil.Text))
            {
                MessageBox.Show("Silmek için derslik adını giriniz.");
                return;
            }

            string derslikAd = text_dersSil.Text.Trim();

            DialogResult onay = MessageBox.Show(
                "Bu dersliği silmek istediğinize emin misiniz?",
                "Uyarı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (onay != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string sorgu = @"
                        DELETE FROM Derslik
                        WHERE DerslikAd = @derslikAd";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@derslikAd", derslikAd);

                        int sonuc = komut.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show("Derslik silindi.");

                            text_dersSil.Clear();
                            text_KapsSil.Clear();

                            Listele();
                        }
                        else
                        {
                            MessageBox.Show("Silinecek derslik bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme sırasında hata oluştu: " + ex.Message);
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (secilenDerslikID == 0)
            {
                DerslikAraFormunuAc();
                return;
            }

            if (string.IsNullOrWhiteSpace(text_drsGuncelle.Text))
            {
                MessageBox.Show("Derslik adı boş olamaz.");
                return;
            }

            int kapasite;

            if (!int.TryParse(text_kapsGuncelle.Text.Trim(), out kapasite))
            {
                MessageBox.Show("Kapasite sayısal olmalıdır.");
                return;
            }

            if (kapasite <= 0)
            {
                MessageBox.Show("Kapasite 0'dan büyük olmalıdır.");
                return;
            }

            string derslikAd = text_drsGuncelle.Text.Trim();

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string kontrolSorgu = @"
                        SELECT COUNT(*)
                        FROM Derslik
                        WHERE DerslikID <> @derslikID
                          AND DerslikAd = @derslikAd";

                    using (SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, baglanti))
                    {
                        kontrolKomut.Parameters.AddWithValue("@derslikID", secilenDerslikID);
                        kontrolKomut.Parameters.AddWithValue("@derslikAd", derslikAd);

                        int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                        if (kayitSayisi > 0)
                        {
                            MessageBox.Show("Bu derslik adı başka bir kayıtta kullanılıyor.");
                            return;
                        }
                    }

                    string sorgu = @"
                        UPDATE Derslik
                        SET DerslikAd = @derslikAd,
                            Kapasite = @kapasite
                        WHERE DerslikID = @derslikID";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@derslikAd", derslikAd);
                        komut.Parameters.AddWithValue("@kapasite", kapasite);
                        komut.Parameters.AddWithValue("@derslikID", secilenDerslikID);

                        int sonuc = komut.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show("Derslik başarıyla güncellendi.");

                            GuncelleAlanlariniTemizle();
                            Listele();
                        }
                        else
                        {
                            MessageBox.Show("Güncellenecek derslik bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message);
            }
        }

        private void GuncelleAlanlariniTemizle()
        {
            secilenDerslikID = 0;

            text_drsGuncelle.Clear();
            text_kapsGuncelle.Clear();
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string sorgu = @"
                        SELECT 
                            DerslikID,
                            DerslikAd,
                            Kapasite
                        FROM Derslik
                        ORDER BY DerslikID";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;

                    if (dataGridView1.Columns.Contains("DerslikID"))
                        dataGridView1.Columns["DerslikID"].HeaderText = "Derslik ID";

                    if (dataGridView1.Columns.Contains("DerslikAd"))
                        dataGridView1.Columns["DerslikAd"].HeaderText = "Derslik Adı";

                    if (dataGridView1.Columns.Contains("Kapasite"))
                        dataGridView1.Columns["Kapasite"].HeaderText = "Kapasite";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme sırasında hata oluştu: " + ex.Message);
            }
        }

        private void lbl_dekle_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }

        private void lbl_dsil_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }

        private void lbl_dgncl_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }

        private void lbl_dlstl_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }
    }

}