using System;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class GraxeiException : Exception
    {
        public GraxeiException(string mensagem) : base(mensagem)
        {
        }
    }
}
