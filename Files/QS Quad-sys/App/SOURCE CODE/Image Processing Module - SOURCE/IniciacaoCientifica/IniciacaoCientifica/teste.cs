using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IniciacaoCientifica
{
    public partial class teste : Form
    {
        public teste()
        {
            InitializeComponent();
            Image imagem = Image.FromFile("C:/export/original.png");
            Bitmap bmp = new Bitmap(imagem);
            original.SizeMode = PictureBoxSizeMode.StretchImage;
            var bmp2 = new Bitmap(original.Width, original.Height);
            using (var g = Graphics.FromImage(bmp2))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, new Rectangle(Point.Empty, bmp2.Size));
                original.Image = bmp2;
            }
            ImageData imagem1 = new ImageData(bmp);
            
            //if (imagem1.Type == imageType.Binary)
            //    MessageBox.Show("Bin");
            //else
            //    MessageBox.Show("não");
            //*/
            //Bitmap img1 = new Bitmap(Image.FromFile("D:/img/imagemdif1.png"));
            //Bitmap img2 = new Bitmap(Image.FromFile("D:/img/imagemdif2.png"));
            //ImageData im1 = new ImageData(img1);
            //ImageData im2 = new ImageData(img2);
            //Morfologia.Uniao(im1, im2);
            //Morfologia.ErodeImg(imagem1);
            //imagem1 = (ImageData)im1.Clone();
            //MorfologiaCinza.Abertura(imagem1);
            Esqueleto.Processa(imagem1);
            Bitmap final1 = imagem1.getBitmap();
            final1.Save("C:/export/export.png");
            final.SizeMode = PictureBoxSizeMode.StretchImage;
            var bmp3 = new Bitmap(final.Width, final.Height);
            using (var g = Graphics.FromImage(bmp3))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, new Rectangle(Point.Empty, bmp2.Size));
                final.Image = bmp3;
            }
        }

        private void original_Click(object sender, EventArgs e)
        {

        }
    }
}
