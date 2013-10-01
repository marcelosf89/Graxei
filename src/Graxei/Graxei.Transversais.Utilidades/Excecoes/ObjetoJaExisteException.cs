using System;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ObjetoJaExisteException : EntidadesException
    {
        public ObjetoJaExisteException(string mensagem): base(mensagem)
        {
        }
    }
}
