using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class RepetidoEmColecaoException : OperacaoEntidadeException
    {
        public RepetidoEmColecaoException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
