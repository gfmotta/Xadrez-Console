using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab) //metodo para imprimir um tabuleiro na tela
        {
            for (int i = 0; i < tab.Linhas; i++) //percorre as linhas do tabuleiro
            {
                for (int j = 0; j < tab.Colunas; j++) //percorre as colunas do tabuleiro
                {
                    if (tab.Peca(i, j) == null) //verifica se existe uma peça na posição i, j
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{tab.Peca(i, j)} ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
