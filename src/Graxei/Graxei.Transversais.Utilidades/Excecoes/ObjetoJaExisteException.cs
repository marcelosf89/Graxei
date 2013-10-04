using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ObjetoJaExisteException : OperacaoEntidadeException
    {
        public ObjetoJaExisteException(string mensagem): base(mensagem)
        {
        }
    }
}
