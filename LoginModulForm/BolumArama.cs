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
    public partial class BolumArama : Form
    {
        public BolumArama()
        {
            InitializeComponent();
        }
        public string islemTipi;
        string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami;Integrated Security=true";

        private void btn_arabolum_Click(object sender, EventArgs e)
        {
            BolumYonetimi bytm = new BolumYonetimi();
            DataBaseClass db = new DataBaseClass(connectionString);
            string b_ad = cmb_bolum.Text;
            string query = @"SELECT BolumAd FROM Bolum WHERE BolumAd=@b_ad";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@b_ad", b_ad)
            };
            DataTable dt = db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                bytm.Show();
                this.Hide();

                if (islemTipi == "sil")
                {
                    bytm.text_silbolum.Text = dt.Rows[0][0].ToString();
                    
                }
                else if (islemTipi == "guncelle")
                {
                    bytm.text_guncellebolum.Text = dt.Rows[0][0].ToString();
                   
                }
            }
            else
            {
                MessageBox.Show("Kayıt bulunamadı");
            }

        }

        private void BolumArama_Load(object sender, EventArgs e)
        {
            DataBaseClass db = new DataBaseClass(connectionString);

            string query = "SELECT BolumID, BolumAd FROM Bolum";
            DataTable dt = db.ExecuteQuery(query);

            cmb_bolum.DataSource = dt;
            cmb_bolum.DisplayMember = "BolumAd";
            cmb_bolum.ValueMember = "BolumID";

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            BolumYonetimi blytm = new BolumYonetimi();
            blytm.Show();
        }
    }
}
