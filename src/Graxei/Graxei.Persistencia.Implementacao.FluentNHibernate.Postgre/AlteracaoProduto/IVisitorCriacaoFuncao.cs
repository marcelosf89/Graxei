using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public interface IVisitorCriacaoFuncao
    {
        void Visit(CriarProdutoVendedor funcao);
        void Visit(AlterarProdutoVendedor funcao);
        void Visit(ExcluirProdutoVendedor funcao);
    }
}
