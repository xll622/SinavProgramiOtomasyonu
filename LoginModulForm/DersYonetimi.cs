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
    public partial class DersYonetimi : Form
    {
        public DersYonetimi()
        {
            InitializeComponent();
        }
        int bolum_id;
        int tip_id;
        int seviye_id;
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string d_ad = text_eklead.Text.Trim();
            string b_ad = cmb_eklebolum.Text.Trim();
            string t_ad = cmb_ekletip.Text.Trim();
            string s_no = cmb_ekleseviye.Text.Trim();

            string query = @"
            INSERT INTO Ders
            (DersAdi, BolumID, DersTipiID, SinifSeviyeID, Kredi, SinavSuresi, DersiAlanOgrenciSayisi)
            SELECT 
                @dersadi,
                b.BolumID,
                t.DersTipiID,
                s.SinifSeviyeID,
                @kredi,
                @sure,
                @ogrencisayisi
            FROM Bolum b
            INNER JOIN DersTipi t ON t.TipAd = @tipAd
            INNER JOIN SinifSeviyesi s ON s.SeviyeNo = @seviyeNo AND s.BolumID = b.BolumID
            WHERE b.BolumAd = @bolumAd";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@dersadi", d_ad),
            new SqlParameter("@bolumAd", b_ad),
            new SqlParameter("@tipAd", t_ad),
            new SqlParameter("@seviyeNo", s_no),
            new SqlParameter("@kredi", Convert.ToInt32(nmup_eklekredi.Text)),
            new SqlParameter("@sure", Convert.ToInt32(nmup_eklesure.Text)),
            new SqlParameter("@ogrencisayisi", Convert.ToInt32(nmup_eklesayi.Text))
            };

            int sonuc = db.ExecuteNonQuery(query, parameters);

            if (sonuc > 0)
            {
                MessageBox.Show("Ders başarıyla eklendi.");

                text_eklead.Clear();
                cmb_eklebolum.SelectedIndex = -1;
                cmb_ekletip.SelectedIndex = -1;
                cmb_ekleseviye.Items.Clear();
                nmup_eklekredi.Value=0;
                nmup_eklesure.Value = 0;
                nmup_eklesayi.Value = 0;
            }
            else
            {
                MessageBox.Show("Ders eklenemedi (eşleşen veri yok olabilir).");
            }


        }

        private void DersYonetimi_Load(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string query = "SELECT BolumAd FROM Bolum";
            string query1 = "SELECT TipAd FROM DersTipi";

            //BÖLÜMLERİ YÜKLE
            DataTable dtBolum = db.ExecuteQuery(query);
            for (int i = 0; i < dtBolum.Rows.Count; i++)
            {
                cmb_eklebolum.Items.Add(dtBolum.Rows[i][0].ToString());
            }

            //DERS TİPİ YÜKLE

            DataTable dtTip = db.ExecuteQuery(query1);
            for (int i = 0; i < dtTip.Rows.Count; i++)
            {
                cmb_ekletip.Items.Add(dtTip.Rows[i][0].ToString());
            }
        }

        private void cmb_eklebolum_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string b_ad = cmb_eklebolum.Text.Trim();
            string query1 = "select BolumID from Bolum where BolumAd=@B_Ad";
            SqlParameter[] parameters = new SqlParameter[] {
             new SqlParameter("@B_Ad", b_ad),
            };
            DataTable dt1 = db.ExecuteQuery(query1, parameters);
            bolum_id=Convert.ToInt32(dt1.Rows[0][0]);

            cmb_ekleseviye.Items.Clear();

            string querySeviye = "SELECT SeviyeNo FROM SinifSeviyesi WHERE BolumID=@bid";
            SqlParameter[] p = {
                new SqlParameter("@bid", bolum_id)
            };

            DataTable dtSeviye = db.ExecuteQuery(querySeviye, p);

            for (int i = 0; i < dtSeviye.Rows.Count; i++)
            {
                cmb_ekleseviye.Items.Add(dtSeviye.Rows[i][0].ToString());
            }
        }

        private void btn_tipekle_Click(object sender, EventArgs e)
        {
            DataBaseClass db=new DataBaseClass(connectionString);
            string t_ad_ek = text_tipekle.Text.Trim();
            string query = "INSERT INTO DersTipi(TipAd) VALUES (@tadi)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@tadi", t_ad_ek)
            };
            int sonuc = db.ExecuteNonQuery(query, parameters);
            if (sonuc > 0)
            {
                MessageBox.Show("Ders Tipi Eklendi");
                text_tipekle.Clear();
            }
            else
            {
                MessageBox.Show("Hata");
            }

        }

        private void cmb_ekletip_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string tad = cmb_ekletip.Text.Trim();
            string query1 = "SELECT DersTipiID FROM DersTipi WHERE TipAd=@t_ad";
            SqlParameter[] parameters = new SqlParameter[] {
             new SqlParameter("@t_ad", tad)
            };
            DataTable dt1 = db.ExecuteQuery(query1, parameters);
            tip_id= Convert.ToInt32(dt1.Rows[0][0]);
        }

        private void cmb_ekleseviye_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string sno = cmb_ekleseviye.Text.Trim();
            string query3 = "SELECT SinifSeviyeID FROM SinifSeviyesi WHERE SeviyeNo=@s_no AND BolumID=@bid";
            SqlParameter[] parameters = new SqlParameter[] {
             new SqlParameter("s_no", sno),
             new SqlParameter("@bid", bolum_id)
            };
            DataTable dt2 = db.ExecuteQuery(query3, parameters);
            seviye_id = Convert.ToInt32(dt2.Rows[0][0]);

        }
    }
}
