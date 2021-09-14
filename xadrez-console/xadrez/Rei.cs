using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private readonly PartidaXadrez Partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaXadrez partida) : base(tab, cor)
        {
            Partida = partida;
        }

        //Verifica se a peça pode se mover para a posiçao passada como parametro (retorna true se a posiçao estiver livre ou se tiver uma peça adversaria)
        private bool PodeMover(Posicao pos)
        {
            return Tab.Peca(pos) == null || Tab.Peca(pos).Cor != Cor;
        }

        //Verifica se a torre pode executar o movimento roque com o rei
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0;
        }

        //Gera a matriz de movimentos possiveis do rei (rei pode se mover para todos os lados apenas uma casa)
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            //Norte
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Sul
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Leste
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Oeste
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Nordeste
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Sudeste
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Noroeste
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Sudoeste
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            //Jogada especial: Roque
            if (QtdMovimentos == 0 && !Partida.Xeque)
            {
                //Roque pequeno
                Posicao posTorre1 = new(Posicao.Linha, Posicao.Coluna + 3);

                if (TesteTorreParaRoque(posTorre1))
                {
                    Posicao p1 = new(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                //Roque grande
                Posicao posTorre2 = new(Posicao.Linha, Posicao.Coluna - 4);

                if (TesteTorreParaRoque(posTorre2))
                {
                    Posicao p1 = new(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return matriz;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
