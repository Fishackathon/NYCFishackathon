using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IniciacaoCientifica
{
    public static class Estatistica
    {
        public static float PorcentagemPretos(ImageData imagem)
        {
            if (imagem.Type == imageType.Binary)
            {
                float numeroPretos = 0;
                float altura = imagem.getAltura();
                float largura = imagem.getLargura();
                for (int i = 0; i < altura; i++)
                {
                    for (int j = 0; j < largura; j++)
                    {
                        if (imagem.getPixel(i, j, 0) == 0)
                            numeroPretos++;
                    }
                }
                return ((numeroPretos * 100) / (altura * largura));
            }
            return 0;
        }
    }
}
