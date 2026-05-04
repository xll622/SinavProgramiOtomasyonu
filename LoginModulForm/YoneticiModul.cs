using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModulForm
{
    public partial class YoneticiModul : Form
    {
        public YoneticiModul()
        {
            InitializeComponent();
        }

        private void btn_kullanici_Click(object sender, EventArgs e)
        {
            KullaniciYonetimi kytm = new KullaniciYonetimi();
            kytm.Show();
            this.Hide();
        }

        private void btn_bolum_Click(object sender, EventArgs e)
        {
            BolumYonetimi blytm = new BolumYonetimi();
            blytm.Show();
            this.Hide();
        }

        private void btn_ders_Click(object sender, EventArgs e)
        {
            DersYonetimi dytm = new DersYonetimi();
            dytm.Show();
            this.Hide();
        }
    }
}
