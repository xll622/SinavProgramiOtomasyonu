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
    public partial class KullaniciArama : Form
    {
        public KullaniciArama()
        {
            InitializeComponent();
        }
        public string islemTipi;
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciYonetimi klytm = new KullaniciYonetimi();
            DataBaseClass db = new DataBaseClass(connectionString);
            string k_ad= text_aramaad.Text.Trim();
            string k_rol = text_aramarol.Text.Trim();
            string k_bolum = text_aramabolum.Text.Trim();
            string query = @"SELECT 
                    k.KullaniciAdi,
                    k.Rol,
                    b.BolumAd
                 FROM Kullanici k
                 LEFT JOIN Bolum b ON k.BolumID = b.BolumID
                 WHERE k.KullaniciAdi=@k_ad 
                 OR k.Rol=@k_rol 
                 OR b.BolumAd=@k_bolum";
            SqlParameter[] parameters = new SqlParameter[] {
                 new SqlParameter("@k_ad",k_ad),
                 new SqlParameter("@k_rol", k_rol),
                 new SqlParameter("@k_bolum", k_bolum)
            };
            DataTable dt = db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                klytm.Show();
                this.Hide();

                if (islemTipi == "sil")
                {
                    klytm.text_silad.Text = dt.Rows[0][0].ToString();
                    klytm.text_silrol.Text = dt.Rows[0][1].ToString();
                    klytm.text_silbolum.Text = dt.Rows[0][2].ToString();
                }
                else if (islemTipi == "guncelle")
                {
                    klytm.text_guncellead.Text = dt.Rows[0][0].ToString();
                    klytm.text_guncellerol.Text = dt.Rows[0][1].ToString();
                    klytm.cmb_guncellebolum.Text = dt.Rows[0][2].ToString();
                }
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı");
            }
        }

        private void lbl_klytm_Click(object sender, EventArgs e)
        {
            this.Close();
            KullaniciYonetimi klytm = new KullaniciYonetimi();
            klytm.Show();

        }
    }
}
