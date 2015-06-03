using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Autenticacao.Interfaces;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Transversais.Comum.NHibernate;
using System.Collections.Generic;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoProdutoVendedor : ServicoPadraoEntidades<ProdutoVendedor>, IServicoProdutoVendedor
    {
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;

        public ServicoProdutoVendedor(IRepositorioProdutoVendedor repositorio, IRepositorioProdutos repositorioProdutos, IServicoProdutos servicoProdutos, IServicoAtributos servicoAtributos, IServicoUnidadeMedida servicoUnidadeMedida, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
           RepositorioEntidades = repositorio;
            _repositorioProdutos = repositorioProdutos;
            _servicoProdutos = servicoProdutos;
            _servicoAtributos = servicoAtributos;
            _servicoUnidadeMedida = servicoUnidadeMedida;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }

        public override void PreSalvar(ProdutoVendedor produtoVendedor)
        {
            Validar(produtoVendedor);
            _servicoProdutos.PreSalvar(produtoVendedor.Produto);
            _servicoAtributos.PreSalvar(produtoVendedor);
            ProdutoVendedor pvExiste = Repositorio.GetPorDescricaoAndLoja(produtoVendedor.Descricao,
                                                                          produtoVendedor.Endereco.Loja.Nome);
            if (pvExiste != null)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoMesmaLoja);
            }
        }

        public override void PreAtualizar(ProdutoVendedor produtoVendedor)
        {
            Validar(produtoVendedor);
            _servicoProdutos.PreAtualizar(produtoVendedor.Produto);
            _servicoAtributos.PreAtualizar(produtoVendedor);
            ProdutoVendedor pvExiste = Repositorio.GetPorDescricaoAndLoja(produtoVendedor.Descricao,
                                                                          produtoVendedor.Endereco.Loja.Nome);
            if (pvExiste != null && pvExiste.Id != produtoVendedor.Id)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoMesmaLoja);
            }
        }

        public void Validar(ProdutoVendedor produtoVendedor)
        {
            if (produtoVendedor.Produto == null)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoNulo);
            }
            if (produtoVendedor.Descricao == null)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoDescricaoNulo);
            }
            if (produtoVendedor.Preco <= 0)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoPrecoInvalido);
            }
            if (produtoVendedor.UnidadeEntrada == null)
            {
                throw new ValidacaoEntidadeException(Erros.UnidadeEntradaNulo);
            }
            if (produtoVendedor.UnidadeSaida == null)
            {
                throw new ValidacaoEntidadeException(Erros.UnidadeSaidaNulo);
            }
            _servicoUnidadeMedida.PreSalvar(produtoVendedor.UnidadeEntrada);
            _servicoUnidadeMedida.PreSalvar(produtoVendedor.UnidadeSaida);
            if (produtoVendedor.Endereco == null || produtoVendedor.Endereco.Loja == null)
            {
                throw new ValidacaoEntidadeException(Erros.LojaNula);
            }
            if (produtoVendedor.FatorConversao <= 0)
            {
                produtoVendedor.FatorConversao = 1;
            }
        }

        public System.Collections.Generic.IList<PesquisaContrato> Get(string texto)
        {
            return Repositorio.GetPorDescricaoPesquisa(texto, "", "", 0);
        }

        public System.Collections.Generic.IList<PesquisaContrato> Get(string texto, string pais, string cidade, int page)
        {
            return Repositorio.GetPorDescricaoPesquisa(texto, pais, cidade, page);
        }


        public long GetMax(string texto, string pais, string cidade, int page)
        {
            return Repositorio.GetMaxPorDescricaoPesquisa(texto, pais, cidade, page);
        }


        public long GetQuantidadeProduto()
        {
            Usuario usuario = _gerenciadorAutenticacao.Get();
            return Repositorio.GetQuantidadeProduto(usuario);
        }

        public long GetQuantidadeProduto(long lojaId)
        {
            return Repositorio.GetQuantidadeProduto(lojaId);
        }

        public IList<ProdutoLojaPrecoContrato> AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato)
        {
            return Repositorio.AtualizarLista(produtoLojaPrecoContrato);
        }

        public IRepositorioProdutoVendedor Repositorio
        {
            get { return (IRepositorioProdutoVendedor) RepositorioEntidades; }
        }

        private IRepositorioProdutos _repositorioProdutos;
        private readonly IServicoProdutos _servicoProdutos;
        private readonly IServicoAtributos _servicoAtributos;
        private readonly IServicoUnidadeMedida _servicoUnidadeMedida;
    }
}