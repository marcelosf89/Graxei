using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class GerenciamentoProdutos : PadraoTransacao, IGerenciamentoProdutos
    {
        public GerenciamentoProdutos(IServicoProdutoVendedor servicoProdutoVendedor)
        {
            Servico = servicoProdutoVendedor;
        }

        #region Implementação de IGerenciamentoProdutos
        public void Salvar(ProdutoVendedor produtoVendedor)
        {
            IniciarTransacao();
            try
            {
                Servico.Salvar(produtoVendedor);
                Confirmar();
            } catch (OperacaoEntidadeException oe)
            {
                Desfazer();
                throw;
            }
        }

        public void Excluir(ProdutoVendedor produtoVendedor)
        {
            IniciarTransacao();
            try
            {
                Servico.Excluir(produtoVendedor);
                Confirmar();
            }
            catch (OperacaoEntidadeException oe)
            {
                Desfazer();
                throw;
            }
        }

        public IServicoProdutoVendedor Servico { get; private set; }

        #endregion
    }
}