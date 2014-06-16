using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace IniciacaoCientifica
{
    public static class Morfologia
    {
       public static void DilataImg(ImageData imagem, byte[,] mascara)
        {
            //recebe imagem e máscara
            if (imagem.Type == imageType.Binary)
                /* Verifica se a imagem é binária. esta operação só funciona em imagens binárias.
                * O enum imageType faz parte da classe ImageData */
            {
                ImageData referencia = (ImageData)imagem.Clone();
                /* Cria uma cópia da imagem para não danificar a imagem 
                 * final e obter resultado diferente do esperado*/
                int altura = imagem.getAltura();
                // lê a altura da imagem original
                int largura = imagem.getLargura();
                // le a altura da imagem original
                int linhaMascara = mascara.GetLength(0);
                /* o método GetLength retorna o tamanho do vetor da dimensão especificada.
                 * No caso em que especifica-se zero, quer-se saber a quantidade de linhas por exemplo
                 * No caso que especifica-se um, o número de colunas.*/
                int colunaMascara = mascara.GetLength(1);
                int centroMascaraAltura = linhaMascara/2;
                // Calcula o centro X da máscara arredondando pra baixo.
                int centroMascaraLargura = colunaMascara/2;
                // Calcula o centro Y da máscara arredondando pra baixo.
                int px, py;
                double pz;
                //Variáveis inicializadas fora do FOR para evitar alocação e desalocação frequente

                for (int i = 0; i < altura; i++)
                {
                    // Percorre a imagem por linhas
                    for (int j = 0; j < largura; j++)
                    {
                        // Percorre as colunas de cada linha
                        pz = referencia.getPixel(i, j, 0);
                        // Recupera o valor do pixel na camada 0 e associa a pz
                        // Se pz for PRETO, ou seja, OBJETO, processe a máscara
                        if (pz == 0)
                        {
                            for (int k = 0; k < linhaMascara; k++)
                            {
                                // Percorra as linhas da máscara
                                for (int l = 0; l < colunaMascara; l++)
                                {
                                    // Percorra as colunas da máscara
                                    py = i - centroMascaraAltura + k;
                                    /* PY é o eixo Y que estamos percorrendo na máscara.
                                     * Ele é obtido diminuindo um dos eixos do ponto analisado na imagem original
                                     * e somando ao índice K que é o eixo na máscara*/
                                    px = j - centroMascaraLargura + l;
                                    // Ocorre de forma semelhante em PX
                                    if (py >= 0 && py < altura && px >= 0 && px < largura)
                                    // Verifica se o pixel existe
                                    {
                                        // Caso existir, use o método setPixel para alterar o valor para 255
                                        imagem.setPixel(py, px, 0, 0);
                                    }

                                }
                            }
                        }
                    }
                }
                // Desaloque sua imagem de referência.
                // O Garbage Collector já fará isso, mas é melhor fazer por segurança.
                referencia = null;
            }
        }
        public static void DilataImg(ImageData imagem)
        {
            byte[,] mascara = new byte[3, 3] {
                                                {1,1,1},
												{1,1,1},
												{1,1,1},
											  };
            DilataImg(imagem, mascara);
        }
        public static void DilataImg(ImageData imagem, int t)
        {
            int valor = 1;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte)valor;
            DilataImg(imagem, mascara);
        }
        public static void ErodeImg(ImageData imagem, byte[,] mascara)
        {
            if (imagem.Type == imageType.Binary)
            {
                ImageData referencia = (ImageData)imagem.Clone();
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                int linhaMascara = mascara.GetLength(0);
                int colunaMascara = mascara.GetLength(1);
                int centroMascaraAltura = linhaMascara / 2;
                int centroMascaraLargura = colunaMascara / 2;
                int px, py;
                double pz;
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        pz = referencia.getPixel(i, j, 0);
                        if (pz == 0)
                        {
                            for (int k = 0; k < linhaMascara; k++)
                            {
                                for (int l = 0; l < colunaMascara; l++)
                                {
                                    py = i - centroMascaraAltura + k;
                                    px = j - centroMascaraLargura + l;
                                    if (py >= 0 && py < altura && px >= 0 && px < largura)
                                    {

                                        if (referencia.getPixel(py, px, 0) == 255)
                                        {
                                            imagem.setPixel(i, j, 0, 255);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                referencia = null;
            }
        }
        public static void ErodeImg(ImageData imagem)
        {
            byte[,] mascara = new byte[3, 3] {
                                                {1,1,1},
												{1,1,1},
												{1,1,1},
											  };
            ErodeImg(imagem, mascara);
        }
        public static void ErodeImg(ImageData imagem, int t)
        {
            int valor = 1;
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
            int valor = 1;
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
            int valor = 1;
            byte[,] mascara = new byte[t, t];
            for (int i = 0; i < t; i++)
                for (int j = 0; j < t; j++)
                    mascara[i, j] = (byte)valor;
            DilataImg(imagem, mascara);
            ErodeImg(imagem, mascara);
        }
        public static void Diferenca(ImageData imagem, ImageData imagem2)
        {
            bool mesmoTamanho = imagem.getAltura() == imagem2.getAltura() && imagem.getLargura() == imagem2.getLargura();

            if (imagem2.Type == imageType.Binary && imagem.Type == imageType.Binary && mesmoTamanho)
            {
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        if (imagem.getPixel(i, j, 0) == 0 && imagem2.getPixel(i, j, 0) == 0)
                            imagem.setPixel(i, j, 0, 255);
                    }
                }
            }
        }
        public static void Uniao(ImageData imagem, ImageData imagem2)
        {
            bool mesmoTamanho = imagem.getAltura() == imagem2.getAltura() && imagem.getLargura() == imagem2.getLargura();

            if (imagem2.Type == imageType.Binary && imagem.Type == imageType.Binary && mesmoTamanho)
            {
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        if (imagem.getPixel(i, j, 0) == 0 || imagem2.getPixel(i, j, 0) == 0)
                            imagem.setPixel(i, j, 0, 0);
                    }
                }
            }
        }
        public static void Inverte(ImageData imagem)
        {
            if (imagem.Type == imageType.Binary)
            {
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        if (imagem.getPixel(i, j, 0) == 0)
                            imagem.setPixel(i, j, 0, 255);

                        else if (imagem.getPixel(i, j, 0) == 255)
                            imagem.setPixel(i, j, 0, 0);
                    }
                }
            }
        }
        public static void BordaExterna(ImageData imagem)
        {
            ImageData imagem2 = (ImageData)imagem.Clone();
            Morfologia.DilataImg(imagem);
            Morfologia.Diferenca(imagem, imagem2);
        }
        public static void BordaInterna(ImageData imagem)
        {
            ImageData imagem2 = (ImageData)imagem.Clone();
            Morfologia.ErodeImg(imagem2);
            Morfologia.Diferenca(imagem, imagem2);
        }
        public static void Borda(ImageData imagem)
        {
            byte[,] mascara = new byte[3, 3] {
                                                {1,1,1},
												{1,1,1},
												{1,1,1},
											  };
            if (imagem.Type == imageType.Binary)
            {
                ImageData referencia = (ImageData)imagem.Clone();
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                int linhaMascara = mascara.GetLength(0);
                int colunaMascara = mascara.GetLength(1);
                int centroMascaraAltura = linhaMascara / 2;
                int centroMascaraLargura = colunaMascara / 2;
                int px, py;
                double pz;
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        pz = referencia.getPixel(i, j, 0);
                        imagem.setPixel(i, j, 0, 255);
                        if (pz == 0)
                        {
                            for (int k = 0; k < linhaMascara; k++)
                            {
                                for (int l = 0; l < colunaMascara; l++)
                                {
                                    py = i - centroMascaraAltura + k;
                                    px = j - centroMascaraLargura + l;
                                    if (py >= 0 && py < altura && px >= 0 && px < largura)
                                    {
                                        if (referencia.getPixel(py, px, 0) == 255)
                                            imagem.setPixel(i, j, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                }
                referencia = null;
            }
        }
        public static void RemoveBorda(ImageData imagem)
        {
            byte[,] mascara = new byte[3, 3] {
                                                {1,1,1},
												{1,1,1},
												{1,1,1},
            };
            RemoveBorda(imagem, mascara);
        }
        public static void RemoveBorda(ImageData imagem, byte[,] mascara)
        {
            
            //recebe imagem e máscara
            if (imagem.Type == imageType.Binary)
            /* Verifica se a imagem é binária. esta operação só funciona em imagens binárias.
            * O enum imageType faz parte da classe ImageData */
            {
                int altura = imagem.getAltura();
                // lê a altura da imagem original
                int largura = imagem.getLargura();
                // le a altura da imagem original
                int linhaMascara = mascara.GetLength(0);
                /* o método GetLength retorna o tamanho do vetor da dimensão especificada.
                 * No caso em que especifica-se zero, quer-se saber a quantidade de linhas por exemplo
                 * No caso que especifica-se um, o número de colunas.*/
                int colunaMascara = mascara.GetLength(1);
                int centroMascaraAltura = linhaMascara / 2;
                // Calcula o centro X da máscara arredondando pra baixo.
                int centroMascaraLargura = colunaMascara / 2;
                // Calcula o centro Y da máscara arredondando pra baixo.
                int px, py;
                double pz;
                //Inicializar as bordas da imagem com o número 2.
                for (int i = 0; i < altura; i++)
                {
                    if (imagem.getPixel(i, 0, 0) == 0)
                    imagem.setPixel(i, 0, 0, 2);
                }
                for (int j = 0; j < largura; j++)
                {
                    if(imagem.getPixel(0,j,0)==0)
                    imagem.setPixel(0, j, 0, 2);
                }
                for (int i = 0; i < altura; i++)
                {
                    if (imagem.getPixel(i, largura-1, 0) == 0)
                        imagem.setPixel(i, largura-1, 0, 2);
                }
                for (int j = 0; j < largura; j++)
                {
                    if (imagem.getPixel(altura-1, j, 0) == 0)
                        imagem.setPixel(altura-1, j, 0, 2);
                }
                //Variáveis inicializadas fora do FOR para evitar alocação e desalocação frequente
                for (int i = 0; i < altura; i++)
                {
                    // Percorre a imagem por linhas
                    for (int j = 0; j < largura; j++)
                    {
                        // Percorre as colunas de cada linha
                        pz = imagem.getPixel(i, j, 0);
                        // Recupera o valor do pixel na camada 0 e associa a pz
                        // Se pz for PRETO, ou seja, OBJETO, processe a máscara
                        if (pz >0 && pz != 255)
                        {
                            for (int k = 0; k < linhaMascara; k++)
                            {
                                // Percorra as linhas da máscara
                                for (int l = 0; l < colunaMascara; l++)
                                {
                                    // Percorra as colunas da máscara
                                    py = i - centroMascaraAltura + k;
                                    /* PY é o eixo Y que estamos percorrendo na máscara.
                                     * Ele é obtido diminuindo um dos eixos do ponto analisado na imagem original
                                     * e somando ao índice K que é o eixo na máscara*/
                                    px = j - centroMascaraLargura + l;
                                    // Ocorre de forma semelhante em PX
                                    if (py >= 0 && py < altura && px >= 0 && px < largura)
                                    // Verifica se o pixel existe
                                    {
                                        if (imagem.getPixel(py, px, 0) == 0)
                                        {
                                            //MessageBox.Show("pixel era " + imagem.getPixel(py, px, 0));
                                            byte b = (byte)(imagem.getPixel(py, px, 0) + 1);
                                            if (b > 255)
                                                b = 244;
                                            imagem.setPixel(py, px, 0, b);
                                            //MessageBox.Show("pixel eh " + imagem.getPixel(py, px, 0));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
               // MessageBox.Show("passei pelo loop" +
                 //   imagem.getPixel(1, 2, 0) + " " +
                  //  imagem.getPixel(1, 0, 0) + " " +
                  //  imagem.getPixel(5, 5, 0));
                for (int i = 0; i < altura; i++)
                {
                    // Percorre a imagem por linhas
                    for (int j = 0; j < largura; j++)
                    {
                        if (imagem.getPixel(i, j, 0) > 0 && imagem.getPixel(i, j, 0) < 254)
                        {
                            //MessageBox.Show("mudei um pixel de cor");
                            imagem.setPixel(i, j, 0, 255);
                        }
                    }
                }
                //MessageBox.Show("colori os pontos" +
                //    imagem.getPixel(1, 2, 0) + " " +
                //    imagem.getPixel(1, 0, 0) + " " +
                //    imagem.getPixel(5, 5, 0));
            }
        }
    }
}