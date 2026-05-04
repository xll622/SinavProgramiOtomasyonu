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
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";
        private void btn_ekle_Click(object sender, EventArgs e)
        {

        }

        private void DersYonetimi_Load(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);
            string query = "SELECT BolumAd FROM Bolum";
            string query1 = "SELECT TipAd FROM DersTipi";
            DataTable dtBolum = db.ExecuteQuery(query);
            for (int i = 0; i < dtBolum.Rows.Count; i++)
            {
                cmb_eklebolum.Items.Add(dtBolum.Rows[i][0].ToString());
            }

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
            string query1 = "SELECT TipID FROM DersTipi WHERE TipAd=@t_ad";
            SqlParameter[] parameters = new SqlParameter[] {
             new SqlParameter("t_ad", tad)
            };
            DataTable dt1 = db.ExecuteQuery(query1, parameters);
            tip_id= Convert.ToInt32(dt1.Rows[0][0]);
        }
    }
}
