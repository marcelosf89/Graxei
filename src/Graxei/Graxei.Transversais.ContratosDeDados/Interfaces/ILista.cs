using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Transversais.ContratosDeDados.Interfaces
{
    public interface ILista<T> where T : IItemLista
    {
        IList<T> Lista { get; }
        ListaTotalElementos Total { get; }
        ListaElementoAtual Atual { get; }
    }
}