namespace Graxei.Persistencia.Contrato.PesquisaProduto
{
    public interface IPesquisaProdutoFactory
    {
        IPesquisaProdutoRepositorio Get(string criterio);
    }
}
