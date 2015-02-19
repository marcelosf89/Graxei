using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;
using System.Linq;
using Graxei.Transversais.ContratosDeDados;
using NHibernate.Criterion;
using NHibernate.Transform;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using NHibernate;
using Graxei.FluentNHibernate.UnitOfWork;
using System.Data;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    /// <summary>
    /// Classe de implementação das funções relativas à entidade ProdutoVendedor
    /// </summary>
    public class ProdutoVendedorRepositorio : PadraoNHibernatePostgre<ProdutoVendedor>, IRepositorioProdutoVendedor
    {
        public ProdutoVendedorRepositorio(IVisitorCriacaoFuncao visitorCriacaoFuncao, IMudancaProdutoVendedorFuncaoFactory mudancaFactory)
        {
            _visitorCriacaoFuncao = visitorCriacaoFuncao;
            _mudancaFactory = mudancaFactory; 
        }

        #region Implementação de IRepositorioProdutoVendedor

        public IList<ProdutoVendedor> GetPorDescricao(string descricao)
        {
            return GetSessaoAtual().Query<ProdutoVendedor>().Where(p => p.Descricao != null
                                                                             &&
                                                                             p.Descricao.Trim().ToLower() ==
                                                                             descricao.Trim().ToLower()).ToList<ProdutoVendedor>();
        }

        public IList<PesquisaContrato> GetPorDescricaoPesquisa(string descricao, string pais, string cidade, int page)
        {
            String loja = GetLojaNaDescricao(descricao);

            descricao = descricao.ToLower().Replace("loja:" + loja, "");

            String textos = descricao.Replace(" ", "");
            double textoL = textos.Length * 0.0074;

            String lojasql = "";
            if (!String.IsNullOrEmpty(loja))
            {
                lojasql = "and (l.nome = '" + loja + "' or l.url = '" + loja + "' )";
            }

            String sql = @"
                select pv.id_produto_vendedor as ""Id"", pv.Descricao ""Descricao"",  p.Codigo ""Codigo"",
                    pv.Preco ""Preco"", pv.id_produto ""ProdutoId"", pv.id_endereco ""EnderecoId"",
                    tl.numero ""Numero""
                from produtos p 
                join produtos_vendedores pv on p.id_produto = pv.id_produto
                join enderecos en on en.id_endereco = pv.id_endereco
                join lojas l on l.id_loja = en.id_loja
                join telefones tl on en.id_endereco = tl.id_endereco
                where similarity(p.descricao || ' ' || p.codigo,:descricao)  > :val {0}
                order by similarity(p.descricao || ' ' || p.codigo,:descricao) desc
                ";

            sql = String.Format(sql, lojasql);

            return GetSessaoAtual().CreateSQLQuery(sql)
          .SetResultTransformer(Transformers.AliasToBean(typeof(PesquisaContrato)))
          .SetParameter<String>("descricao", descricao)
          .SetParameter<double>("val", textoL)
          .SetFirstResult((page * 10) < 0 ? 1 : (page * 10))
          .SetMaxResults(10)
          .List<PesquisaContrato>();
        }

        private String GetLojaNaDescricao(String descricao)
        {
            String loja = "";
            if (descricao.ToLower().Contains("loja:"))
            {
                int idxOfLoja = descricao.ToLower().IndexOf("loja:");
                int nIdxOf = descricao.Substring(idxOfLoja).IndexOf(' ');

                if (nIdxOf <= 0)
                    loja =   descricao.Substring(idxOfLoja + 5);
                else
                    loja = descricao.Substring(idxOfLoja + 5, nIdxOf - 5);
            }
            return  loja;
        }

        public ProdutoVendedor GetPorDescricaoAndLoja(string descricao, string nomeLoja)
        {
            ProdutoVendedor pvl = GetSessaoAtual().Query<ProdutoVendedor>()
                                                 .SingleOrDefault(p => p.Descricao.Trim().ToLower() == descricao.Trim().ToLower()
                                                                    && p.Endereco.Loja.Nome.Trim().ToLower() == nomeLoja.Trim().ToLower());
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
            ProdutoVendedor produtoVendedor =
                                  GetSessaoAtual().Query<ProdutoVendedor>()
                                             .SingleOrDefault(p => p.Descricao.Trim().ToLower() == descricao.Trim().ToLower() && p.Endereco.Loja.Nome.Trim().ToLower() == loja.Nome.Trim().ToLower());
            return produtoVendedor;
        }

        public void ExcluirDe(Loja loja)
        {
            if (loja == null || UtilidadeEntidades.IsTransiente(loja))
            {
                throw new ArgumentException("Loja é nula ou não foi salva");
            }
            GetSessaoAtual().CreateSQLQuery(ConsultasSQL.ExcluirProdutosVendedorDeLoja)
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

        public long GetMaxPorDescricaoPesquisa(string descricao, string pais, string cidade, int page)
        {
            String textos = descricao.Replace(" ", "");
            double textoL = textos.Length * 0.0074;

            String sql = @"
                select count(p.id_produto) from produtos p 
                join produtos_vendedores pv on p.id_produto = pv.id_produto
                join enderecos en on en.id_endereco = pv.id_endereco
                join telefones tl on en.id_endereco = tl.id_endereco
                where similarity(p.descricao || ' ' || p.codigo,:descricao)  > :val
                ";
            return GetSessaoAtual().CreateSQLQuery(sql)
                .SetParameter<String>("descricao", descricao)
                .SetParameter<double>("val", textoL)
                .UniqueResult<long>();
        }


        public long GetQuantidadeProduto(Usuario usuario)
        {
            return (from l in GetSessaoAtual().Query<Loja>()
                    from e in l.Enderecos
                    join pv in SessaoAtual.Query<ProdutoVendedor>() on e.Id equals pv.Endereco.Id
                    from u in l.Usuarios
                    where u.Id == usuario.Id
                    select pv.Id).Count();
        }


        public long GetQuantidadeProduto(long lojaId)
        {
            return (from l in GetSessaoAtual().Query<Loja>()
                    from e in l.Enderecos
                    join pv in SessaoAtual.Query<ProdutoVendedor>() on e.Id equals pv.Endereco.Id
                    from u in l.Usuarios
                    where l.Id == lojaId
                    select pv.Id).Count();
        }

        public void AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContratos)
        {
            if (produtoLojaPrecoContratos == null || produtoLojaPrecoContratos.Count == 0)
            {
                return;
            }

            IList<IMudancaProdutoVendedorFuncao> listaMudancaProdutoVendedor = _mudancaFactory.GetComBaseEm(produtoLojaPrecoContratos);
            foreach (IMudancaProdutoVendedorFuncao produtoVendedor in listaMudancaProdutoVendedor)
            {
                produtoVendedor.Aceitar(_visitorCriacaoFuncao);
            }

            string sql = _visitorCriacaoFuncao.GetResultado();
            using (IDbCommand command = GetSessaoAtual().Connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteReader();
            };
            
            //GetSessaoAtual().CreateSQLQuery(sql)
                           // .UniqueResult();
        }

        public ISession GetSessaoAtual()
        {
            if (_sessao == null)
            {
                _sessao = UnitOfWorkNHibernate.GetInstancia().SessaoAtual;
            }

            return _sessao;
        }

        public void SetSessaoAtual(ISession session)
        {
            _sessao = session;
        }

        private IVisitorCriacaoFuncao _visitorCriacaoFuncao;

        private IMudancaProdutoVendedorFuncaoFactory _mudancaFactory;

        private ISession _sessao;
    }
}
