using System;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class AutenticacaoException : GraxeiException
    {
        public AutenticacaoException(string mensagem) : base(mensagem)
        {
        }
    }
}
