using Graxei.Modelo.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Contrato.Pesquisa
{
    
    public interface IPesquisa
    {
        IList<IPesquisaPrincipal> Pesquisar(string nome);
    }

}