using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using System.Collections.Generic;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

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

        public void PreSalvar(Produto produto)
        {
            ValidarProduto(produto);
            Produto pExiste = _reposProdutos.GetPorDescricao(produto.Descricao);
            if (pExiste != null)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoExiste);
            }
        }

        public void PreAtualizar(Produto produto)
        {
            ValidarProduto(produto);
            Produto pExiste = _reposProdutos.GetPorDescricao(produto.Descricao);
            if (pExiste != null && pExiste.Id != produto.Id)
            {
                throw new ObjetoJaExisteException(Erros.ProdutoExiste);
            }
        }

        #endregion

        private void ValidarProduto(Produto produto)
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
    }
}