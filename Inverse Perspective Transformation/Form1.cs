using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TersPerspektif
{

    public partial class Form1 : Form
    {
            double[,] R;
            double[,] Rt;
            double[] Matris;
            double[] TopMat;
            int xa, ya, za, nxs, nzs;
            double alfa, beta, teta, K, X, Y, bX, bY, payda,Z;
            double f1 = 0, f2 = 0, f3 = 0, f4 = 0, den1 = 0, den2 = 0;






        // String filename;
        double D;
            Bitmap floor;
            Bitmap background;
            Bitmap newBitmap = new Bitmap(400,400);



        double[] sinus;
            double[] cosinus;
        public Form1()
        {
            InitializeComponent();
            R = new double[3, 3];  //transformasyon matrisi
            Rt = new double[3, 3];  //ters transformasyon matrisi
            Matris = new double[3]; //xs-d-xz icin olusturuldu.
            TopMat = new double[3]; //
            sinus = new double[1800];
            cosinus=new double[1800];
            for (int i = 0; i < 1800; i++)
            {
                sinus[i] = Math.Sin(i * Math.PI / 1800);
                cosinus[i] = Math.Cos(i * Math.PI / 1800);
            }

        }

        public void ttransformasyon(double alfa, double beta, double teta)
        {
            Rt[0, 0] = Math.Cos(beta * Math.PI / 180) * Math.Cos(teta * Math.PI / 180);
            Rt[0, 1] = Math.Cos(beta * Math.PI / 180) * Math.Sin(teta * Math.PI / 180);
            Rt[0, 2] = - Math.Sin(beta * Math.PI / 180);

            Rt[1, 0] = - Math.Cos(alfa * Math.PI / 180) * Math.Sin(teta * Math.PI / 180) + Math.Sin(beta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180) * Math.Cos(teta * 3.14 / 180);
            Rt[1, 1] = Math.Cos(alfa * Math.PI / 180) * Math.Cos(teta * Math.PI / 180) + Math.Sin(beta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180) * Math.Sin(teta * Math.PI / 180);;
            Rt[1, 2] = Math.Cos(beta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180);

            Rt[2, 0] = Math.Sin(teta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180)  + Math.Sin(beta * Math.PI / 180) * Math.Cos(alfa * Math.PI / 180) * Math.Cos(teta * 3.14 / 180);
            Rt[2, 1] = - Math.Sin(alfa * Math.PI / 180) * Math.Cos(teta * Math.PI / 180) + Math.Sin(beta * Math.PI / 180) * Math.Cos(alfa * Math.PI / 180) * Math.Sin(teta * Math.PI / 180);
            Rt[2, 2] = Math.Cos(beta * Math.PI / 180) * Math.Cos(alfa * Math.PI / 180);
        }

        public void transformasyon(double alfa, double beta, double teta)
        {
            R[0, 0] = Math.Cos(beta * Math.PI / 180) * Math.Cos(teta * Math.PI / 180);
            R[0, 1] = - Math.Cos(alfa * Math.PI / 180) * Math.Sin(teta * Math.PI / 180) + Math.Sin(beta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180) * Math.Cos(teta * 3.14 / 180);
            R[0, 2] = Math.Sin(teta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180)  + Math.Sin(beta * Math.PI / 180) * Math.Cos(alfa * Math.PI / 180) * Math.Cos(teta * 3.14 / 180);

            R[1, 0] = Math.Cos(beta * Math.PI / 180) * Math.Sin(teta * Math.PI / 180);
            R[1, 1] = Math.Cos(alfa * Math.PI / 180) * Math.Cos(teta * Math.PI / 180) + Math.Sin(beta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180) * Math.Sin(teta * Math.PI / 180);;
            R[1, 2] = - Math.Sin(alfa * Math.PI / 180) * Math.Cos(teta * Math.PI / 180) + Math.Sin(beta * Math.PI / 180) * Math.Cos(alfa * Math.PI / 180) * Math.Sin(teta * Math.PI / 180);

            R[2, 0] = - Math.Sin(beta * Math.PI / 180);
            R[2, 1] = Math.Cos(beta * Math.PI / 180) * Math.Sin(alfa * Math.PI / 180);
            R[2, 2] = Math.Cos(beta * Math.PI / 180) * Math.Cos(alfa * Math.PI / 180);
        }



        //------------------------------------------------------------------------------------------------------
        //Bu fonksiyon girilen parametrelere göre 400x400'lük bir alana ters perspektif dönüşüm ile doku kaplar.
        //Default parametreler (X,Y,Z)=(0,60,150) (alfa,beta,teta)=(0,0,0)


        private void Apply_Click(object sender, EventArgs e)
        {//dolması gereken yer


            Graphics g = Graphics.FromImage(newBitmap);
            g.Clear(SystemColors.Control);

            xa = Convert.ToInt32(textBox1.Text);
            ya = Convert.ToInt32(textBox2.Text);
            za = Convert.ToInt32(textBox3.Text);
            alfa = Convert.ToInt32(textBox4.Text);
            beta = Convert.ToInt32(textBox5.Text);
            teta = Convert.ToInt32(textBox6.Text);
            D = Convert.ToInt32(textBox7.Text);
            

            for(int j = 0;  j < 400; j++)
            {
                for(int i = 0; i < 400; i++)
                {
                    nxs = i - (pictureBox1.Width >> 1);
                    nzs = ((pictureBox1.Height >> 1) - j);

                    Matris[0] = nxs;
                    Matris[1] = D;
                    Matris[2] = nzs;

                    
                    ttransformasyon(alfa, beta, teta);

                    //matris carpimi
                    TopMat[0] = Rt[0, 0] * Matris[0] + Rt[0, 1] * Matris[1] + Rt[0, 2] * Matris[2];
                    TopMat[1] = Rt[1, 0] * Matris[0] + Rt[1, 1] * Matris[1] + Rt[1, 2] * Matris[2];
                    TopMat[2] = Rt[2, 0] * Matris[0] + Rt[2, 1] * Matris[1] + Rt[2, 2] * Matris[2];



                    
                    if (TopMat[2] == 0)
                    {
                        continue;
                    }
                    
                    K = (-za / TopMat[2]);
                    TopMat[0] *= K; TopMat[1] *= K; TopMat[2] *= K;
                    X = xa + TopMat[0];
                    Y = ya + TopMat[1];


                    if (K > 0)
                    {
                        

                        f1 = Math.Floor(X + 0.5);
                        f2 = Math.Floor(Y + 0.5);

                        f3 = floor.Width;
                        f4 = floor.Height;

                        den1 = Math.Abs(f1 % f3);
                        den2 = Math.Abs(f2 % f4);

                        if (X < 0) //<
                        {
                            den1 = f3 - den1 -1 ;
                        }
                        if(Y < 0) //<
                        {
                            den2 = (int)(f4 - den2) % f4;
                        }

                        newBitmap.SetPixel(i, j, floor.GetPixel(Convert.ToInt32(den1), Convert.ToInt32(den2)));

                    }

                   

                    if (K < 0)
                    {


                        f1 = Math.Floor(X + 0.5);
                        f2 = Math.Floor(Y + 0.5);

                        f3 = background.Width;
                        f4 = background.Height;

                        den1 = Math.Abs(f1 % f3);
                        den2 = Math.Abs(f2 % f4);

                        if (X < 0)
                        {
                            den1 = f3 - den1 - 1;
                        }
                        if (Y > 0)
                        {
                            den2 = (int)(f4 - den2) % f4;
                        }

                        newBitmap.SetPixel(i, j, background.GetPixel(Convert.ToInt32(den1), Convert.ToInt32(den2)));
                    }

                    


                }

            }

            this.pictureBox1.Image = newBitmap;
            

            // CreateGraphics()->DrawRectangle(pen, temp_x, temp_y, 1, 1);

        }

//---------------------------------------------------------------------------
//Bu fonksiyon kullanıcıdan resim seçmesini ister ve zemini belleğe yükler.
        private void AndFloor_Click(object sender, EventArgs e)
        {

            resimAc.Title = "Browse BMP Files";
            resimAc.DefaultExt = "bmp";
            resimAc.Filter = "BMP files (*.bmp)|*.BMP";
            if (resimAc.ShowDialog() == DialogResult.OK)
            {
                floor = new Bitmap(resimAc.FileName);
            }
        }


//----------------------------------------------------------------------------------------------
//Bu fonksiyon kullanıcıdan resim seçmesini ister ve arka planı belleğe yükler.
        private void AndBackground_Click(object sender, EventArgs e)
        {

            resimAc.Title = "Browse BMP Files";
            resimAc.DefaultExt = "bmp";
            resimAc.Filter = "BMP files (*.bmp)|*.BMP";
            if (resimAc.ShowDialog() == DialogResult.OK)
            {
                background = new Bitmap(resimAc.FileName);
            }



        }

        private void OBackground_Click(object sender, EventArgs e)
        {
            resimAc.Title = "Browse BMP Files";
            resimAc.DefaultExt = "bmp";
            resimAc.Filter = "BMP files (*.bmp)|*.BMP";
            if (resimAc.ShowDialog() == DialogResult.OK)
            {
                background = new Bitmap(resimAc.FileName);
            }

            Graphics g = Graphics.FromImage(newBitmap);
            g.Clear(SystemColors.Control);

            xa = Convert.ToInt32(textBox1.Text);
            ya = Convert.ToInt32(textBox2.Text);
            za = Convert.ToInt32(textBox3.Text);
            alfa = Convert.ToInt32(textBox4.Text);
            beta = Convert.ToInt32(textBox5.Text);
            teta = Convert.ToInt32(textBox6.Text);
            D = Convert.ToInt32(textBox7.Text);

            for (int j = 0; j < 400; j++)
            {
                for (int i = 0; i < 400; i++)
                {
                    nxs = i - (pictureBox1.Width >> 1);
                    nzs = ((pictureBox1.Height >> 1) - j);

                    Matris[0] = nxs;
                    Matris[1] = D;
                    Matris[2] = nzs;


                    ttransformasyon(alfa, beta, teta);

                    //matris carpimi
                    TopMat[0] = Rt[0, 0] * Matris[0] + Rt[0, 1] * Matris[1] + Rt[0, 2] * Matris[2];
                    TopMat[1] = Rt[1, 0] * Matris[0] + Rt[1, 1] * Matris[1] + Rt[1, 2] * Matris[2];
                    TopMat[2] = Rt[2, 0] * Matris[0] + Rt[2, 1] * Matris[1] + Rt[2, 2] * Matris[2];


                    if (TopMat[2] == 0)
                    {
                        continue;
                    }

                    K = (-za / TopMat[2]);
                    TopMat[0] *= K; TopMat[1] *= K; TopMat[2] *= K;
                    X = xa + TopMat[0];
                    Y = ya + TopMat[1];



                    if (K < 0)
                    {


                        f1 = Math.Floor(X + 0.5);
                        f2 = Math.Floor(Y + 0.5);

                        f3 = background.Width;
                        f4 = background.Height;

                        den1 = Math.Abs(f1 % f3);
                        den2 = Math.Abs(f2 % f4);

                        if (X > 0)
                        {
                            den1 = f3 - den1 - 1;
                        }
                        if (Y > 0)
                        {
                            den2 = (int)(f4 - den2) % f4;
                        }

                        newBitmap.SetPixel(i, j, background.GetPixel(Convert.ToInt32(den1), Convert.ToInt32(den2)));
                    }


                    //this.pictureBox1.Image = newBitmap;

                }

            }

            this.pictureBox1.Image = newBitmap;

        }



        private void OFloor_Click(object sender, EventArgs e)
        {
            resimAc.Title = "Browse BMP Files";
            resimAc.DefaultExt = "bmp";
            resimAc.Filter = "BMP files (*.bmp)|*.BMP";
            if (resimAc.ShowDialog() == DialogResult.OK)
            {
                floor = new Bitmap(resimAc.FileName);
            }

            Graphics g = Graphics.FromImage(newBitmap);
            g.Clear(SystemColors.Control);

            xa = Convert.ToInt32(textBox1.Text);
            ya = Convert.ToInt32(textBox2.Text);
            za = Convert.ToInt32(textBox3.Text);
            alfa = Convert.ToInt32(textBox4.Text);
            beta = Convert.ToInt32(textBox5.Text);
            teta = Convert.ToInt32(textBox6.Text);
            D = Convert.ToInt32(textBox7.Text);

            for (int j = 0; j < 400; j++)
            {
                for (int i = 0; i < 400; i++)
                {
                    nxs = i - (pictureBox1.Width >> 1);
                    nzs = ((pictureBox1.Height >> 1) - j);

                    Matris[0] = nxs;
                    Matris[1] = D;
                    Matris[2] = nzs;


                    ttransformasyon(alfa, beta, teta);

                    //matris carpimi
                    TopMat[0] = Rt[0, 0] * Matris[0] + Rt[0, 1] * Matris[1] + Rt[0, 2] * Matris[2];
                    TopMat[1] = Rt[1, 0] * Matris[0] + Rt[1, 1] * Matris[1] + Rt[1, 2] * Matris[2];
                    TopMat[2] = Rt[2, 0] * Matris[0] + Rt[2, 1] * Matris[1] + Rt[2, 2] * Matris[2];



                    if (TopMat[2] == 0)
                    {
                        continue;
                    }

                    K = (-za / TopMat[2]);
                    TopMat[0] *= K; TopMat[1] *= K; TopMat[2] *= K;
                    X = xa + TopMat[0];
                    Y = ya + TopMat[1];



                    if (K > 0)
                    {


                        f1 = Math.Floor(X + 0.5);
                        f2 = Math.Floor(Y + 0.5);

                        f3 = floor.Width;
                        f4 = floor.Height;

                        den1 = Math.Abs(f1 % f3);
                        den2 = Math.Abs(f2 % f4);

                        if (X < 0)
                        {
                            den1 = f3 - den1 - 1;
                        }
                        if (Y < 0)
                        {
                            den2 = (int)(f4 - den2) % f4;
                        }

                        newBitmap.SetPixel(i, j, floor.GetPixel(Convert.ToInt32(den1), Convert.ToInt32(den2)));
                    }


                }

            }

            this.pictureBox1.Image = newBitmap;

        }


    }
}
