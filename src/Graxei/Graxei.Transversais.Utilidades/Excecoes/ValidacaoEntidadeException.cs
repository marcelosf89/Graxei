using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ValidacaoEntidadeException : GraxeiException
    {
        public ValidacaoEntidadeException(string mensagem) : base(mensagem)
        {
        }
    }
}
