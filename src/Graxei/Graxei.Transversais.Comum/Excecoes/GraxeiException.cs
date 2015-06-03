using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class GraxeiException : Exception
    {
        public GraxeiException(string mensagem) : base(mensagem)
        {
        }
    }
}
