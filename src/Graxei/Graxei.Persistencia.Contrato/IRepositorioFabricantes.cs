using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioFabricantes : IRepositorioIrrestrito<Fabricante>
    {
        IList<string> TodosNomes();
    }
}