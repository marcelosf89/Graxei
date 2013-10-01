using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class AutenticacaoException : GraxeiException
    {
        public AutenticacaoException(string mensagem) : base(mensagem)
        {
        }
    }
}
