using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ObjetoNaoEncontradoException : OperacaoEntidadeException
    {
        public ObjetoNaoEncontradoException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
