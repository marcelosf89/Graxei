using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Transversais.ContratosDeDados.Interfaces
{
    public interface ILista<T> where T : IItemLista
    {
        IList<T> Lista { get; }
        TotalElementosLista Total { get; }
        PaginaAtualLista Atual { get; }
    }
}