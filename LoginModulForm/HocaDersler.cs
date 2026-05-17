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
    public partial class HocaDersler: Form
    {
        int gelenBolumID;

        string connectionString =
            @"Data Source=localhost; Initial Catalog=SinavProgrami; Integrated Security=true";

        public HocaDersler(int bolumID)
        {
            InitializeComponent();
            gelenBolumID = bolumID;
        }

        private void HocaDersler_Load(object sender, EventArgs e)
        {
            DersleriListele();
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            DersleriListele();
        }

        private void btn_kapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DersleriListele()
        {
            DataBaseClass db = new DataBaseClass(connectionString);

            string query = @"
                SELECT 
                    d.DersID,
                    d.DersAdi AS [Ders Adı],
                    b.BolumAd AS [Bölüm],
                    dt.TipAd AS [Ders Tipi],
                    ss.SeviyeNo AS [Sınıf Seviyesi],
                    d.Kredi AS [Kredi],
                    d.SinavSuresi AS [Sınav Süresi],
                    d.DersiAlanOgrenciSayisi AS [Öğrenci Sayısı]
                FROM Ders d
                INNER JOIN Bolum b ON d.BolumID = b.BolumID
                INNER JOIN DersTipi dt ON d.DersTipiID = dt.DersTipiID
                INNER JOIN SinifSeviyesi ss ON d.SinifSeviyeID = ss.SinifSeviyeID
                WHERE d.BolumID = @BolumID
                ORDER BY ss.SeviyeNo, d.DersAdi";

            SqlParameter[] parameters =
            {
                new SqlParameter("@BolumID", gelenBolumID)
            };

            DataTable dt = db.ExecuteQuery(query, parameters);

            dataGridView_dersler.DataSource = dt;

            GridAyarla();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Bu bölüme ait ders bulunamadı.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void GridAyarla()
        {
            dataGridView_dersler.ReadOnly = true;
            dataGridView_dersler.AllowUserToAddRows = false;
            dataGridView_dersler.AllowUserToDeleteRows = false;
            dataGridView_dersler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_dersler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView_dersler.Columns.Contains("DersID"))
            {
                dataGridView_dersler.Columns["DersID"].Visible = false;
            }
        }
    }
}
