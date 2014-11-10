using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    /// <summary>
    /// Classe de implementação das funções relativas à entidade ProdutoVendedor
    /// </summary>
    public class ProdutoVendedorNHibernateMySQL : PadraoNHibernateMySQL<ProdutoVendedor>, IRepositorioProdutoVendedor
    {
        #region Implementação de IRepositorioProdutoVendedor

        public IList<ProdutoVendedor> GetPorDescricao(string descricao)
        {
            return SessaoAtual.Query<ProdutoVendedor>().Where(p => p.Descricao != null
                                                                             &&
                                                                             p.Descricao.Trim().ToLower() ==
                                                                             descricao.Trim().ToLower()).ToList<ProdutoVendedor>();
        }

        public long GetMaxPorDescricaoPesquisa(string descricao, string pais, string cidade, int page)
        {
            
            String sql = @"
                select count(p.id_produto) from produtos p 
                join produtos_vendedores pv on p.id_produto = pv.id_produto
                where match(p.descricao, p.codigo) against(:descricao)
                ";
            return SessaoAtual.CreateSQLQuery(sql)
                .SetParameter<String>("descricao", descricao)
                .UniqueResult<long>();
        }

        public IList<ProdutoVendedor> GetPorDescricaoPesquisa(string descricao, string pais, string cidade, int page)
        {
            String sql = @"
                select pv.* from produtos p 
                join produtos_vendedores pv on p.id_produto = pv.id_produto
                where match(p.descricao, p.codigo) against(:descricao)
                ";
            return SessaoAtual.CreateSQLQuery(sql)
                .AddEntity(typeof(ProdutoVendedor))
                .SetParameter<String>("descricao",descricao)
                .SetFirstResult((page * 10) < 0 ? 1 : (page * 10))
                .SetMaxResults(10)
                .List<ProdutoVendedor>();
        }

        public ProdutoVendedor GetPorDescricaoAndLoja(string descricao, string nomeLoja)
        {
            ProdutoVendedor pvl = SessaoAtual.Query<ProdutoVendedor>()
                                                 .SingleOrDefault(p => p.Descricao.Trim().ToLower() == descricao.Trim().ToLower()
                                                                    && p.Loja.Nome.Trim().ToLower() == nomeLoja.Trim().ToLower());
            return pvl;
        }

        public ProdutoVendedor GetPorDescricaoAndLoja(string descricao, Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentNullException("loja", Erros.LojaNula);
            }
            if (!loja.Validar())
            {
                throw new EntidadeInvalidaException(Erros.LojaInvalida);
            }
            ProdutoVendedor pvl = SessaoAtual.Query<ProdutoVendedor>()
                                             .SingleOrDefault(p => p.Descricao.Trim().ToLower() == descricao.Trim().ToLower() && p.Loja.Nome.Trim().ToLower() == loja.Nome.Trim().ToLower());
            return pvl;
        }

        public void ExcluirDe(Loja loja)
        {
            if (loja == null || UtilidadeEntidades.IsTransiente(loja))
            {
                throw new ArgumentException("Loja é nula ou não foi salva");
            }
            SessaoAtual.CreateSQLQuery(ConsultasSQL.ExcluirProdutosVendedorDeLoja)
                       .SetParameter("p0", loja.Id).ExecuteUpdate();
        }

        #endregion

        #region Métodos de Sobrescrita
        public new void Excluir(ProdutoVendedor produtoVendedor)
        {
            produtoVendedor.Excluida = true;
            Salvar(produtoVendedor);
        }

        #endregion
    }

}
