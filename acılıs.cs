using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BunifuAnimatorNS;


namespace h_dden
{
    public partial class acılıs : Form
    {
        public acılıs()
        {
            InitializeComponent();
        }

        //açılış animasyonu 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.050;
            }
            else
            {
                timer1.Stop();
                this.Hide();
              frmanaform frmanaform = new frmanaform();
                frmanaform.Show();
             
            }
        }

        private void acılıs_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
