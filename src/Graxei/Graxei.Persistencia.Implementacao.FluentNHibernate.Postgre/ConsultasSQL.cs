namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class ConsultasSQL
    {
        public const string ExcluirProdutosVendedorDeLoja =
            "UPDATE produtos_vendedor SET excluida = 1 WHERE id_loja = :p0";
    }
}
