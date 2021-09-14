using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        //Verifica se a peça pode se mover para a posiçao passada como parametro (retorna true se a posiçao estiver livre ou se tiver uma peça adversaria)
        private bool PodeMover(Posicao pos) 
        {
            return Tab.Peca(pos) == null || Tab.Peca(pos).Cor != Cor;
        }

        //Gera a matriz de movimentos possiveis da dama (dama se movimenta nas diagonais e para todos os lados)
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            //Norte
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha - 1, pos.Coluna);
            }

            //Sul
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha + 1, pos.Coluna);
            }

            //Leste
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha, pos.Coluna + 1);
            }

            //Oeste
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha, pos.Coluna - 1);
            }

            //Nordeste
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha - 1, pos.Coluna + 1);
            }

            //Sudeste
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha + 1, pos.Coluna + 1);
            }

            //Noroeste
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha - 1, pos.Coluna - 1);
            }

            //Sudoeste
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.DefinirPosicao(pos.Linha + 1, pos.Coluna - 1);
            }

            return matriz;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
