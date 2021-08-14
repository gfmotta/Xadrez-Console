using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab) //metodo para imprimir um tabuleiro na tela
        {
            for (int i = 0; i < tab.Linhas; i++) //percorre as linhas do tabuleiro
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < tab.Colunas; j++) //percorre as colunas do tabuleiro
                {
                    if (tab.Peca(i, j) == null) //verifica se existe uma peça na posição i, j da matriz de peças
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ColocarPeca(tab.Peca(i, j));
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string p = Console.ReadLine().Replace(" ", "").ToLower();
            char coluna = p[0];
            int linha = int.Parse($"{p[1]}");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ColocarPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write($"{peca} ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{peca} ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
