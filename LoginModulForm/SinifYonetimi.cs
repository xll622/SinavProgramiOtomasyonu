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
    public partial class SinifYonetimi : Form
    {
        public SinifYonetimi()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";
        private void btn_bolumekle_Click_1(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string s_blm_ek = cmb_blmek.Text.Trim();
            int s_seviye_ek = Convert.ToInt32(cmb_seviyeek.Text);
            int s_mevcut_ek = (int)nmup_mevcudek.Value;
            string query = "INSERT INTO SinifSeviyesi (SeviyeNo, SinifMevcudu, BolumID) SELECT @sno, @smvcd, b.BolumID FROM Bolum b  WHERE b.BolumAd = @bolumAd";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@sno", s_seviye_ek),
                new SqlParameter("@smvcd", s_mevcut_ek),
                new SqlParameter("@bolumAd", s_blm_ek)
            };
            int sonuc = db.ExecuteNonQuery(query, parameters);
            if (sonuc > 0)
            {
                MessageBox.Show("Sınıf Seviyesi eklendi");
            }
            else
            {
                MessageBox.Show("Hata");
            }
        }

        private void SinifYonetimi_Load(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string query = "SELECT BolumAd FROM Bolum";
            DataTable dt = db.ExecuteQuery(query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_blmek.Items.Add(dt.Rows[i][0].ToString());

            }

            cmb_blmek.SelectedIndex = -1;

        }
    }
}
