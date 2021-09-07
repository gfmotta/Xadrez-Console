using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        private bool ExisteInimigo(Posicao pos) //Verifica se existe uma peça adversaria na posição passada como parametro
        {
            return Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor;
        }

        private bool Livre(Posicao pos) //Verifica se a posição esta livre
        {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            if (Cor == Cor.Branca)
            {
                //Em seu primeiro movimento, o Peao pode andar duas casas, após o primeiro movimento apenas de uma em uma
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
            }
            else
            {
                //Em seu primeiro movimento, o Peao pode andar duas casas, após o primeiro movimento apenas de uma em uma
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
            }

            return matriz;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
