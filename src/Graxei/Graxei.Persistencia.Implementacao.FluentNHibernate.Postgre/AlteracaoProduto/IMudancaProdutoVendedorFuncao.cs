using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public interface IMudancaProdutoVendedorFuncao
    {
        void Aceitar(IVisitorCriacaoFuncao visitor);
    }
}
