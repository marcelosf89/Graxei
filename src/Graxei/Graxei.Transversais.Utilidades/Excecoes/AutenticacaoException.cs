using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class AutenticacaoException: Exception
    {
        public AutenticacaoException(string mensagem) : base(mensagem)
        {
        }
    }
}
