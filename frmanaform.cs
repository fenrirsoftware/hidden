using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace h_dden
{
    public partial class frmanaform : Form
    {
        private Bitmap bmp = null;
        private string cikarilanYazi = string.Empty;
        public frmanaform()
        {
            InitializeComponent();
        }

        private void btnAc_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Resim Dosyaları (*.png,*.bmp,*.jpg,*.gif)|*.png;*.bmp;*.jpg;*.gif";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picIsleme.Image = Image.FromFile(dialog.FileName);
                btnGizle.Enabled = true;
            }
            int lsb1, lsb2;
            for (int i = 0; i < picIsleme.Height; i++)
            {
                for (int j = 0; j < picIsleme.Width; j++)
                {
                    lsb1 = picIsleme.Height * picIsleme.Width * 3;
                    lsb2 = lsb1 / 8;
                    toolStripSayi.Text = lsb2.ToString();
                }
            }
          
            btnAc.Enabled = true;
            btnCoz.Enabled = true;
            btnGizle.Enabled = true;
            btnKaydet.Enabled = true;
       
           
            
        }

        private void btnGizle_Click(object sender, EventArgs e)
        {
           
                bmp = (Bitmap)picIsleme.Image;
                string yazi = txtMesaj.Text;
                bmp = StegoIslem.yaziGizle(yazi, bmp);
                MessageBox.Show("İşlendi. Resmi Kaydetmeyi unutmayın!");

                 

            

          
           
        
    }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp|Gif Image|*.gif";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Png);
                        }
                        break;
                    case 1:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                        }
                        break;

                    case 2:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Gif);
                        }
                        break;



                }
                picIsleme.Image = null;
                txtMesaj.Text = null;
            }
        }

        private void btnCoz_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)picIsleme.Image;
            string coz = StegoIslem.Coz(bmp);
            txtMesaj.Text = "";
            txtMesaj.Text = coz;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iletişimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formtht = new formtht();
            formtht.Show();

        }

        private void bilgilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bir görsel seçip önce 'aç' butonuna tıklayınız \n sonra mesaj ekleme yerine mesajınızı girin ve 'gizle'butonuna tıklayınız sonrasında fotoğrafı kaydedin. \n zaten şifrelenmiş bir görseli çözmek istiyorsanız çöz butonuna tıklayınız. ", "nasıl kullanılır?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripSayi_Click(object sender, EventArgs e)
        {

        }

        private void frmanaform_Load(object sender, EventArgs e)
        {

        }
    }
}
