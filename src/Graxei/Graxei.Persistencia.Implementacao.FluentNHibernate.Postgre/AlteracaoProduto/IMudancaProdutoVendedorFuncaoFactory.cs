using Graxei.Transversais.ContratosDeDados;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public interface IMudancaProdutoVendedorFuncaoFactory
    {
        IList<IMudancaProdutoVendedorFuncao> GetComBaseEm(IList<ProdutoLojaPrecoContrato> list);
    }
}
