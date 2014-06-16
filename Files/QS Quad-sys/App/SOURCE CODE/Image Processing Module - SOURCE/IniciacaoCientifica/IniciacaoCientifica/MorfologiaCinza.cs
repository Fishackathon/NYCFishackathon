using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IniciacaoCientifica
{
    public static class MorfologiaCinza
    {
        public static void DilataImg(ImageData imagem, byte[,] mascara)
        {
            if (imagem.Type == imageType.Gray)
            {
                ImageData referencia = (ImageData)imagem.Clone();
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                int linhaMascara = mascara.GetLength(0);
                int colunaMascara = mascara.GetLength(1);
                int centroMascaraAltura = linhaMascara / 2;
                int centroMascaraLargura = colunaMascara / 2;
                int px, py, maior, valor;
                int num = mascara[0, 0];
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        {
                            maior = 0;
                            for (int k = 0; k < linhaMascara; k++)
                            {
                                for (int l = 0; l < colunaMascara; l++)
                                {
                                    py = i - centroMascaraAltura + k;
                                    px = j - centroMascaraLargura + l;
                                    if (py >= 0 && py < altura && px >= 0 && px < largura)
                                    {
                                        valor = referencia.getPixel(px, py, 0) + mascara[k, l];
                                        if (valor > maior)
                                            maior = valor;
                                    }
                                }
                            }
                            if (maior > 255)
                                maior = 255;
                            if (maior < 0)
                                maior = 0;
                            imagem.setPixel(j,i, 0, (byte)maior);
                        }
                    }
                }
                referencia = null;
            }
        }
        public static void DilataImg(ImageData imagem)
        {
            byte valor = 10;
            byte valor2 = 0;
            byte[,] mascara = new byte[3, 3] {
                                                {valor2,valor,valor2},
												{valor,valor,valor},
												{valor2,valor,valor2},
											  };
            DilataImg(imagem, mascara);
        }
        public static void DilataImg(ImageData imagem, int t)
        {
            int valor = 10;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte) valor;
            DilataImg(imagem, mascara);
        }
        public static void ErodeImg(ImageData imagem, byte[,] mascara)
        {
            if (imagem.Type == imageType.Gray)
            {
                ImageData referencia = (ImageData)imagem.Clone();
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                int linhaMascara = mascara.GetLength(0);
                int colunaMascara = mascara.GetLength(1);
                int centroMascaraAltura = linhaMascara / 2;
                int centroMascaraLargura = colunaMascara / 2;
                int px, py, menor = 255, valor;
                int num = mascara[0, 0];
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        {
                            menor = 255;
                            for (int k = 0; k < linhaMascara; k++)
                            {
                                for (int l = 0; l < colunaMascara; l++)
                                {
                                    py = i - centroMascaraAltura + k;
                                    px = j - centroMascaraLargura + l;
                                    if (py >= 0 && py < altura && px >= 0 && px < largura)
                                    {
                                        valor = referencia.getPixel(px, py, 0) - mascara[k, l];
                                        if (valor < menor)
                                            menor = valor;
                                    }
                                }
                            }
                            if (menor > 255)
                                menor = 255;
                            if (menor < 0)
                                menor = 0;
                            imagem.setPixel(j, i, 0, (byte)menor);
                        }
                    }
                }
                referencia = null;
            }
        }
        public static void ErodeImg(ImageData imagem)
        {
            byte valor = 10;
            byte valor2 = 0;
            byte[,] mascara = new byte[3, 3] {
                                                {valor2,valor,valor2},
												{valor,valor,valor},
												{valor2,valor,valor2},
											  };
            ErodeImg(imagem, mascara);
        }
        public static void ErodeImg(ImageData imagem, int t)
        {
            int valor = 10;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte)valor;
            ErodeImg(imagem, mascara);
        }
        public static void Abertura(ImageData imagem, byte[,] mascara)
        {
            ErodeImg(imagem, mascara);
            DilataImg(imagem, mascara);
        }
        public static void Abertura(ImageData imagem)
        {
            ErodeImg(imagem);
            DilataImg(imagem);
        }
        public static void Abertura(ImageData imagem, int t)
        {
            int valor = 10;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte)valor;
            ErodeImg(imagem, mascara);
            DilataImg(imagem, mascara);
        }
        public static void Fechamento(ImageData imagem, byte[,] mascara)
        {
            DilataImg(imagem, mascara);
            ErodeImg(imagem, mascara);
        }
        public static void Fechamento(ImageData imagem)
        {
            DilataImg(imagem);
            ErodeImg(imagem);
        }
        public static void Fechamento(ImageData imagem, int t)
        {
            int valor = 10;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte)valor;
            DilataImg(imagem, mascara);
            ErodeImg(imagem, mascara);
        }
    }
}
