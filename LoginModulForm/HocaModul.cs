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
    public partial class HocaModul : Form
    {
        int gelenBolumID;
        public HocaModul(int bolumID)
        {
            InitializeComponent();
            gelenBolumID = bolumID;
        }

        private void btn_derslerim_Click(object sender, EventArgs e)
        {
            HocaDersler hd =new HocaDersler(gelenBolumID);
            hd.Show();
            this.Hide();
        }

        private void btn_programolustur_Click(object sender, EventArgs e)
        {
            SinavProgramiOlustur spo = new SinavProgramiOlustur(gelenBolumID);
            spo.Show();
            this.Hide();
        }

       
    }
}
