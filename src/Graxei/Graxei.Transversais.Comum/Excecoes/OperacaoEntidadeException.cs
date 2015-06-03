using System;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class OperacaoEntidadeException : GraxeiException
    {
        public OperacaoEntidadeException(string mensagem):base(mensagem)
        {
        }
    }
}
