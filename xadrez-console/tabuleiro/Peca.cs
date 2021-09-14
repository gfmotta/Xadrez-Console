namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; private set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Tab = tab;
            Cor = cor;
            QtdMovimentos = 0;
        }

        //Incrementa a quantidade de movimento da peça
        public void IncrementarQtdMovimentos()
        {
            QtdMovimentos++;
        }

        //Decrementa a quantidade de movimento da peça
        public void DecrementarQtdMovimentos()
        {
            QtdMovimentos--;
        }

        //Verica se existe algum movimento que a peça possa realizar
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();

            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //Verifica se a posiçao passada como parametro é um possivel movimento daquela peça
        public bool MovimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        //Gera uma matriz de movimentos possiveis para cada peça
        public abstract bool[,] MovimentosPossiveis();
    }
}
