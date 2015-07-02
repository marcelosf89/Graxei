using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Contrato.PesquisaProduto
{
    public interface IPesquisaProdutoFactory
    {
        IPesquisaProdutoRepositorio Get(string criterio);
    }
}
