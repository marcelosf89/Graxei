using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioFabricantes : IRepositorioEntidades<Fabricante>
    {
        IList<string> TodosNomes();
    }
}