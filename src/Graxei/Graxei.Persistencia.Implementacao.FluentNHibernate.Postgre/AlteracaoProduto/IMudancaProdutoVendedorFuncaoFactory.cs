using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public interface IMudancaProdutoVendedorFuncaoFactory
    {
        IList<IMudancaProdutoVendedorFuncao> GetComBaseEm(IList<ProdutoLojaPrecoContrato> list);
    }
}
