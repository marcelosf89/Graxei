namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor
{
    public interface IVisitorCriacaoFuncao
    {
        string GetResultado();
        string Visit(CriarProdutoVendedor funcao);
        string Visit(AlterarProdutoVendedor funcao);
        string Visit(ExcluirProdutoVendedor funcao);
    }
}
