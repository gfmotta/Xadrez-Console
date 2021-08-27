using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);

            ImprimirPecasCapturadas(partida);

            Console.WriteLine(Environment.NewLine + $"Turno: {partida.Turno}");
            Console.WriteLine($"Jogador atual: {partida.JogadorAtual}");
            Console.WriteLine("Aguardando jogada...");

            if (partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine(Environment.NewLine + "Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("(");
            foreach (Peca p in conjunto)
            {
                Console.Write($"{p} ");
            }
            Console.WriteLine(")");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab) //imprime um tabuleiro na tela
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    ColocarPeca(tab.Peca(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] movimentosPossiveis) //imprime um tabuleiro na tela com os possiveis movimentos de uma peça selecionada pelo jogador
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (movimentosPossiveis[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    ColocarPeca(tab.Peca(i, j));
                    Console.BackgroundColor = ConsoleColor.Black;
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
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
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
}
