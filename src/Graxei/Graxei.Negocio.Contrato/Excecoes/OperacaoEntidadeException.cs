using System;
namespace Graxei.Negocio.Contrato.Excecoes
{
    public class OperacaoEntidadeException : Exception
    {
        public OperacaoEntidadeException(string mensagem):base(mensagem)
        {
        }
    }
}
