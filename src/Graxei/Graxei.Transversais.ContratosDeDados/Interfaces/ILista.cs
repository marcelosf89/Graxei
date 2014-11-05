using System.Collections.Generic;

namespace Graxei.Transversais.ContratosDeDados.Interfaces
{
    public interface ILista<T> where T : IItemLista
    {
        IList<T> Lista { get; }
        int Total { get; }
    }
}