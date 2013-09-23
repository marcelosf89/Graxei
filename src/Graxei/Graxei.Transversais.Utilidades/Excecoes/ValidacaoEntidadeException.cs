using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ValidacaoEntidadeException : Exception
    {
        public ValidacaoEntidadeException(string mensagem) : base(mensagem)
        {
        }
    }
}
