using tabuleiro.exceçao;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        //Permite acessar uma peça da matriz de peças passando como parametro uma linha e uma coluna
        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        //Permite acessar uma peça da matriz de peças passando como parametro uma posiçao
        public Peca Peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        //Verifica se ja existe uma peça na posição passada como parametro
        private bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        //Coloca uma peça no tabuleiro
        public void ColocarPeca(Peca p, Posicao pos) 
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Ja existe uma peça nessa posição!");
            }

            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        //Retira uma peça do tabuleiro
        public Peca RetirarPeca(Posicao pos)
        {
            if (Peca(pos) == null)
            {
                return null;
            }

            Peca aux = Peca(pos);
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        //Valida uma posiçao
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição invalida!");
            }
        }

        //Verifica se a posiçao passada como parametro nao ultrapassa as linhas e colunas do tabuleiro
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }

            return true;
        }
    }
}
