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



/*    
Hljóðs bið ek allar helgar kindir,
meiri ok minni mögu Heimdallar;
viltu at ek, Valföðr, vel fyr telja
forn spjöll fíra, þau er fremst um man.
 */


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

        int lsb1, lsb2;
        private void btnAc_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            dialog.Filter = "Resim Dosyaları (*.png,*.bmp,*.jpg)|*.png;*.bmp;*.jpg;";


            dialog.RestoreDirectory = true;
            dialog.Title = "Dosya Seçiniz..";
            

            string yol;
            yol = dialog.SafeFileName;



            if (string.IsNullOrWhiteSpace(yol))
            {
                MessageBox.Show("geçersiz yol");
            }

            else
            {
                picIsleme.Image = Image.FromFile(dialog.FileName);

                
               
               
                for (int i = 0; i < picIsleme.Image.Height; i++)
                {
                    for (int j = 0; j < picIsleme.Image.Width; j++)
                    {
                        lsb1 = picIsleme.Image.Height * picIsleme.Image.Width * 3;
                        lsb2 = lsb1 / 8;
                        toolStripSayi.Text = lsb2.ToString();

                        
                    
                    }

                }
                btnAc.Enabled = true;
                btnCoz.Enabled = true;
                btnGizle.Enabled = true;
                btnKaydet.Enabled = true;
                txtMesaj.Enabled = true;
                txtMesaj.Text = "";

            }
            
           


        }

    





    

        private void btnGizle_Click(object sender, EventArgs e)
        {
           
                bmp = (Bitmap)picIsleme.Image;
                string yazi = txtMesaj.Text;
                bmp = Stego.yaziGizle(yazi, bmp);
                MessageBox.Show("İşlendi. Resmi Kaydetmeyi unutmayın!");

                 

            

          
           
        
    }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Png);
                            MessageBox.Show("Kaydedildi!", "no problem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 1:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                            MessageBox.Show("Kaydedildi!", "no problem", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string coz = Stego.Coz(bmp);
            txtMesaj.Text = "";
            txtMesaj.Text = coz;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iletişimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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

        private void bunifuProgressBar1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuProgressBar.ProgressChangedEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
