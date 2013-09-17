using System;

namespace Graxei.Negocio.Contrato.Excecoes
{
    public class ValidacaoEntidadeException : Exception
    {
        public ValidacaoEntidadeException(string mensagem) : base(mensagem)
        {
        }
    }
}
