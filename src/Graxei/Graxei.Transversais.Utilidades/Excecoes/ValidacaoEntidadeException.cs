using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ValidacaoEntidadeException : OperacaoEntidadeException
    {
        public ValidacaoEntidadeException(string mensagem) : base(mensagem)
        {
        }
    }
}
