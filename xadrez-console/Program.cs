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
                PartidaXadrez partida = new();

                while (!partida.PartidaTerminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab);

                        Console.WriteLine(Environment.NewLine + $"Turno: {partida.Turno}");
                        Console.WriteLine($"Jogador atual: {partida.JogadorAtual}");
                        Console.WriteLine("Aguardando jogada...");

                        Console.Write(Environment.NewLine + "Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, partida.Tab.Peca(origem).MovimentosPosiveis());

                        Console.Write(Environment.NewLine + "Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizarJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
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
