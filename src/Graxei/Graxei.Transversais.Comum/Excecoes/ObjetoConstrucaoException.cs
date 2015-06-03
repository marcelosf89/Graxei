using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class ObjetoConstrucaoException : GraxeiException
    {
        public ObjetoConstrucaoException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
