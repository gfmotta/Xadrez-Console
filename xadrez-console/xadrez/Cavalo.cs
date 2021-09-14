using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        //Verifica se a peça pode se mover para a posiçao passada como parametro (retorna true se a posiçao estiver livre ou se tiver uma peça adversaria)
        private bool PodeMover(Posicao pos)
        {
            return Tab.Peca(pos) == null || Tab.Peca(pos).Cor != Cor;
        }

        //Gera a matriz de movimentos possiveis do cavalo (cavalo se movimenta em formato de L)
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            return matriz;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
