using System;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class ValidacaoEntidadeException : OperacaoEntidadeException
    {
        public ValidacaoEntidadeException(string mensagem) : base(mensagem)
        {
        }
    }
}
