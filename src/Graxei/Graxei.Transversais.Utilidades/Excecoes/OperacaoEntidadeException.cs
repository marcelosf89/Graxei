using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class OperacaoEntidadeException : GraxeiException
    {
        public OperacaoEntidadeException(string mensagem):base(mensagem)
        {
        }
    }
}
