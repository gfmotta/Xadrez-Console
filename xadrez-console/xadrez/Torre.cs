using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public bool PodeMover(Posicao pos) //Verifica se a peça pode se mover para tal posição
        {
            return Tab.Peca(pos) == null || Tab.Peca(pos).Cor != Cor;
        }

        public override bool[,] MovimentosPosiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            //Verifica se a posição norte é valida
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.Linha--;
            }

            //Verifica se a posição leste é valida
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.Coluna++;
            }

            //Verifica se a posição sul é valida
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.Linha++;
            }

            //Verifica se a posição oeste é valida
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }

                pos.Coluna--;
            }

            return matriz;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
