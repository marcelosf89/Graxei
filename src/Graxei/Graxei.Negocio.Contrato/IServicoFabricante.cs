using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoFabricantes : IServicoEntidade<Fabricante>
    {
        IList<string> TodosNomes();
    }
}
