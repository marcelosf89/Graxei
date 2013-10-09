using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoProdutoVendedor : ServicoPadraoSomenteLeitura<ProdutoVendedor>, IServicoProdutoVendedor
    {

        #region Construtor
        public ServicoProdutoVendedor(IRepositorioProdutoVendedor repositorio, IRepositorioProdutos repositorioProdutos, IServicoProdutos servicoProdutos, IServicoAtributos servicoAtributos)
        {
            _repositorioEntidades = repositorio;
            _repositorioProdutos = repositorioProdutos;
            _servicoProdutos = servicoProdutos;
            _servicoAtributos = servicoAtributos;
        }
        #endregion

        public void Salvar(ProdutoVendedor produtoVendedor)
        {
            if (UtilidadeEntidades.IsTransiente(produtoVendedor))
            {
                PreSalvar(produtoVendedor);
            }
            else
            {
                PreAtualizar(produtoVendedor);
            }
            Repositorio.Salvar(produtoVendedor);
        }

        private void PreSalvar(ProdutoVendedor produtoVendedor)
        {
            ValidarAndCorrigirEntidade(produtoVendedor);
            _servicoProdutos.PreSalvar(produtoVendedor.Produto);
            _servicoAtributos.PreSalvar(produtoVendedor);
            ProdutoVendedor pvExiste = Repositorio.GetPorDescricaoAndLoja(produtoVendedor.Descricao,
                                                                          produtoVendedor.Loja.Nome);
            if (pvExiste != null)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoMesmaLoja);
            }
        }

        private void PreAtualizar(ProdutoVendedor produtoVendedor)
        {
            ValidarAndCorrigirEntidade(produtoVendedor);
            _servicoProdutos.PreAtualizar(produtoVendedor.Produto);
            _servicoAtributos.PreAtualizar(produtoVendedor);
            ProdutoVendedor pvExiste = Repositorio.GetPorDescricaoAndLoja(produtoVendedor.Descricao,
                                                                          produtoVendedor.Loja.Nome);
            if (pvExiste != null && pvExiste.Id != produtoVendedor.Id)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoMesmaLoja);
            }
        }

        private void ValidarAndCorrigirEntidade(ProdutoVendedor produtoVendedor)
        {
            if (produtoVendedor.Produto == null)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoNulo);
            }
            if (produtoVendedor.Descricao == null)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoDescricaoNulo);
            }
            if (produtoVendedor.UnidadeEntrada == null)
            {
                throw new ValidacaoEntidadeException(Erros.UnidadeEntradaNulo);
            }
            if (produtoVendedor.UnidadeSaida == null)
            {
                throw new ValidacaoEntidadeException(Erros.UnidadeSaidaNulo);
            }
            if (produtoVendedor.Loja == null)
            {
                throw new ValidacaoEntidadeException(Erros.LojaNulo);
            }
            if (produtoVendedor.FatorConversao <= 0)
            {
                produtoVendedor.FatorConversao = 1;
            }
        }

        #region Propriedades Privadas
        public IRepositorioProdutoVendedor Repositorio
        {
            get { return (IRepositorioProdutoVendedor) _repositorioEntidades; }
        }
        #endregion

        #region Atributos Privados
        private IRepositorioProdutos _repositorioProdutos;
        private IServicoProdutos _servicoProdutos;
        private IServicoAtributos _servicoAtributos;

        #endregion
    }
}