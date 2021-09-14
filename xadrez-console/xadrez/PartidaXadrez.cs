using System.Collections.Generic;
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
        public bool Xeque { get; private set; }
        HashSet<Peca> Pecas;
        HashSet<Peca> Capturadas;
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            PartidaTerminada = false;
            Xeque = false;
            Pecas = new();
            Capturadas = new();
            PosicionarPecas();
            VulneravelEnPassant = null;
        }

        //Coloca uma nova peça no jogo
        private void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        //Posiciona as peças no inicio da partida de xadrez
        private void PosicionarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));
        }

        //Retira e coloca uma peça em uma nova posição (utilizado para realizar jogadas)
        private Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            p.IncrementarQtdMovimentos();

            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            //Jogada especial: Roque Pequeno (movimento da torre)
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RetirarPeca(origemT);
                Tab.ColocarPeca(t, destinoT);
                t.IncrementarQtdMovimentos();
            }

            //Jogada especial: Roque Grande (movimento da torre)
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RetirarPeca(origemT);
                Tab.ColocarPeca(t, destinoT);
                t.IncrementarQtdMovimentos();
            }

            //Jogada especial: En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posPeao;
                    if (p.Cor == Cor.Branca)
                    {
                        posPeao = new(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posPeao = new(destino.Linha - 1, destino.Coluna);
                    }

                    pecaCapturada = Tab.RetirarPeca(posPeao);
                    Capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        //Desfaz o movimento se for irregular
        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(p, origem);
            p.DecrementarQtdMovimentos();

            //Jogada especial: Roque Pequeno (movimento da torre)
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RetirarPeca(destinoT);
                Tab.ColocarPeca(t, origemT);
                t.DecrementarQtdMovimentos();
            }

            //Jogada especial: Roque Grande (movimento da torre)
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RetirarPeca(destinoT);
                Tab.ColocarPeca(t, origemT);
                t.DecrementarQtdMovimentos();
            }

            //Jogada especial: En Passant (arrumando a posiçao do peao capturado)
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posPeao;
                    if (p.Cor == Cor.Branca)
                    {
                        posPeao = new(3, destino.Coluna);
                    }
                    else
                    {
                        posPeao = new(4, destino.Coluna);
                    }

                    Tab.ColocarPeca(peao, posPeao);
                }
            }
        }

        //Realiza uma jogada valida
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voçe não pode se colocar em xeque! Por favor, faça outra jogada.");
            }

            //Jogada especial: Promoçao
            Peca p = Tab.Peca(destino);

            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha ==0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tab.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(Tab, p.Cor);
                    Tab.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }

            if (EstaEmXeque(Adversario(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversario(JogadorAtual)))
            {
                PartidaTerminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            //Jogada especial: En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }

        }

        //Retorna as peças capturadas da mesma cor passada como parametro.
        public HashSet<Peca> PecasCapturadas(Cor cor)
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

        //Retora as peças da mesma cor passada como parametro que ainda estão em jogo.
        public HashSet<Peca> PecasEmJogo(Cor cor)
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

        //Metodo auxiliar para identificar a cor adversaria
        private Cor Adversario(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        //Verifica se o Rei ainda esta em jogo
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        //Verifica se o Rei da cor passada como parametro esta em xeque
        private bool EstaEmXeque(Cor cor)
        {
            Peca r = Rei(cor);

            if (r == null)
            {
                throw new TabuleiroException($"Não tem Rei da cor {cor} no tabuleiro!");
            }

            foreach (Peca x in PecasEmJogo(Adversario(cor)))
            {
                bool[,] matriz = x.MovimentosPossiveis();

                if (matriz[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        //Verifica se o Rei da cor passada como parametro esta em xeque mate
        private bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] matriz = x.MovimentosPossiveis();

                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (matriz[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        //Verifica se a posiçao de origem que o usuario digitou é valida
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

        //Verifica se a posiçao de destino que o usuario digitou é valida
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            Tab.ValidarPosicao(destino);

            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino invalida! Por favor, escolha outra posição.");
            }
        }

        private void MudaJogador()
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
