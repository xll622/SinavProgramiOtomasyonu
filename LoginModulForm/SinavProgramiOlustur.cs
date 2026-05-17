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
    public partial class SinavProgramiOlustur : Form
    {
        private readonly int gelenBolumID;

        private readonly string connectionString =
            @"Data Source=192.168.1.197,1433;Initial Catalog=SinavProgrami;User ID=sas ;Password=123;TrustServerCertificate=True";

        public SinavProgramiOlustur(int bolumID)
        {
            InitializeComponent();

            gelenBolumID = bolumID;

            this.Load += SinavProgramiOlustur_Load;

            btn_programOlustur.Click += btn_programOlustur_Click;
            btn_listele.Click += btn_listele_Click;
            btn_temizle.Click += btn_temizle_Click;

            cmb_programVersiyon.SelectedIndexChanged += cmb_programVersiyon_SelectedIndexChanged;
        }

        private void SinavProgramiOlustur_Load(object sender, EventArgs e)
        {
            FormAyarlariniYap();
            TakvimleriYukle();
            ProgramVersiyonlariniYukle();
        }

        private void FormAyarlariniYap()
        {
            this.Text = "Sınav Programı Oluştur";
            this.StartPosition = FormStartPosition.CenterScreen;

            cmb_takvim.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_programVersiyon.DropDownStyle = ComboBoxStyle.DropDownList;

            dataGridView_program.ReadOnly = true;
            dataGridView_program.AllowUserToAddRows = false;
            dataGridView_program.AllowUserToDeleteRows = false;
            dataGridView_program.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_program.MultiSelect = false;
            dataGridView_program.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_program.RowHeadersVisible = false;

            lbl_baslik.Text = "Sınav Programı Oluştur";
            lbl_aciklama.Text = "Bu ekranda seçilen dönem için 3 farklı sınav programı oluşturulur.";
            lbl_durum.Text = "Durum: Hazır";
        }

        private void TakvimleriYukle()
        {
            string query = @"
                SELECT 
                    TakvimID,
                    DonemAdi + ' - ' + SinavTipi AS TakvimBilgisi
                FROM AkademikTakvim
                ORDER BY TakvimID DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    cmb_takvim.DataSource = dt;
                    cmb_takvim.DisplayMember = "TakvimBilgisi";
                    cmb_takvim.ValueMember = "TakvimID";
                    cmb_takvim.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Akademik takvim bilgileri yüklenirken hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ProgramVersiyonlariniYukle()
        {
            cmb_programVersiyon.Items.Clear();

            cmb_programVersiyon.Items.Add("Program 1 - Dengeli Dağılım");
            cmb_programVersiyon.Items.Add("Program 2 - Erken Biten");
            cmb_programVersiyon.Items.Add("Program 3 - Geniş Yayılım");

            cmb_programVersiyon.SelectedIndex = 0;
        }

        private void cmb_programVersiyon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_programVersiyon.SelectedIndex == 0)
            {
                lbl_durum.Text = "Seçilen strateji: Dengeli dağılım. Amaç sınav yükünü günlere dengeli yaymaktır.";
            }
            else if (cmb_programVersiyon.SelectedIndex == 1)
            {
                lbl_durum.Text = "Seçilen strateji: Erken biten program. Amaç sınavları mümkün olduğunca erken tamamlamaktır.";
            }
            else if (cmb_programVersiyon.SelectedIndex == 2)
            {
                lbl_durum.Text = "Seçilen strateji: Geniş yayılım. Amaç sınavları takvim aralığına daha rahat dağıtmaktır.";
            }
        }

        private void btn_programOlustur_Click(object sender, EventArgs e)
        {
            if (gelenBolumID <= 0)
            {
                MessageBox.Show(
                    "Bölüm bilgisi alınamadı. Hoca/personel kullanıcısının BolumID değeri kontrol edilmelidir.",
                    "Eksik Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (cmb_takvim.SelectedIndex == -1 || cmb_takvim.SelectedValue == null)
            {
                MessageBox.Show(
                    "Lütfen dönem / sınav tipi seçiniz.",
                    "Eksik Seçim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            int takvimID = Convert.ToInt32(cmb_takvim.SelectedValue);
            bool cumartesiKullanilsin = chk_cumartesiKullan.Checked;

            DialogResult onay = MessageBox.Show(
                "Seçilen dönem için bu bölüme ait eski programlar silinip 3 yeni sınav programı oluşturulacaktır.\n\n" +
                "Cumartesi sınav yapılabilir mi?: " + (cumartesiKullanilsin ? "Evet" : "Hayır") + "\n\n" +
                "Devam edilsin mi?",
                "Program Oluşturma Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (onay != DialogResult.Yes)
                return;

            try
            {
                lbl_durum.Text = "Durum: Programlar oluşturuluyor...";
                Application.DoEvents();

                SinavProgramiMotoru motor = new SinavProgramiMotoru(connectionString, gelenBolumID);

                SinavProgramiMotoru.MotorSonuc sonuc =
                    motor.ProgramlariOlustur(takvimID, cumartesiKullanilsin);

                if (!sonuc.Basarili)
                {
                    lbl_durum.Text = "Durum: Program oluşturulamadı.";

                    MessageBox.Show(
                        sonuc.Mesaj,
                        "Program Oluşturma Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                lbl_durum.Text = "Durum: " + sonuc.Mesaj;

                MessageBox.Show(
                    sonuc.Mesaj,
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                cmb_programVersiyon.SelectedIndex = 0;

                ProgramiListele(takvimID, 1);
            }
            catch (Exception ex)
            {
                lbl_durum.Text = "Durum: Hata oluştu.";

                MessageBox.Show(
                    "Program oluşturulurken beklenmeyen hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            if (cmb_takvim.SelectedIndex == -1 || cmb_takvim.SelectedValue == null)
            {
                MessageBox.Show(
                    "Lütfen dönem / sınav tipi seçiniz.",
                    "Eksik Seçim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (cmb_programVersiyon.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Lütfen program versiyonu seçiniz.",
                    "Eksik Seçim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            int takvimID = Convert.ToInt32(cmb_takvim.SelectedValue);
            int programVersiyon = cmb_programVersiyon.SelectedIndex + 1;

            ProgramiListele(takvimID, programVersiyon);
        }

        private void ProgramiListele(int takvimID, int programVersiyon)
        {
            string query = @"
                SELECT
                    s.ProgramVersiyon AS [Program],
                    z.Tarih AS [Tarih],
                    z.BaslangicSaat AS [Başlangıç],
                    z.BitisSaat AS [Bitiş],
                    d.DersAdi AS [Ders Adı],
                    ss.SeviyeNo AS [Sınıf],
                    dt.TipAd AS [Ders Tipi],
                    d.Kredi AS [Kredi],
                    d.SinavSuresi AS [Sınav Süresi],
                    d.DersiAlanOgrenciSayisi AS [Öğrenci Sayısı],
                    dr.DerslikAd AS [Derslik],
                    dr.Kapasite AS [Derslik Kapasitesi]
                FROM Sinav s
                INNER JOIN Ders d 
                    ON s.DersID = d.DersID
                INNER JOIN ZamanDilimi z 
                    ON s.ZamanID = z.ZamanID
                INNER JOIN Derslik dr 
                    ON s.DerslikID = dr.DerslikID
                INNER JOIN SinifSeviyesi ss 
                    ON d.SinifSeviyeID = ss.SinifSeviyeID
                INNER JOIN DersTipi dt 
                    ON d.DersTipiID = dt.DersTipiID
                WHERE d.BolumID = @BolumID
                  AND z.TakvimID = @TakvimID
                  AND s.ProgramVersiyon = @ProgramVersiyon
                ORDER BY z.Tarih, z.BaslangicSaat, ss.SeviyeNo, d.DersAdi";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@BolumID", SqlDbType.Int).Value = gelenBolumID;
                    cmd.Parameters.Add("@TakvimID", SqlDbType.Int).Value = takvimID;
                    cmd.Parameters.Add("@ProgramVersiyon", SqlDbType.Int).Value = programVersiyon;

                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    dataGridView_program.DataSource = dt;

                    if (dataGridView_program.Columns.Contains("Tarih"))
                    {
                        dataGridView_program.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    }

                    lbl_durum.Text = "Durum: Program " + programVersiyon + " listelendi. Kayıt sayısı: " + dt.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Program listelenirken hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            dataGridView_program.DataSource = null;
            cmb_programVersiyon.SelectedIndex = -1;
            lbl_durum.Text = "Durum: Ekran temizlendi.";
        }
    }
}
