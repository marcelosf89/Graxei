using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ObjetoJaExisteException : Exception
    {
        public ObjetoJaExisteException(string mensagem): base(mensagem)
        {
        }
    }
}
