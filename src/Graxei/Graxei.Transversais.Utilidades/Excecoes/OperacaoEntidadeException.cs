using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class OperacaoEntidadeException : EntidadesException
    {
        public OperacaoEntidadeException(string mensagem):base(mensagem)
        {
        }
    }
}
