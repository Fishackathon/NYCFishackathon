using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IniciacaoCientifica
{
    public delegate void delPassData(Image imagem);
    public partial class toPB : Form
    {
        public Bitmap img, bmp2, original;
        ImageData u;
        public void funData(Image i)
        {
            final1 = (Bitmap)i;
            img = new Bitmap(i);
            u = new ImageData(img);
            original = (Bitmap)i;
            boximagem.SizeMode = PictureBoxSizeMode.StretchImage;
            var bmp2 = new Bitmap(boximagem.Width, boximagem.Height);
            using (var g = Graphics.FromImage(bmp2))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(img, new Rectangle(Point.Empty, bmp2.Size));
                boximagem.Image = bmp2;
            }
        }
        
        public toPB()
        {
            InitializeComponent();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            img = original;
            final1 = u.getBitmapBinary(trackBar1.Value);
            boximagem.SizeMode = PictureBoxSizeMode.StretchImage;
            var bmp3 = new Bitmap(boximagem.Width, boximagem.Height);
            using (var g = Graphics.FromImage(bmp3))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(final1, new Rectangle(Point.Empty, bmp3.Size));
                boximagem.Image = bmp3;
            }
        }
        private void carregar(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            img = original;
            final1 = u.getBitmapBinary(trackBar1.Value);
            boximagem.SizeMode = PictureBoxSizeMode.StretchImage;
            var bmp3 = new Bitmap(boximagem.Width, boximagem.Height);
            using (var g = Graphics.FromImage(bmp3))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(final1, new Rectangle(Point.Empty, bmp3.Size));
                boximagem.Image = bmp3;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            window j = new window();
            Bitmap bit = u.getBitmapBinary(trackBar1.Value);
            delPassData del = new delPassData(j.funData);
            del(this.final1);
            j.Show();
            this.Hide();
        }
        public Bitmap final1 { get; set; }
    }
}
