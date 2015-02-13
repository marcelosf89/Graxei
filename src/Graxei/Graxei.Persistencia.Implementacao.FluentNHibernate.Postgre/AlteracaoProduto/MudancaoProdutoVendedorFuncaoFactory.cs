using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public class MudancaoProdutoVendedorFuncaoFactory : IMudancaProdutoVendedorFuncaoFactory
    {

        public MudancaoProdutoVendedorFuncaoFactory(IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }

        public IList<IMudancaProdutoVendedorFuncao> GetComBaseEm(IList<ProdutoLojaPrecoContrato> list)
        {
            if (list == null)
            {
                return new List<IMudancaProdutoVendedorFuncao>();
            }

            List<IMudancaProdutoVendedorFuncao> resultado = new List<IMudancaProdutoVendedorFuncao>();
            for (int i = 0; i < list.Count; i++)
            {
                resultado.Add(GetComBaseEm(list[i]));
            }

            return resultado;
        }

        public IMudancaProdutoVendedorFuncao GetComBaseEm(ProdutoLojaPrecoContrato produtoLojaPrecoContrato)
        {
            switch (produtoLojaPrecoContrato.OperacaoNoContrato)
            {
                case OperacaoProdutoLoja.Incluir:
                    return new CriarProdutoVendedor(produtoLojaPrecoContrato.IdProduto,
                                                    produtoLojaPrecoContrato.MinhaDescricao,
                                                    produtoLojaPrecoContrato.Preco,
                                                    produtoLojaPrecoContrato.IdEndereco,
                                                    _gerenciadorAutenticacao.Get());
                    break;
                case OperacaoProdutoLoja.Alterar:
                    return new AlterarProdutoVendedor(produtoLojaPrecoContrato.IdMeuProduto,
                                                      produtoLojaPrecoContrato.MinhaDescricao,
                                                      produtoLojaPrecoContrato.Preco,
                                                      _gerenciadorAutenticacao.Get());
                    break;
                case OperacaoProdutoLoja.Excluir:
                    return new ExcluirProdutoVendedor(produtoLojaPrecoContrato.IdMeuProduto,
                                                      _gerenciadorAutenticacao.Get());
            }

            return null;
        }

        private IGerenciadorAutenticacao _gerenciadorAutenticacao;
    }
}
