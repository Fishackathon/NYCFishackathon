using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IniciacaoCientifica
{
    public static class MorfologiaColorida
    {
        private static void DilataImg(ImageData imagem, byte[,] mascara, int canal)
        {
            if (imagem.Type == imageType.RGB)
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
                                        valor = referencia.getPixel(px, py, canal) + mascara[k, l];
                                        if (valor > maior)
                                            maior = valor;
                                    }
                                }
                            }
                            if (maior > 255)
                                maior = 255;
                            if (maior < 0)
                                maior = 0;
                            imagem.setPixel(j, i, canal, (byte)maior);
                        }
                    }
                }
                referencia = null;
            }
        }
        public static void DilataImg(ImageData imagem, byte[,] mascara)
        {
            DilataImg(imagem, mascara, 0);
            DilataImg(imagem, mascara, 1);
            DilataImg(imagem, mascara, 2);
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
            DilataImg(imagem, mascara, 0);
            DilataImg(imagem, mascara, 1);
            DilataImg(imagem, mascara, 2);
        }
        public static void DilataImg(ImageData imagem, int t)
        {
            int valor = 10;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte)valor;
            DilataImg(imagem, mascara);
        }
    }
}
