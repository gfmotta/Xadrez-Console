﻿using System.Collections.Generic;
using tabuleiro;
using tabuleiro.exceçao;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool PartidaTerminada { get; private set; }
        HashSet<Peca> Pecas;
        HashSet<Peca> Capturadas;

        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Pecas = new();
            Capturadas = new();
            PosicionarPecas();
            PartidaTerminada = false;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        public void PosicionarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));

            ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
            ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);

            if (Tab.Peca(destino) != null)
            {
                Peca pecaCapturada = Tab.RetirarPeca(destino);
                Capturadas.Add(pecaCapturada);
            }

            Tab.ColocarPeca(p, destino);
            p.IncrementarQtdMovimentos();
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) //Metodo que retorna as peças capturadas da mesma cor passada como parametro.
        {
            HashSet<Peca> aux = new();

            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor) //Metodo que retora as peças da mesma cor passada como parametro que ainda estão em jogo.
        {
            HashSet<Peca> aux = new();

            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            Tab.ValidarPosicao(origem);

            if (Tab.Peca(origem) == null)
            {
                throw new TabuleiroException("Não existe nenhuma peça na posição selecionada! Por favor, escolha outra posição.");
            }
            if (Tab.Peca(origem).Cor != JogadorAtual)
            {
                throw new TabuleiroException("A peça escolhida não é sua! Por favor, escolha outra peça.");
            }
            if (!Tab.Peca(origem).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Essa peça esta impossibilitada de se mover! Por favor, escolha outra peça.");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            Tab.ValidarPosicao(destino);

            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalida! Por favor, escolha outra posição.");
            }
        }

        public void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
    }
}
