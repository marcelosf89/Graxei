using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Autenticacao.Interfaces;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Transversais.Comum.NHibernate;
using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoProdutoVendedor : ServicoPadraoEntidades<ProdutoVendedor>, IServicoProdutoVendedor
    {
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;

        public ServicoProdutoVendedor(IRepositorioProdutoVendedor repositorio, IRepositorioProdutos repositorioProdutos, IServicoProdutos servicoProdutos, IServicoAtributos servicoAtributos, IServicoUnidadeMedida servicoUnidadeMedida, IGerenciadorAutenticacao gerenciadorAutenticacao, IRepositorioPesquisaProduto repositorioPesquisaProduto)
        {
           RepositorioEntidades = repositorio;
            _repositorioProdutos = repositorioProdutos;
            _servicoProdutos = servicoProdutos;
            _servicoAtributos = servicoAtributos;
            _servicoUnidadeMedida = servicoUnidadeMedida;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
            _repositorioPesquisaProduto = repositorioPesquisaProduto;
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

        public ListaPesquisaContrato Get(string texto)
        {
            IList<PesquisaContrato> resultadoPesquisa = _repositorioPesquisaProduto.GetPorDescricaoPesquisa(texto, "", "", 0);
            return new ListaPesquisaContrato(resultadoPesquisa, new TotalElementosLista(0), new PaginaAtualLista(0));
        }

        public ListaPesquisaContrato Get(string texto, string pais, string cidade, int page)
        {
            IList<PesquisaContrato> resultadoPesquisa = _repositorioPesquisaProduto.GetPorDescricaoPesquisa(texto, "", "", 0);
            return new ListaPesquisaContrato(resultadoPesquisa, new TotalElementosLista(0), new PaginaAtualLista(page));
        }

        public ListaPesquisaContrato GetUltimaPagina(string texto, string pais, string cidade)
        {
            return _repositorioPesquisaProduto.GetUltimaPagina(10, texto, pais, cidade);
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
        IRepositorioPesquisaProduto _repositorioPesquisaProduto;
    }
}