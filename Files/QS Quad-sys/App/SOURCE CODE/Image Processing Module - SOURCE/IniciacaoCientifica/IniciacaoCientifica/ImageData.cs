using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

public enum imageType { Indefinido, RGB, Gray, Binary };
namespace IniciacaoCientifica
{
    [Serializable]
    public class ImageData
    {
        //dados da imagem
        private byte[][,] imageData;//cria um vetor [] de matrizes [,]
        private int height, width, channels;
        private imageType tipo;
        //dados do contorno
        private double[,] Contorno;
        private int Cpontos, Ccoord;

        //INíCIO: como dublicar um objeto: Clone (deep copy)
        #region ICloneable Members
        public object Clone()
        {
            object clone;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                // Serialize this object             
                formatter.Serialize(stream, this);
                stream.Position = 0;
                // Deserialize to another object             
                clone = formatter.Deserialize(stream);
            }
            return clone;
        }
        #endregion
        //FIM: como dublicar um objeto: Clone (deep copy)

        public bool removerCanal(int canal)
        {
            if (canal < channels)
            {
                for (int i = canal; i < channels - 1; i++)
                    imageData[i] = imageData[i + 1];

                imageData[channels - 1] = null;
                this.channels--;
                this.tipo = imageType.Indefinido;
                return true;
            }
            else
                return false;
        }

        private void verificaBinary()
        {
            bool b = true;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (imageData[0][i, j] != 0 && imageData[0][i, j] != 255)
                    {
                        b = false;
                        break;
                    }
                }
            }
            if (b == true)
                this.Type = imageType.Binary;

        }

        private bool verificaGray()
        {
            bool b = true;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //if ((imageData[0][i,j] != imageData[1][i,j]) && (imageData[1][i,j] != imageData[2][i,j]) && (imageData[0][i,j] != imageData[2][i,j]))
                    if ((imageData[0][i, j] != imageData[1][i, j]) || (imageData[1][i, j] != imageData[2][i, j]) || (imageData[0][i, j] != imageData[2][i, j]))
                    {
                        b = false;
                        break;
                    }
                }
                if (b == false)
                    break;
            }
            if (b == true)
            {
                this.tipo = imageType.Gray;
                this.channels = 1;
                imageData[1] = null;
                imageData[2] = null;
            }
            return b;
        }

        public int getAltura()
        {
            return this.height;
        }
        public int getLargura()
        {
            return this.width;
        }

        public byte getPixel(int linha, int coluna, int canal)
        {
            return this.imageData[canal][linha, coluna];
        }

        public void setPixel(int linha, int coluna, int canal, byte valor)
        {
            this.imageData[canal][linha, coluna] = valor;
        }

        public imageType Type
        {
            get
            {
                return this.tipo;
            }
            set
            {
                tipo = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Channels
        {
            get
            {
                return this.channels;
            }
        }

        public ImageData(string path)//carrega contorno - INCOMPLETA AINDA
        {
            FileStream file = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            string line;
            line = sr.ReadLine();
            //retorna ao começo
            sr.DiscardBufferedData();
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            sr.BaseStream.Position = 0;

            //line.

            sr.Close();
            file.Close();

        }

        public ImageData(Bitmap bmp)//carrega imagem
        {
            //Bitmap bmp = new Bitmap(path);
            this.tipo = imageType.RGB;
            this.height = bmp.Height;
            this.width = bmp.Width;
            this.channels = 3;
            this.imageData = new byte[3][,];
            for (int i = 0; i < 3; i++)
                this.imageData[i] = new byte[bmp.Height, bmp.Width];
            BitmapData bitmapData1 = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        imageData[2][i, j] = (byte)imagePointer1[0];//Canal B
                        imageData[1][i, j] = (byte)imagePointer1[1];//Canal G
                        imageData[0][i, j] = (byte)imagePointer1[2];//Canal R
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }//end for i
            }//end unsafe
            bmp.UnlockBits(bitmapData1);

            if (verificaGray())
                verificaBinary();

        }

        public Bitmap getBitmap()
        {
            switch (this.tipo)
            {
                case imageType.RGB: return getBitmapRGB();
                case imageType.Gray: return getBitmapGray();
                case imageType.Binary: return getBitmapGray();
                default: return getBitmapRGB();
            }

        }


        ~ImageData()
        {
            this.imageData = null;
            this.height = 0;
            this.width = 0;
            this.channels = 0;
        }

        private Bitmap getBitmapRGB()
        {
            Bitmap returnMap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < this.height; i++)
                {
                    for (int j = 0; j < this.width; j++)
                    {
                        // write the logic implementation here
                        imagePointer2[0] = (byte)imageData[2][i, j];//B
                        imagePointer2[1] = (byte)imageData[1][i, j];//G
                        imagePointer2[2] = (byte)imageData[0][i, j];//R
                        imagePointer2[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer2 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer2 += bitmapData2.Stride - (bitmapData2.Width * 4);
                }//end for i
            }//end unsafe
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }

        public Bitmap getBitmapGray()
        {
            Bitmap returnMap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < this.height; i++)
                {
                    for (int j = 0; j < this.width; j++)
                    {
                        // write the logic implementation here
                        imagePointer2[0] = (byte)imageData[0][i, j];
                        imagePointer2[1] = (byte)imageData[0][i, j];
                        imagePointer2[2] = (byte)imageData[0][i, j];
                        imagePointer2[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer2 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer2 += bitmapData2.Stride - (bitmapData2.Width * 4);
                }//end for i
            }//end unsafe
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }
        public Bitmap getBitmapBinary()
        {
            Bitmap returnMap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            ImageData im = new ImageData(returnMap);
            returnMap = im.getBitmapGray();
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < this.height; i++)
                {
                    for (int j = 0; j < this.width; j++)
                    {
                        // write the logic implementation here
                        if((byte)imageData[0][i, j]>128)
                            imagePointer2[0] = (byte)255;
                        else
                            imagePointer2[0] = (byte)0;
                        imagePointer2[1] = (byte)imageData[0][i, j];
                        imagePointer2[2] = (byte)imageData[0][i, j];
                        imagePointer2[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer2 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer2 += bitmapData2.Stride - (bitmapData2.Width * 4);
                }//end for i
            }//end unsafe
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }
        public Bitmap getBitmapBinary(int level)
        {
            Bitmap returnMap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            ImageData im = new ImageData(returnMap);
            returnMap = im.getBitmapGray();
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0, returnMap.Width, returnMap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < this.height; i++)
                {
                    for (int j = 0; j < this.width; j++)
                    {
                        // write the logic implementation here
                        if ((byte)imageData[0][i, j] > level)
                        {
                            imagePointer2[0] = (byte)255;
                            imagePointer2[1] = (byte)255;
                            imagePointer2[2] = (byte)255;
                        }
                        else
                        {
                            imagePointer2[0] = (byte)0;
                            imagePointer2[1] = (byte)0;
                            imagePointer2[2] = (byte)0;
                        }
                        
                        imagePointer2[3] = (byte)255;
                        //4 bytes per pixel
                        imagePointer2 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer2 += bitmapData2.Stride - (bitmapData2.Width * 4);
                }//end for i
            }//end unsafe
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }
    }
}