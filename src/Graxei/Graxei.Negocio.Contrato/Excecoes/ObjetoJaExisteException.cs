using System;

namespace Graxei.Negocio.Contrato.Excecoes
{
    public class ObjetoJaExisteException : Exception
    {
        public ObjetoJaExisteException(string mensagem): base(mensagem)
        {
        }
    }
}
