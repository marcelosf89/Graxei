using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ValidacaoEntidadeException : EntidadesException
    {
        public ValidacaoEntidadeException(string mensagem) : base(mensagem)
        {
        }
    }
}
