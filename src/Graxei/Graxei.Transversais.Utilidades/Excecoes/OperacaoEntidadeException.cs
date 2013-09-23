using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class OperacaoEntidadeException : Exception
    {
        public OperacaoEntidadeException(string mensagem):base(mensagem)
        {
        }
    }
}
