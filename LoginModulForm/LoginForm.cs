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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost; Initial Catalog=SinavProgrami; Integrated Security=true";
            DataBaseClass dbc = new DataBaseClass(connectionString);
            string username = textBox_kad.Text.Trim();
            string password = textBox_ksifre.Text.Trim();
            string query = "Select * From Kullanici where [KullaniciAdi]=@KullaniciAdi and [Sifre]=@Sifre";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@KullaniciAdi", username),
                new SqlParameter("@Sifre", password)
            };

            DataTable dt = dbc.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                string rol = dt.Rows[0]["Rol"].ToString();
                this.Hide();
                if (rol == "admin")
                {
                    YoneticiModul ynt = new YoneticiModul();
                    ynt.Show();
                }
                else if (rol == "hoca")
                {
                    int bolumID = 0;

                    if (dt.Rows[0]["BolumID"] != DBNull.Value)
                    {
                        bolumID = Convert.ToInt32(dt.Rows[0]["BolumID"]);
                    }

                    new HocaModul(bolumID).Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı");
            }

        }
    }
}
