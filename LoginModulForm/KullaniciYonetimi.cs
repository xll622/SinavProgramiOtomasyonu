using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModulForm
{
    public partial class KullaniciYonetimi : Form
    {
        public KullaniciYonetimi()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";
        int bolum_id;
        private void label_geri_Click(object sender, EventArgs e)
        {
            this.Close();
            YoneticiModul ymdl = new YoneticiModul();
            ymdl.Show();
        }

        private void label_geri2_Click(object sender, EventArgs e)
        {
            this.Close();
            YoneticiModul ymdl = new YoneticiModul();
            ymdl.Show();
        }

        private void label_geri3_Click(object sender, EventArgs e)
        {
            this.Close();
            YoneticiModul ymdl = new YoneticiModul();
            ymdl.Show();
        }

        private void label_geri4_Click(object sender, EventArgs e)
        {
            this.Close();
            YoneticiModul ymdl = new YoneticiModul();
            ymdl.Show();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string k_ad_ek = text_eklead.Text.Trim();
            string k_sifre_ek = text_eklesifre.Text.Trim();
            string k_rol_ek = cmb_eklerol.Text.Trim();
            string k_bolum_ek = cmb_eklebolum.Text.Trim();
            string query = "INSERT INTO Kullanici (KullaniciAdi, Sifre, Rol, BolumID) SELECT @kadi, @sifre, @rol, b.BolumID FROM Bolum b  WHERE b.BolumAd = @bolumAd";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@kadi", k_ad_ek),
                new SqlParameter("@sifre", k_sifre_ek),
                new SqlParameter("@rol", k_rol_ek),
                new SqlParameter("@bolumAd", k_bolum_ek)            
            };
            int sonuc = db.ExecuteNonQuery(query, parameters);
            if (sonuc > 0)
            {
                MessageBox.Show("Kullanıcı eklendi");
                FormuTemizle();
            }
            else
            {
                MessageBox.Show("Hata");
            }
        }

        private void KullaniciYonetimi_Load(object sender, EventArgs e)  //
        {          
            DataBaseClass db = new DataBaseClass(connectionString);
            string query = "SELECT BolumAd FROM Bolum";
            DataTable dt = db.ExecuteQuery(query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_eklebolum.Items.Add(dt.Rows[i][0].ToString());
                cmb_guncellebolum.Items.Add(dt.Rows[i][0].ToString());
            }

            cmb_eklebolum.SelectedIndex = -1;
            cmb_guncellebolum.SelectedIndex = -1;
            cmb_eklerol.SelectedIndex = -1;


            ToolTip mesaj = new ToolTip();
            mesaj.ToolTipTitle = "Arama";
            mesaj.ToolTipIcon = ToolTipIcon.Info;
            mesaj.ShowAlways = true;
            mesaj.SetToolTip(text_silad, "Arama için F4 tuşuna basınız");
            mesaj.SetToolTip(text_guncellead, "Arama için F4 tuşuna basınız");

        }
        private void cmb_eklerol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_eklerol.Text.ToLower() == "admin")
            {
                cmb_eklebolum.Enabled = false;
                cmb_eklebolum.SelectedIndex = -1;  //
            }
            else
            {
                cmb_eklebolum.Enabled = true;
            };

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            DialogResult mesaj = MessageBox.Show("Bu Kaydı Silmek İstediğinizden Emin misiniz?", "Uyarı",
                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if(mesaj== DialogResult.Yes)
            {
                string k_ad = text_silad.Text.Trim();
                string query = "DELETE FROM Kullanici WHERE KullaniciAdi = @ad";
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@ad", k_ad)

                };
                int d_kyt_sy = db.ExecuteNonQuery(query, parameters);
                if (d_kyt_sy > 0)
                {
                    MessageBox.Show("Kayıt Silindi");
                    FormuTemizle();
                }
                else
                {
                    MessageBox.Show("Kayıt Silinmedi");
                }
            }
            else
            {
                MessageBox.Show("Silme işlemi iptal edildi");
            }
        }

        private void btn_gıncelle_Click(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            DialogResult mesaj = MessageBox.Show("Bu Kaydı Güncellemek İstediğinizden Emin misiniz?", "Uyarı",
                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if(mesaj== DialogResult.Yes)
            {
                string ad = text_guncellead.Text.Trim();
                string rol = text_guncellerol.Text.Trim();     
                object bolumID = DBNull.Value;

                if (rol.ToLower() == "hoca")
                {
                    bolumID = cmb_guncellebolum.SelectedValue;
                }

                string query = @"
                       UPDATE Kullanici
                       SET 
                       Rol = @rol,
                       BolumID = @bolumID
                       WHERE KullaniciAdi = @ad";

                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@ad", ad),
                new SqlParameter("@rol", rol),
                new SqlParameter("@bolumID", bolumID)
                };

                int sonuc = db.ExecuteNonQuery(query, parameters);

                if (sonuc > 0)
                {
                    MessageBox.Show("Güncellendi");
                    FormuTemizle();
                }

                else
                {
                   MessageBox.Show("Güncelleme başarısız");
                }
                   
            }
            else
            {
                MessageBox.Show("Silme işlemi iptal edildi");
            }
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);

            string query = @"
                      SELECT 
                         k.KullaniciID,
                         k.KullaniciAdi,
                         k.Rol,
                         b.BolumAd
                      FROM Kullanici k
                      LEFT JOIN Bolum b ON k.BolumID = b.BolumID";
            DataTable dt = db.ExecuteQuery(query);

            dataGrid_listele.DataSource = dt;

        }

        private void FormuTemizle()
        {
            // EKLE ALANI
            text_eklead.Clear();
            text_eklesifre.Clear();
            cmb_eklerol.SelectedIndex = -1;
            cmb_eklebolum.SelectedIndex = -1;
            cmb_eklebolum.Enabled = false;

            // SİL ALAN
            text_silad.Clear();
            text_silrol.Clear();
            text_silbolum.Clear();

            // GÜNCELLE ALANI
            text_guncellead.Clear();
            text_guncellerol.Clear();
            cmb_guncellebolum.SelectedIndex = -1;
        }

        private void text_silad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                KullaniciArama kfrm = new KullaniciArama();
                kfrm.islemTipi = "sil";
                kfrm.Show();
                this.Hide();
            }
        }

        private void text_guncellead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                KullaniciArama kfrm = new KullaniciArama();
                kfrm.islemTipi = "guncelle";
                kfrm.Show();
                this.Hide();

            }
        }

        private void cmb_eklebolum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
