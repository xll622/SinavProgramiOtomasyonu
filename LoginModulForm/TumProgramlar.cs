using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModulForm
{
    public partial class TumProgramlar : Form
    {
        private readonly string connectionString =
            @"Data Source=192.168.1.197,1433;Initial Catalog=SinavProgrami;User ID=sas ;Password=123;TrustServerCertificate=True";

        public TumProgramlar()
        {
            InitializeComponent();

            this.Load += TumProgramlar_Load;

            btn_listele.Click += btn_listele_Click;
            btn_excelAktar.Click += btn_excelAktar_Click;
            btn_temizle.Click += btn_temizle_Click;
        }

        private void TumProgramlar_Load(object sender, EventArgs e)
        {
            FormAyarlariniYap();
            BolumleriYukle();
            TakvimleriYukle();
            ProgramVersiyonlariniYukle();
        }

        private void FormAyarlariniYap()
        {
            this.Text = "Tüm Programlar";

            cmb_bolum.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_takvim.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_programVersiyon.DropDownStyle = ComboBoxStyle.DropDownList;

            dataGridView_programlar.ReadOnly = true;
            dataGridView_programlar.AllowUserToAddRows = false;
            dataGridView_programlar.AllowUserToDeleteRows = false;
            dataGridView_programlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_programlar.MultiSelect = false;
            dataGridView_programlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_programlar.RowHeadersVisible = false;

            lbl_baslik.Text = "Tüm Programlar";
            lbl_durum.Text = "Durum: Hazır";
        }

        private void BolumleriYukle()
        {
            string query = @"
                SELECT 
                    BolumID,
                    BolumAd
                FROM dbo.Bolum
                ORDER BY BolumAd";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    cmb_bolum.DataSource = dt;
                    cmb_bolum.DisplayMember = "BolumAd";
                    cmb_bolum.ValueMember = "BolumID";
                    cmb_bolum.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bölümler yüklenirken hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TakvimleriYukle()
        {
            string query = @"
                SELECT 
                    TakvimID,
                    DonemAdi + ' - ' + SinavTipi AS TakvimBilgisi
                FROM dbo.AkademikTakvim
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
                    "Akademik takvim yüklenirken hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ProgramVersiyonlariniYukle()
        {
            cmb_programVersiyon.Items.Clear();

            cmb_programVersiyon.Items.Add("Program 1 - Dengeli Dağılım");
            cmb_programVersiyon.Items.Add("Program 2 - Erken Biten");
            cmb_programVersiyon.Items.Add("Program 3 - Geniş Yayılım");

            cmb_programVersiyon.SelectedIndex = -1;
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            if (cmb_bolum.SelectedIndex == -1 || cmb_bolum.SelectedValue == null)
            {
                MessageBox.Show(
                    "Lütfen bölüm seçiniz.",
                    "Eksik Seçim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cmb_takvim.SelectedIndex == -1 || cmb_takvim.SelectedValue == null)
            {
                MessageBox.Show(
                    "Lütfen dönem / sınav tipi seçiniz.",
                    "Eksik Seçim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cmb_programVersiyon.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Lütfen program versiyonu seçiniz.",
                    "Eksik Seçim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int bolumID = Convert.ToInt32(cmb_bolum.SelectedValue);
            int takvimID = Convert.ToInt32(cmb_takvim.SelectedValue);
            int programVersiyon = cmb_programVersiyon.SelectedIndex + 1;

            ProgramlariListele(bolumID, takvimID, programVersiyon);
        }

        private void ProgramlariListele(int bolumID, int takvimID, int programVersiyon)
        {
            string query = @"
                SELECT
                    s.ProgramVersiyon AS [Program],
                    b.BolumAd AS [Bölüm],
                    CONVERT(VARCHAR(10), z.Tarih, 104) AS [Tarih],
                    CONVERT(VARCHAR(5), z.BaslangicSaat, 108) AS [Başlangıç],
                    CONVERT(VARCHAR(5), z.BitisSaat, 108) AS [Bitiş],
                    d.DersAdi AS [Ders Adı],
                    ss.SeviyeNo AS [Sınıf],
                    dt.TipAd AS [Ders Tipi],
                    d.Kredi AS [Kredi],
                    d.SinavSuresi AS [Sınav Süresi],
                    d.DersiAlanOgrenciSayisi AS [Öğrenci Sayısı],
                    dr.DerslikAd AS [Derslik],
                    dr.Kapasite AS [Derslik Kapasitesi]
                FROM dbo.Sinav s
                INNER JOIN dbo.Ders d 
                    ON s.DersID = d.DersID
                INNER JOIN dbo.Bolum b
                    ON d.BolumID = b.BolumID
                INNER JOIN dbo.ZamanDilimi z 
                    ON s.ZamanID = z.ZamanID
                INNER JOIN dbo.Derslik dr 
                    ON s.DerslikID = dr.DerslikID
                INNER JOIN dbo.SinifSeviyesi ss 
                    ON d.SinifSeviyeID = ss.SinifSeviyeID
                INNER JOIN dbo.DersTipi dt 
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
                    cmd.Parameters.Add("@BolumID", SqlDbType.Int).Value = bolumID;
                    cmd.Parameters.Add("@TakvimID", SqlDbType.Int).Value = takvimID;
                    cmd.Parameters.Add("@ProgramVersiyon", SqlDbType.Int).Value = programVersiyon;

                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    dataGridView_programlar.DataSource = dt;

                    lbl_durum.Text =
                        "Durum: Program " + programVersiyon +
                        " listelendi. Kayıt sayısı: " + dt.Rows.Count;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            "Seçilen bilgilere ait program bulunamadı. Önce hoca/personel modülünden sınav programı oluşturulmalıdır.",
                            "Kayıt Yok",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Program listelenirken hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btn_excelAktar_Click(object sender, EventArgs e)
        {
            if (dataGridView_programlar.Rows.Count == 0 || dataGridView_programlar.DataSource == null)
            {
                MessageBox.Show(
                    "Excel'e aktarılacak program bulunamadı. Önce program listeleyiniz.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Excel Dosyası Kaydet";
            saveFileDialog.Filter = "CSV Dosyası (*.csv)|*.csv";
            saveFileDialog.FileName = DosyaAdiOlustur();

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                DataTable dt = dataGridView_programlar.DataSource as DataTable;

                if (dt == null)
                {
                    MessageBox.Show(
                        "Aktarılacak veri uygun formatta değil.",
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DataTableCsvOlarakKaydet(dt, saveFileDialog.FileName);

                MessageBox.Show(
                    "Program Excel uyumlu CSV dosyası olarak başarıyla aktarıldı.",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lbl_durum.Text = "Durum: Program Excel'e aktarıldı.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel'e aktarım sırasında hata oluştu:\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string DosyaAdiOlustur()
        {
            string bolum = "Bolum";
            string takvim = "Takvim";
            string program = "Program";

            if (cmb_bolum.SelectedIndex != -1)
                bolum = cmb_bolum.Text.Replace(" ", "_");

            if (cmb_takvim.SelectedIndex != -1)
                takvim = cmb_takvim.Text.Replace(" ", "_").Replace("-", "");

            if (cmb_programVersiyon.SelectedIndex != -1)
                program = "Program_" + (cmb_programVersiyon.SelectedIndex + 1);

            return bolum + "_" + takvim + "_" + program + ".csv";
        }

        private void DataTableCsvOlarakKaydet(DataTable dt, string dosyaYolu)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(Tirnakla(dt.Columns[i].ColumnName));

                if (i < dt.Columns.Count - 1)
                    sb.Append(";");
            }

            sb.AppendLine();

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string deger = "";

                    if (row[i] != null && row[i] != DBNull.Value)
                    {
                        deger = row[i].ToString();
                    }

                    sb.Append(Tirnakla(deger));

                    if (i < dt.Columns.Count - 1)
                        sb.Append(";");
                }

                sb.AppendLine();
            }

            File.WriteAllText(dosyaYolu, sb.ToString(), Encoding.UTF8);
        }

        private string Tirnakla(string metin)
        {
            if (metin == null)
                metin = "";

            metin = metin.Replace("\"", "\"\"");

            return "\"" + metin + "\"";
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            dataGridView_programlar.DataSource = null;

            cmb_bolum.SelectedIndex = -1;
            cmb_takvim.SelectedIndex = -1;
            cmb_programVersiyon.SelectedIndex = -1;

            lbl_durum.Text = "Durum: Ekran temizlendi.";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            YoneticiModul frm = new YoneticiModul();
            frm.Show();
            this.Hide();
        }
    }
}