using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IniciacaoCientifica
{
    public static class Esqueleto
    {
        private static int transicoes(ImageData temp, int i, int j, int[,] vetor)
        {
            int vizinhos = 0;
            for (int k = 0; k < 7; k++)
            {
                if (temp.getPixel(i + vetor[k, 0], j + vetor[k, 1], 0) == (byte)0 && temp.getPixel(i + vetor[k + 1, 0], j + vetor[k + 1, 1], 0) == (byte)255)
                    vizinhos++;
            }
            if (temp.getPixel(i - 1, j - 1, 0) == 0 && temp.getPixel(i - 1, j, 0) == 255)
                vizinhos++;
            return vizinhos;
        }

        private static int nro_viz(ImageData temp, int i, int j, int[,] vetor)
        {
            int vizinhos = 0;
            for (int k = 0; k < 8; k++)
            {
                if (temp.getPixel(i + vetor[k, 0], j + vetor[k, 1], 0) == (byte) 0)
                    vizinhos++;
            }
            return vizinhos;
        }

        public static void Processa(ImageData imagem)
        {
            if (imagem.Type == imageType.Binary)
            {
                int iteracao = 0;
                ImageData temp = (ImageData)imagem.Clone();
                int altura = imagem.getAltura();
                int largura = imagem.getLargura();
                int vizinhos = 0;
                int condicoes = 0;
                bool operou = true;
                int[,] vetor = new int[8,2] {  { -1,  0 }, 
                                               { -1,  1 },
                                               { 0 ,  1 },
                                               { 1 ,  1 },
                                               { 1 ,  0 },
                                               { 1 , -1 },
                                               { 0 , -1 },
                                               { -1, -1}};
                while (operou)
                {
                    operou = false;
                    for (int i = 1; i < altura - 2; i++)
                    {
                        for (int j = 1; j < largura - 2; j++)
                        {
                            if (temp.getPixel(i, j, 0) == 0)
                            {                                
                                // verifica o numero de vizinhos pretos
                                vizinhos = nro_viz(temp, i, j, vetor);
                                if (vizinhos <= 6 && vizinhos >= 2)
                                {
                                    condicoes++;
                                    //MessageBox.Show("cond2");
                                }

                                //MessageBox.Show(vizinhos.ToString());
                                // verifica a condição de conectividade entre os elementos
                                if (transicoes(temp, i, j, vetor) == 1)
                                {
                                    condicoes++;
                                   // MessageBox.Show("cond1");
                                }                                                                                             

                                // verifica pixel branco nos pontos da máscara
                                if (temp.getPixel(i - 1, j, 0) == 255 || temp.getPixel(i, j + 1, 0) == 255 || temp.getPixel(i + 1, j, 0) == 255)
                                {
                                    condicoes++;
                                    //    MessageBox.Show("cond3");
                                } 

                                // verifica pixel branco nos pontos da máscara
                                if (temp.getPixel(i, j - 1, 0) == 255 || temp.getPixel(i + 1, j, 0) == 255 || temp.getPixel(i, j + 1, 0) == 255)
                                {
                                    condicoes++;
                                    //MessageBox.Show("cond4");
                                }

                                // apaga o pixel se todas as condicoes forem verdadeiras
                                if (condicoes == 4)
                                {
                                    operou = true;
                                    imagem.setPixel(i, j, 0, 255);
                                    //MessageBox.Show("estive aqui");
                                }
                                condicoes = 0;
                                vizinhos = 0;
                            }
                        }
                    }
                    temp = (ImageData)imagem.Clone();
                    for (int i = 1; i < altura - 2; i++)
                    {
                        for (int j = 1; j < largura - 2; j++)
                        {
                            if (temp.getPixel(i, j, 0) == 0)
                            {                                
                                // verifica o numero de vizinhos pretos

                                vizinhos = nro_viz(temp, i, j, vetor);
                                if (vizinhos <= 6 && vizinhos >= 2)
                                {
                                    condicoes++;
                                    //MessageBox.Show("cond2");
                                }

                                // verifica a condição de conectividade entre os elementos
                                //MessageBox.Show(vizinhos.ToString());
                                if (transicoes(temp, i, j, vetor) == 1)
                                {
                                    condicoes++;
                                    // MessageBox.Show("cond1");
                                }

                                // verifica pixel branco nos pontos da máscara
                                if (temp.getPixel(i, j - 1, 0) == 255 || temp.getPixel(i - 1, j, 0) == 255 || temp.getPixel(i, j + 1, 0) == 255)
                                {
                                    condicoes++;
                                    //    MessageBox.Show("cond3");
                                }

                                // verifica pixel branco nos pontos da máscara
                                if (temp.getPixel(i - 1, j, 0) == 255 || temp.getPixel(i, j - 1, 0) == 255 || temp.getPixel(i + 1, j, 0) == 255)
                                {
                                    condicoes++;
                                    //MessageBox.Show("cond4");
                                } 

                                // apaga o pixel se todas as condicoes forem verdadeiras
                                if (condicoes == 4)
                                {
                                    operou = true;
                                    imagem.setPixel(i, j, 0, 255);
                                    //MessageBox.Show("estive aqui");
                                }
                                condicoes = 0;
                                vizinhos = 0;
                            }
                        }
                    }
                    temp = (ImageData)imagem.Clone();
                    iteracao++;
                }
                temp = (ImageData)imagem.Clone();
                temp = null;
            }
        }
    }
}
