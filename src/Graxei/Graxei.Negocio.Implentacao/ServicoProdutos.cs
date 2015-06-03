using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoProdutos : ServicoPadraoEntidades<Produto>, IServicoProdutos
    {
        public ServicoProdutos(IRepositorioProdutos reposProdutos)
        {
            _reposProdutos = reposProdutos;
        }

        #region Atributos privados
        private readonly IRepositorioProdutos _reposProdutos;
        #endregion

        #region Implementação de IServicoProdutos

        public override void PreSalvar(Produto produto)
        {
            Validar(produto);
            Produto pExiste = _reposProdutos.GetPorDescricao(produto.Descricao);
            if (pExiste != null)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoExiste);
            }
        }

        public override void PreAtualizar(Produto produto)
        {
            Validar(produto);
            Produto pExiste = _reposProdutos.GetPorDescricao(produto.Descricao);
            if (pExiste != null && pExiste.Id != produto.Id)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoExiste);
            }
        }

        #endregion

        public void Validar(Produto produto)
        {
            if (produto.Descricao == null)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoDescricaoNulo);
            }
            if (produto.Fabricante == null)
            {
                throw new ValidacaoEntidadeException(Erros.ProdutoFabricanteNulo);
            }
        }

        public long GetMax(string texto)
        {
            return _reposProdutos.GetMax(texto);
        }

        public System.Collections.Generic.IList<Produto> Get(string texto, long page)
        {
            return _reposProdutos.Get(texto, page);
        }
    }
}