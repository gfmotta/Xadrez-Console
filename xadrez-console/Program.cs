using System;
using tabuleiro;
using xadrez;
using tabuleiro.exceçao;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez p = new();

                while (!p.PartidaTerminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(p.Tab);

                    Console.Write(Environment.NewLine + "Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(p.Tab, p.Tab.Peca(origem).MovimentosPosiveis());

                    Console.Write(Environment.NewLine + "Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                    p.ExecutarMovimento(origem, destino);
                }

                Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
