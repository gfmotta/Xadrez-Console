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
                Tabuleiro tab = new(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));

                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(6, 4));

                Tela.ImprimirTabuleiro(tab);

                Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
