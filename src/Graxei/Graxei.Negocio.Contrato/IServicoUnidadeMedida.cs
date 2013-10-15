using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoUnidadeMedida : IServicoEntidades<UnidadeMedida>
    {
        void PreSalvar(UnidadeMedida unidade);
        void PreAtualizar(UnidadeMedida unidade);
    }
}
