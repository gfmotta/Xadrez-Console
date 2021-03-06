using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaXadrez Partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaXadrez partida) : base(tab, cor)
        {
            Partida = partida;
        }

        //Verifica se existe uma peça adversaria na posição passada como parametro
        private bool ExisteInimigo(Posicao pos)
        {
            return Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor;
        }

        //Verifica se a posição passada como parametro esta livre
        private bool Livre(Posicao pos)
        {
            return Tab.Peca(pos) == null;
        }

        //Gera a matriz de movimentos possiveis do peao (peao pode se mover duas casas para frente apenas em seu primeiro movimento e uma casa para frente no restante da partida)
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            if (Cor == Cor.Branca)
            {
                //Em seu primeiro movimento, o Peao pode andar duas casas
                pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna);
                Posicao pos2 = new(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos2) && Livre(pos2) && Tab.PosicaoValida(pos) && Livre(pos) && QtdMovimentos == 0)
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1); //Peao só pode comer a peça inimiga na diagonal
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1); //Peao só pode comer a peça inimiga na diagonal
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                //Jogada especial: En Passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                //Em seu primeiro movimento, o Peao pode andar duas casas
                pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna);
                Posicao pos2 = new(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos2) && Livre(pos2) && Tab.PosicaoValida(pos) && Livre(pos) && QtdMovimentos == 0)
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1); //Peao só pode comer a peça inimiga na diagonal
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1); //Peao só pode comer a peça inimiga na diagonal
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                //Jogada especial: En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
