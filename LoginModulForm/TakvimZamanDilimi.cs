using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModulForm
{
    public partial class TakvimZamanDilimi : Form
    {
        string connectionString = @"Data Source=192.168.1.197,1433;Initial Catalog=SinavProgrami;User ID=sas ;Password=123;TrustServerCertificate=True";

        int secilenZamanID = 0;

        ToolTip aramaToolTip = new ToolTip();

        public TakvimZamanDilimi()
        {
            InitializeComponent();

            btn_ekle.Click += btn_ekle_Click;
            btn_sil.Click += btn_sil_Click;
            btn_guncelle.Click += btn_guncelle_Click;

            cmb_guncelle.KeyDown += Guncelle_KeyDown;
            dtp_guncelle.KeyDown += Guncelle_KeyDown;
            mtxt_başGuncelle.KeyDown += Guncelle_KeyDown;
            mtxt_bitGuncelle.KeyDown += Guncelle_KeyDown;

            cmb_guncelle.Enter += GuncelleAlan_Enter;
            dtp_guncelle.Enter += GuncelleAlan_Enter;
            mtxt_başGuncelle.Enter += GuncelleAlan_Enter;
            mtxt_bitGuncelle.Enter += GuncelleAlan_Enter;

            ToolTipleriAyarla();

            DonemleriYukle();
            Listele();
        }

        private void ToolTipleriAyarla()
        {
            aramaToolTip.IsBalloon = true;
            aramaToolTip.ToolTipIcon = ToolTipIcon.Info;
            aramaToolTip.ToolTipTitle = "Arama";
            aramaToolTip.AutoPopDelay = 4000;
            aramaToolTip.InitialDelay = 300;
            aramaToolTip.ReshowDelay = 100;

            aramaToolTip.SetToolTip(cmb_guncelle, "Arama için F4 tuşuna basınız");
            aramaToolTip.SetToolTip(dtp_guncelle, "Arama için F4 tuşuna basınız");
            aramaToolTip.SetToolTip(mtxt_başGuncelle, "Arama için F4 tuşuna basınız");
            aramaToolTip.SetToolTip(mtxt_bitGuncelle, "Arama için F4 tuşuna basınız");
            aramaToolTip.SetToolTip(btn_guncelle, "Önce F4 ile kayıt arayın, sonra burada güncelleyin");
        }

        private void GuncelleAlan_Enter(object sender, EventArgs e)
        {
            Control kontrol = sender as Control;

            if (kontrol != null)
            {
                aramaToolTip.Show(
                    "Arama için F4 tuşuna basınız",
                    kontrol,
                    kontrol.Width / 2,
                    kontrol.Height,
                    3000
                );
            }
        }

        private void DonemleriYukle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string sorgu = @"
                SELECT 
                    TakvimID,
                    DonemAdi + ' - ' + SinavTipi AS DonemTipi
                FROM AkademikTakvim
                ORDER BY DonemAdi, SinavTipi";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("AkademikTakvim tablosunda kayıt yok. Önce dönem ve sınav tipi eklemelisiniz.");
                        return;
                    }

                    cmb_dnm_Ekle.DataSource = dt.Copy();
                    cmb_dnm_Ekle.DisplayMember = "DonemTipi";
                    cmb_dnm_Ekle.ValueMember = "TakvimID";
                    cmb_dnm_Ekle.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmb_dnm_Ekle.SelectedIndex = -1;

                    cmb_sil.DataSource = dt.Copy();
                    cmb_sil.DisplayMember = "DonemTipi";
                    cmb_sil.ValueMember = "TakvimID";
                    cmb_sil.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmb_sil.SelectedIndex = -1;

                    cmb_guncelle.DataSource = dt.Copy();
                    cmb_guncelle.DisplayMember = "DonemTipi";
                    cmb_guncelle.ValueMember = "TakvimID";
                    cmb_guncelle.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmb_guncelle.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dönem bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private int SecilenTakvimIDGetir(ComboBox comboBox)
        {
            if (comboBox.SelectedValue == null)
                return 0;

            int takvimID;

            if (int.TryParse(comboBox.SelectedValue.ToString(), out takvimID))
                return takvimID;

            return 0;
        }

        private bool SaatKontrol(string saat, out TimeSpan sonuc)
        {
            sonuc = TimeSpan.Zero;

            return TimeSpan.TryParseExact(
                saat,
                @"hh\:mm",
                CultureInfo.InvariantCulture,
                out sonuc);
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            int takvimID = SecilenTakvimIDGetir(cmb_dnm_Ekle);

            if (takvimID == 0)
            {
                MessageBox.Show("Dönem tipi seçiniz.");
                return;
            }

            if (!mtx_BaşEkle.MaskCompleted || !mtx_bitEkle.MaskCompleted)
            {
                MessageBox.Show("Başlangıç ve bitiş saatini giriniz.");
                return;
            }

            TimeSpan baslangicSaat;
            TimeSpan bitisSaat;

            if (!SaatKontrol(mtx_BaşEkle.Text, out baslangicSaat))
            {
                MessageBox.Show("Başlangıç saati geçerli değil. Örnek: 09:00");
                return;
            }

            if (!SaatKontrol(mtx_bitEkle.Text, out bitisSaat))
            {
                MessageBox.Show("Bitiş saati geçerli değil. Örnek: 10:00");
                return;
            }

            if (baslangicSaat >= bitisSaat)
            {
                MessageBox.Show("Başlangıç saati, bitiş saatinden küçük olmalıdır.");
                return;
            }

            DateTime tarih = dtpEkle.Value.Date;

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string kontrolSorgu = @"
                        SELECT COUNT(*)
                        FROM ZamanDilimi
                        WHERE TakvimID = @takvimID
                          AND Tarih = @tarih
                          AND BaslangicSaat = @baslangicSaat
                          AND BitisSaat = @bitisSaat";

                    using (SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, baglanti))
                    {
                        kontrolKomut.Parameters.AddWithValue("@takvimID", takvimID);
                        kontrolKomut.Parameters.AddWithValue("@tarih", tarih);
                        kontrolKomut.Parameters.AddWithValue("@baslangicSaat", baslangicSaat);
                        kontrolKomut.Parameters.AddWithValue("@bitisSaat", bitisSaat);

                        int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                        if (kayitSayisi > 0)
                        {
                            MessageBox.Show("Bu zaman dilimi zaten kayıtlı.");
                            return;
                        }
                    }

                    string ekleSorgu = @"
                        INSERT INTO ZamanDilimi
                        (TakvimID, Tarih, BaslangicSaat, BitisSaat)
                        VALUES
                        (@takvimID, @tarih, @baslangicSaat, @bitisSaat)";

                    using (SqlCommand ekleKomut = new SqlCommand(ekleSorgu, baglanti))
                    {
                        ekleKomut.Parameters.AddWithValue("@takvimID", takvimID);
                        ekleKomut.Parameters.AddWithValue("@tarih", tarih);
                        ekleKomut.Parameters.AddWithValue("@baslangicSaat", baslangicSaat);
                        ekleKomut.Parameters.AddWithValue("@bitisSaat", bitisSaat);

                        int sonuc = ekleKomut.ExecuteNonQuery();

                        if (sonuc > 0)
                        {

                            cmb_dnm_Ekle.SelectedIndex = -1;
                            dtpEkle.Value = DateTime.Now;
                            mtx_BaşEkle.Clear();
                            mtx_bitEkle.Clear();

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
            int takvimID = SecilenTakvimIDGetir(cmb_sil);

            if (takvimID == 0)
            {
                MessageBox.Show("Silmek için dönem tipi seçiniz.");
                return;
            }

            if (!mtx_başSil.MaskCompleted || !mtx_bitSil.MaskCompleted)
            {
                MessageBox.Show("Silmek için başlangıç ve bitiş saatini giriniz.");
                return;
            }

            TimeSpan baslangicSaat;
            TimeSpan bitisSaat;

            if (!SaatKontrol(mtx_başSil.Text, out baslangicSaat))
            {
                MessageBox.Show("Başlangıç saati geçerli değil. Örnek: 09:00");
                return;
            }

            if (!SaatKontrol(mtx_bitSil.Text, out bitisSaat))
            {
                MessageBox.Show("Bitiş saati geçerli değil. Örnek: 10:00");
                return;
            }

            DateTime tarih = dtp_sil.Value.Date;

            DialogResult onay = MessageBox.Show(
                "Bu zaman dilimini silmek istediğinize emin misiniz?",
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
                        DELETE FROM ZamanDilimi
                        WHERE TakvimID = @takvimID
                          AND Tarih = @tarih
                          AND BaslangicSaat = @baslangicSaat
                          AND BitisSaat = @bitisSaat";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@takvimID", takvimID);
                        komut.Parameters.AddWithValue("@tarih", tarih);
                        komut.Parameters.AddWithValue("@baslangicSaat", baslangicSaat);
                        komut.Parameters.AddWithValue("@bitisSaat", bitisSaat);

                        int sonuc = komut.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show("Zaman dilimi silindi.");

                            cmb_sil.SelectedIndex = -1;
                            dtp_sil.Value = DateTime.Now;
                            mtx_başSil.Clear();
                            mtx_bitSil.Clear();

                            Listele();
                        }
                        else
                        {
                            MessageBox.Show("Silinecek kayıt bulunamadı.");
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
            if (secilenZamanID == 0)
            {
                GuncellenecekKaydiAra();
                return;
            }

            int takvimID = SecilenTakvimIDGetir(cmb_guncelle);

            if (takvimID == 0)
            {
                MessageBox.Show("Dönem tipi seçiniz.");
                return;
            }

            if (!mtxt_başGuncelle.MaskCompleted || !mtxt_bitGuncelle.MaskCompleted)
            {
                MessageBox.Show("Başlangıç ve bitiş saatini giriniz.");
                return;
            }

            TimeSpan baslangicSaat;
            TimeSpan bitisSaat;

            if (!SaatKontrol(mtxt_başGuncelle.Text, out baslangicSaat))
            {
                MessageBox.Show("Başlangıç saati geçerli değil. Örnek: 09:00");
                return;
            }

            if (!SaatKontrol(mtxt_bitGuncelle.Text, out bitisSaat))
            {
                MessageBox.Show("Bitiş saati geçerli değil. Örnek: 10:00");
                return;
            }

            if (baslangicSaat >= bitisSaat)
            {
                MessageBox.Show("Başlangıç saati, bitiş saatinden küçük olmalıdır.");
                return;
            }

            DateTime tarih = dtp_guncelle.Value.Date;

            try
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();

                    string kontrolSorgu = @"
                        SELECT COUNT(*)
                        FROM ZamanDilimi
                        WHERE ZamanID <> @zamanID
                          AND TakvimID = @takvimID
                          AND Tarih = @tarih
                          AND BaslangicSaat = @baslangicSaat
                          AND BitisSaat = @bitisSaat";

                    using (SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, baglanti))
                    {
                        kontrolKomut.Parameters.AddWithValue("@zamanID", secilenZamanID);
                        kontrolKomut.Parameters.AddWithValue("@takvimID", takvimID);
                        kontrolKomut.Parameters.AddWithValue("@tarih", tarih);
                        kontrolKomut.Parameters.AddWithValue("@baslangicSaat", baslangicSaat);
                        kontrolKomut.Parameters.AddWithValue("@bitisSaat", bitisSaat);

                        int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                        if (kayitSayisi > 0)
                        {
                            MessageBox.Show("Bu zaman dilimi zaten kayıtlı.");
                            return;
                        }
                    }

                    string sorgu = @"
                        UPDATE ZamanDilimi
                        SET TakvimID = @takvimID,
                            Tarih = @tarih,
                            BaslangicSaat = @baslangicSaat,
                            BitisSaat = @bitisSaat
                        WHERE ZamanID = @zamanID";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@takvimID", takvimID);
                        komut.Parameters.AddWithValue("@tarih", tarih);
                        komut.Parameters.AddWithValue("@baslangicSaat", baslangicSaat);
                        komut.Parameters.AddWithValue("@bitisSaat", bitisSaat);
                        komut.Parameters.AddWithValue("@zamanID", secilenZamanID);

                        int sonuc = komut.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show("Zaman dilimi başarıyla güncellendi.");

                            GuncelleAlanlariniTemizle();
                            Listele();
                        }
                        else
                        {
                            MessageBox.Show("Güncellenecek kayıt bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message);
            }
        }

        private void GuncellenecekKaydiAra()
        {
            using (TakvimZamanDilimiAraForm araForm = new TakvimZamanDilimiAraForm())
            {
                if (araForm.ShowDialog() == DialogResult.OK)
                {
                    secilenZamanID = araForm.SecilenZamanID;

                    cmb_guncelle.SelectedValue = araForm.SecilenTakvimID;
                    dtp_guncelle.Value = araForm.SecilenTarih;
                    mtxt_başGuncelle.Text = araForm.SecilenBaslangicSaat.ToString(@"hh\:mm");
                    mtxt_bitGuncelle.Text = araForm.SecilenBitisSaat.ToString(@"hh\:mm");

                    tabControl1.SelectedTab = tabPage3;

                }
            }
        }

        private void Guncelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                GuncellenecekKaydiAra();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void GuncelleAlanlariniTemizle()
        {
            secilenZamanID = 0;

            cmb_guncelle.SelectedIndex = -1;
            dtp_guncelle.Value = DateTime.Now;
            mtxt_başGuncelle.Clear();
            mtxt_bitGuncelle.Clear();
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
                            z.ZamanID,
                            z.TakvimID,
                            a.DonemAdi,
                            a.SinavTipi,
                            z.Tarih,
                            z.BaslangicSaat,
                            z.BitisSaat
                        FROM ZamanDilimi z
                        INNER JOIN AkademikTakvim a 
                            ON z.TakvimID = a.TakvimID
                        ORDER BY z.Tarih, z.BaslangicSaat";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    dataGridView2.DataSource = dt;

                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView2.ReadOnly = true;
                    dataGridView2.AllowUserToAddRows = false;

                    if (dataGridView2.Columns.Contains("ZamanID"))
                        dataGridView2.Columns["ZamanID"].HeaderText = "Zaman ID";

                    if (dataGridView2.Columns.Contains("TakvimID"))
                        dataGridView2.Columns["TakvimID"].HeaderText = "Takvim ID";

                    if (dataGridView2.Columns.Contains("DonemAdi"))
                        dataGridView2.Columns["DonemAdi"].HeaderText = "Dönem";

                    if (dataGridView2.Columns.Contains("SinavTipi"))
                        dataGridView2.Columns["SinavTipi"].HeaderText = "Sınav Tipi";

                    if (dataGridView2.Columns.Contains("Tarih"))
                    {
                        dataGridView2.Columns["Tarih"].HeaderText = "Tarih";
                        dataGridView2.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    }

                    if (dataGridView2.Columns.Contains("BaslangicSaat"))
                        dataGridView2.Columns["BaslangicSaat"].HeaderText = "Başlangıç Saati";

                    if (dataGridView2.Columns.Contains("BitisSaat"))
                        dataGridView2.Columns["BitisSaat"].HeaderText = "Bitiş Saati";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme sırasında hata oluştu: " + ex.Message);
            }
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void lbl_ekle_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }

        private void lbl_sil_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }

        private void lbl_guncelle_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }

        private void lbl_listele_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }
    }
}
