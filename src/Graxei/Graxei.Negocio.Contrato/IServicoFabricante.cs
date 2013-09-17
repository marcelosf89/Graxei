using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoFabricantes : IServicoEntidades<Fabricante>
    {
        IList<string> TodosNomes();
    }
}
