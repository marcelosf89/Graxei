using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Utilidades.Excecoes
{
    public class ObjetoConstrucaoException : GraxeiException
    {
        public ObjetoConstrucaoException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
