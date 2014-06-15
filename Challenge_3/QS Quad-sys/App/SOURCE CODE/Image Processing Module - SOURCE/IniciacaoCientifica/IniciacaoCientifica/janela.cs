using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace IniciacaoCientifica
{
    public partial class janela : Form
    {
        public janela()
        {
            InitializeComponent();
            button2.Hide();
            label9.Hide();
            trackBar1.Hide();
            label2.Hide();
            comboBox1.Hide();
            groupBox1.Hide();

        }
        private void botaoOk_Click(object sender, EventArgs e)
        {
            //define as propriedades do controle 
            //OpenFileDialog
            this.ofd1.Multiselect = false;
            this.ofd1.Title = "Selecionar Fotos";
            ofd1.InitialDirectory = @"D:\img";
            //filtra para exibir somente arquivos de imagens
            ofd1.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.FilterIndex = 2;
            ofd1.RestoreDirectory = true;
            ofd1.ReadOnlyChecked = false;
            ofd1.ShowReadOnly = false;

            DialogResult dr = this.ofd1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Le os arquivos selecionados 
                foreach (String arquivo in ofd1.FileNames)
                {
                    endereco.Text = "";
                    endereco.Text += arquivo;
                    // cria um PictureBox
                    try
                    {
                        flowLayoutPanel1.Controls.Clear();
                        Imagem = Image.FromFile(arquivo);
                        PictureBox pb = new PictureBox();
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        //para exibir as imagens em tamanho natural 
                        //descomente as linhas abaixo e comente as duas seguintes
                        //pb.Height = loadedImage.Height;
                        //pb.Width = loadedImage.Width;
                        pb.Height = 500;
                        pb.Width = 410;
                        //atribui a imagem ao PictureBox - pb
                        pb.Image = Imagem;
                        Bitmap imgbmp = new Bitmap(Imagem);
                        ImageData dado = new ImageData(imgbmp);
                        bool binario = dado.Type == imageType.Binary;//dado.verificaBinary();
                        if (binario)
                        {
                            label3.Text = "Binária: SIM";
                        }
                        else
                            label3.Text = "Binária: NÃO";
                        bool cinza = dado.Type == imageType.Gray;//dado.verificaGray();
                        if (cinza)
                        {
                            label4.Text = "Escala de Cinza: SIM";
                        }
                        else
                            label4.Text = "Escala de Cinza: NÃO";
                        int h, l;
                        h = dado.getAltura();
                        l = dado.getLargura();
                        label6.Text = "Dimensões: " + h + "x" + l;
                        float tamanhoarq = (float)new System.IO.FileInfo(endereco.Text).Length;
                        float original = tamanhoarq;
                        String extTamanho = " bytes";
                        if (tamanhoarq > 1000.0 && tamanhoarq < 1000000.0)
                        {
                            tamanhoarq = tamanhoarq/1000;
                            extTamanho = " KB";
                        }
                        if (tamanhoarq>1000000.0)
                        {
                            tamanhoarq = tamanhoarq/1000000;
                            extTamanho = " MB";
                        }

                        label8.Text = "Porcentagem de preto: " + Math.Round(Estatistica.PorcentagemPretos(dado),3).ToString() + "%";

                        label7.Text = "Tamanho em disco: " + tamanhoarq + extTamanho;
                        //inclui a imagem no containter flowLayoutPanel
                        flowLayoutPanel1.Controls.Add(pb);
                        //System.Drawing.Color cor;
                        //cor = mainn.retornaCorR(arquivo);
                        //label2.Text = cor.ToString();
                        //exibecor.BackColor = cor;
                        //int ehPB = mainn.pb(arquivo);
                        /*
                         * if (ehPB == 1)
                            pbsn.Text = "SIM";
                        else
                            pbsn.Text = "NÃO";
                         */
                    }
                    finally
                    {
                        button2.Show();
                        label2.Show();
                        label9.Show();
                        trackBar1.Show();
                        comboBox1.Show();
                        groupBox1.Show();
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ofd1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void botaoOk_Click(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
/*
        private void label7_Click(object sender, EventArgs e)
        {

        }
 */
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"D:\img";
            sfd.Title = "Especifique o nome e o caminho para o arquivo a ser salvo:";
            sfd.Filter = "PNG(*.png)|*.png|JPG(*.jpg)|*.jpg";
            sfd.Filter += "|BITMAP(*.bmp)|*.bmp";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                ImageFormat imgFormat = ImageFormat.Png;
                if (fileExtension == "BMP")
                {
                    imgFormat = ImageFormat.Bmp;
                }
                else if (fileExtension == "JPG")
                {
                    imgFormat = ImageFormat.Jpeg;
                }
                StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                Imagem.Save(streamWriter.BaseStream, imgFormat);
                streamWriter.Flush();
                streamWriter.Close();
            }
            float tamanhoarq = (float)new System.IO.FileInfo(sfd.FileName).Length;
            float original = tamanhoarq;
            String extTamanho = " bytes";
            if (tamanhoarq > 1000.0 && tamanhoarq < 1000000.0)
            {
                tamanhoarq = tamanhoarq / 1000;
                extTamanho = " KB";
            }
            if (tamanhoarq > 1000000.0)
            {
                tamanhoarq = tamanhoarq / 1000000;
                extTamanho = " MB";
            }
            label7.Text = "Tamanho em disco: " + tamanhoarq + extTamanho;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            PictureBox novaImagem = new PictureBox();
            novaImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            novaImagem.Height = 500;
            novaImagem.Width = 410;
            String opcaoProc = comboBox1.Text;
            Bitmap final, imgpr;
            imgpr = new Bitmap(Imagem);
            final = new Bitmap(Imagem);
            ImageData pct = new ImageData(final);
            label8.Text = "Porcentagem de preto: " + Math.Round(Estatistica.PorcentagemPretos(pct), 3).ToString() + "%";
            switch (opcaoProc)
            {
                case "Dilatação (PB)":
                    ImageData img = new ImageData(final);
                    Morfologia.DilataImg(img, trackBar1.Value);
                    final = img.getBitmap();
                    break;
                case "Erosão (PB)":
                    ImageData img2 = new ImageData(final);
                    Morfologia.ErodeImg(img2, trackBar1.Value);
                    final = img2.getBitmap();
                    break;
                case "Abertura (PB)":
                    ImageData img3 = new ImageData(final);
                    Morfologia.Abertura(img3, trackBar1.Value);
                    final = img3.getBitmap();
                    break;
                case "Fechamento (PB)":
                    ImageData img4 = new ImageData(final);
                    Morfologia.Fechamento(img4, trackBar1.Value);
                    final = img4.getBitmap();
                    break;
                case "Borda Externa (PB)":
                    ImageData img11 = new ImageData(final);
                    Morfologia.BordaExterna(img11);
                    final = img11.getBitmap();
                    break;
                case "Borda Interna (PB)":
                    ImageData img12 = new ImageData(final);
                    Morfologia.BordaInterna(img12);
                    final = img12.getBitmap();
                    break;
                case "Borda Efetiva (PB)":
                    ImageData img13 = new ImageData(final);
                    Morfologia.Borda(img13);
                    final = img13.getBitmap();
                    break;
                case "Remover Borda(PB)":
                    ImageData img14 = new ImageData(final);
                    Morfologia.RemoveBorda(img14);
                    final = img14.getBitmap(); 
                    break;
                case "Esqueleto (PB)":
                    ImageData img9 = new ImageData(final);
                    Esqueleto.Processa(img9);
                    final = img9.getBitmap();
                    break;
                case "Dilatação (Colorido)":
                    ImageData img10 = new ImageData(final);
                    MorfologiaColorida.DilataImg(img10, trackBar1.Value);
                    final = img10.getBitmap();
                    break;
                case "Dilatação (Cinza)":
                    ImageData img5 = new ImageData(final);
                    MorfologiaCinza.DilataImg(img5, trackBar1.Value);
                    final = img5.getBitmap();
                    break;
                case "Erosão (Cinza)":
                    ImageData img6 = new ImageData(final);
                    MorfologiaCinza.ErodeImg(img6, trackBar1.Value);
                    final = img6.getBitmap();
                    break;
                case "Abertura (Cinza)":
                    ImageData img7 = new ImageData(final);
                    MorfologiaCinza.Abertura(img7, trackBar1.Value);
                    final = img7.getBitmap();
                    break;
                case "Fechamento (Cinza)":
                    ImageData img8 = new ImageData(final);
                    MorfologiaCinza.Fechamento(img8, trackBar1.Value);
                    final = img8.getBitmap();
                    break;
                default:
                    break;
            }
            
            novaImagem.Image = (Image) final;
            Imagem = final;
            flowLayoutPanel1.Controls.Add(novaImagem);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            ImageData nvimg = new ImageData((Bitmap)Imagem); 
            PictureBox novaImagem = new PictureBox();
            novaImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            novaImagem.Height = 500;
            novaImagem.Width = 410;
            Imagem = (Image)nvimg.getBitmapGray();
            label4.Text = "Escala de Cinza: SIM";
            novaImagem.Image = Imagem;            
            flowLayoutPanel1.Controls.Add(novaImagem);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            
            flowLayoutPanel1.Controls.Clear();
            ImageData nvimg = new ImageData((Bitmap)Imagem);
            PictureBox novaImagem = new PictureBox();
            novaImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            novaImagem.Height = 500;
            novaImagem.Width = 410;
            paraPB pb = new paraPB();
            delPassData del = new delPassData(pb.funData);
            del(this.Imagem);
            pb.Show();
            this.Hide();
            label3.Text = "Binária: SIM";
            label4.Text = "Escala de Cinza: SIM";
            novaImagem.Image = Imagem;
            flowLayoutPanel1.Controls.Add(novaImagem);
            
        }
        public void funData(Image i)
        {
            Imagem = i;
            flowLayoutPanel1.Controls.Clear();
            PictureBox novaImagem = new PictureBox();
            novaImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            novaImagem.Height = 500;
            novaImagem.Width = 410;
            novaImagem.Image = i;
            button2.Show();
            label2.Show();
            label9.Show();
            trackBar1.Show();
            comboBox1.Show();
            groupBox1.Show();
            label3.Text = "Binária: SIM";
            label4.Text = "Escala de Cinza: SIM";
            flowLayoutPanel1.Controls.Add(novaImagem);
        }
        private void flowLayoutPanel1_Paint(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public Image Imagem { get; set; }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar1.Value.ToString() + " x " + trackBar1.Value.ToString();
        }

        private void janela_Fecha(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            PictureBox novaImagem = new PictureBox();
            novaImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            novaImagem.Height = 500;
            novaImagem.Width = 410;
            String opcaoProc = comboBox1.Text;
            Bitmap final, imgpr;
            imgpr = new Bitmap(Imagem);
            final = new Bitmap(Imagem);
            ImageData pct = new ImageData(final);
            label8.Text = "Porcentagem de preto: " + Math.Round(Estatistica.PorcentagemPretos(pct), 3).ToString() + "%";
            ImageData img6 = new ImageData(final);
            Morfologia.Inverte(img6);
            final = img6.getBitmap();
            novaImagem.Image = (Image)final;
            Imagem = final;
            flowLayoutPanel1.Controls.Add(novaImagem);
        }
    }
}
