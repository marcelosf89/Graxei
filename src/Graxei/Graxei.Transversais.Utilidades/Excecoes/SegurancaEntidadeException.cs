using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class SegurancaEntidadeException : GraxeiException
    {
        public SegurancaEntidadeException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
