#region Kütüphaneler
using System;
using System.Drawing;
#endregion

namespace h_dden
{
    class Stego
    {
        #region sabit
        //Enum program içerisinde belirleyeceğim sabitlerin anlamlandırılıp tutulması işii görür
        public enum Durum
        {
            Gizle, sifirUzunluk
           };
        #endregion


        #region Yazı Gizleme Kodu
        public static Bitmap yaziGizle(string yazi, Bitmap bmp)
        {
            Durum durum = Durum.Gizle; // resimde karakterleri gizliyorum

            int charIndex = 0; //Gizlenenen karakterin dizinini tutan değişken

            int charValue = 0; //Tam sayıya dönüştürülmüş karakterin değerini tutar

            long pixelElementIndex = 0;//İşlenmekte olan karakterin renk dizinini tutar  (R,G,B)

            int sifirSayisi = 0;//Süreç işlenip bittikten sonra eklenen son sıfırların sayısını tutar.

            int R = 0, G = 0, B = 0;//Piksel renk değerini tutacak


          

            for (int i = 0; i < bmp.Height; i++)  //görsel boyutlarını alıyorum
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);//İşleyeceğim pikseli alıyorum
                    R = pixel.R - pixel.R % 2;//Her piksel için en anlamsız biti temizliyorum. Soldan bir bit. 010001 değeri 101 ise 010000 değeri 100 olacaktır gibi
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;

                    for (int n = 0; n < 3; n++) //Her piksel elemanını inceleyip işlem yapacağım
                    {
                        if (pixelElementIndex % 8 == 0)//8 bit işlendi mi diye kontrol ediyorum
                        {
                            if (durum == Durum.sifirUzunluk && sifirSayisi == 8)//8 bit işlenince 8 sıfır ekle. Bu şekilde çözülürken sadece mesajı görmüş olacağım
                            {
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));//Gelen karakterlerin değerleri yazılıyor
                                }
                                return bmp;//İşlenen değeri döndürüyorum.
                            }
                            if (charIndex >= yazi.Length)//Tüm karakterlerin gizlenmiş olup olmadığını kontrol ediyorum
                            {
                                durum = Durum.sifirUzunluk;//Metnin sonuna işaretlemek için bir sıfır ekliyorum
                            }
                            else
                            {
                                charValue = yazi[charIndex++];//Bir sonraki karaktere geçip tekrar işlem yapma kısmı

                                Console.WriteLine(charValue); 
                            }
                        }

                        switch (pixelElementIndex % 3)//İşlenen karakterin renk değeri hangisi ise onunla işlem yapıyor
                        {
                            case 0:
                                {
                                    if (durum == Durum.Gizle)
                                    {
                                        R += charValue % 2;//Karakterdeki en sağdaki bit
                                        /*Çıkarılan en önemsi bit(LSB) yerine değer konulur.*/
                                        charValue /= 2;

                                        Console.WriteLine(R.ToString());
                                    }
                                    break;
                                }

                            case 1:
                                {
                                    if (durum == Durum.Gizle)
                                    {
                                        G += charValue % 2;
                                        charValue /= 2;

                                        Console.WriteLine(G.ToString());
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (durum == Durum.Gizle)
                                    {
                                        B += charValue % 2;
                                        charValue /= 2;

                                        Console.WriteLine(B.ToString());
                                    }
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                break;
                        }

                        pixelElementIndex++;
                        if (durum == Durum.sifirUzunluk)
                        {
                            sifirSayisi++;
                        }
                    }

                }

            }

            return bmp; 
           
        }
        #endregion



        #region yazı çözme
        public static string Coz(Bitmap bmp)
        {
            
            int colorUnitIndex = 0;
            int charVal = 0;
            string cikarilanYazi = "";//Çıkarılacak metni tutacağım
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);

                    for (int n = 0; n < 3; n++)//Her piksel için kontrol etmek
                    {
                        switch (colorUnitIndex % 3)
                        {
                            /*
                             * Pixel öğelerinden LSB'yi alıyoeum
                             * geçerli karakterin sağına bir bit ekliyorum( charVal = charVal * 2)
                             * eklenen biti değiştiriyorum (varsayılan değer 0)
                             */
                            case 0:
                                {
                                    charVal = charVal * 2 + pixel.R % 2;//Kırmızı pikselde bulunan en önemsi bit. Bu bit işlenmiş bittir
                                }
                                break;
                            case 1:
                                {
                                    charVal = charVal * 2 + pixel.G % 2;
                                }
                                break;
                            case 2:
                                {
                                    charVal = charVal * 2 + pixel.B % 2;
                                }
                                break;
                        }
                        colorUnitIndex++;

                        //8 bit eklenmişse

                        if (colorUnitIndex % 8 == 0)
                        {
                            charVal = reverseBits(charVal);/*8 bite ulaşana kadar işlem yapmaktayım. Fonksiyon kolaylık için yazıldı*/
                            //
                            Console.WriteLine(charVal);//Kaç????
                            //

                            if (charVal == 0)
                            {
                                return cikarilanYazi;
                            }
                            #region Türkçe Karakterlerin sorun çıkarmaması için
                            if (charVal == 94)/*Ü=220 Ç=*/
                            {
                                charVal = 350;

                            }
                            else if (charVal == 95)
                            {
                                charVal = 351;
                            }
                            else if (charVal == 48)
                            {
                                charVal = 304;
                            }
                            #endregion
                            char c = (char)charVal;

                            cikarilanYazi += c.ToString();
                        }
                    }
                }

            }

            return cikarilanYazi;
        }



        public static int reverseBits(int n)
        {
            int sonuc = 0;
            for (int i = 0; i < 8; i++)
            {
                sonuc = sonuc * 2 + n % 2;
                n /= 2;
            }
            return sonuc;
        }
    }
    #endregion
}

