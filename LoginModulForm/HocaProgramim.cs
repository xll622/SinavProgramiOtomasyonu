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
    public partial class HocaProgramim : Form
    {
        int gelenBolumID;

        string connectionString =
            @"Data Source=localhost; Initial Catalog=SinavProgrami; Integrated Security=true";

        public HocaProgramim(int bolumID)
        {
            InitializeComponent();
            gelenBolumID = bolumID;
        }

        private void HocaProgramim_Load(object sender, EventArgs e)
        {
            TakvimleriYukle();
            ProgramVersiyonlariniYukle();
        }

        private void TakvimleriYukle()
        {
            DataBaseClass db = new DataBaseClass(connectionString);

            string query = @"
                SELECT 
                    TakvimID,
                    DonemAdi + ' - ' + SinavTipi AS TakvimBilgisi
                FROM AkademikTakvim
                ORDER BY TakvimID DESC";

            DataTable dt = db.ExecuteQuery(query);

            cmb_takvim.DataSource = dt;
            cmb_takvim.DisplayMember = "TakvimBilgisi";
            cmb_takvim.ValueMember = "TakvimID";
            cmb_takvim.SelectedIndex = -1;
        }

        private void ProgramVersiyonlariniYukle()
        {
            cmb_programVersiyon.Items.Clear();
            cmb_programVersiyon.Items.Add("1");
            cmb_programVersiyon.Items.Add("2");
            cmb_programVersiyon.Items.Add("3");
            cmb_programVersiyon.SelectedIndex = -1;
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            if (cmb_takvim.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen dönem ve sınav tipi seçiniz.");
                return;
            }

            if (cmb_programVersiyon.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen program versiyonu seçiniz.");
                return;
            }

            int takvimID = Convert.ToInt32(cmb_takvim.SelectedValue);
            int programVersiyon = Convert.ToInt32(cmb_programVersiyon.Text);

            ProgramiListele(takvimID, programVersiyon);
        }

        private void ProgramiListele(int takvimID, int programVersiyon)
        {
            DataBaseClass db = new DataBaseClass(connectionString);

            string query = @"
                SELECT 
                    s.ProgramVersiyon AS [Program],
                    z.Tarih AS [Tarih],
                    z.BaslangicSaat AS [Başlangıç],
                    z.BitisSaat AS [Bitiş],
                    d.DersAdi AS [Ders],
                    ss.SeviyeNo AS [Sınıf],
                    dt.TipAd AS [Ders Tipi],
                    d.Kredi,
                    d.DersiAlanOgrenciSayisi AS [Öğrenci Sayısı],
                    dr.DerslikAd AS [Derslik],
                    dr.Kapasite
                FROM Sinav s
                INNER JOIN Ders d ON s.DersID = d.DersID
                INNER JOIN ZamanDilimi z ON s.ZamanID = z.ZamanID
                INNER JOIN Derslik dr ON s.DerslikID = dr.DerslikID
                INNER JOIN SinifSeviyesi ss ON d.SinifSeviyeID = ss.SinifSeviyeID
                INNER JOIN DersTipi dt ON d.DersTipiID = dt.DersTipiID
                WHERE d.BolumID = @BolumID
                AND z.TakvimID = @TakvimID
                AND s.ProgramVersiyon = @ProgramVersiyon
                ORDER BY z.Tarih, z.BaslangicSaat, ss.SeviyeNo";

            SqlParameter[] parameters =
            {
                new SqlParameter("@BolumID", gelenBolumID),
                new SqlParameter("@TakvimID", takvimID),
                new SqlParameter("@ProgramVersiyon", programVersiyon)
            };

            DataTable dt = db.ExecuteQuery(query, parameters);

            dataGridView_programim.DataSource = dt;
            dataGridView_programim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_programim.ReadOnly = true;
            dataGridView_programim.AllowUserToAddRows = false;
        }
    }
}
