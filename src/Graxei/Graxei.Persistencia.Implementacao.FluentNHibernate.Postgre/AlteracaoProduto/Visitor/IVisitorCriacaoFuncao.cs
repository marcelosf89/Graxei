using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor
{
    public interface IVisitorCriacaoFuncao
    {
        string Visit(CriarProdutoVendedor funcao);
        string Visit(AlterarProdutoVendedor funcao);
        string Visit(ExcluirProdutoVendedor funcao);
    }
}
