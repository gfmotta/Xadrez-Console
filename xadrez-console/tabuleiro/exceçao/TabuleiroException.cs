using System;

namespace tabuleiro.exceçao
{
    class TabuleiroException : Exception
    {
        public TabuleiroException(string msg) : base(msg)
        {
        }
    }
}
