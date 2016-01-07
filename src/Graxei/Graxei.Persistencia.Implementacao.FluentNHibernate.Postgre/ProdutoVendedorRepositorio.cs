using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Transversais.Comum.NHibernate;
using NHibernate.Linq;
using System.Linq;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using NHibernate;
using Graxei.FluentNHibernate.UnitOfWork;
using System.Data;
using Graxei.Transversais.Comum;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class ProdutoVendedorRepositorio : PadraoNHibernatePostgre<ProdutoVendedor>, IRepositorioProdutoVendedor
    {
        public ProdutoVendedorRepositorio(IVisitorCriacaoFuncao visitorCriacaoFuncao, IMudancaProdutoVendedorFuncaoFactory mudancaFactory, IProdutoVendedorNativo produtoVendedorNativo)
        {
            _visitorCriacaoFuncao = visitorCriacaoFuncao;
            _mudancaFactory = mudancaFactory;
            _produtoVendedorNativo = produtoVendedorNativo;
        }

        public IList<ProdutoVendedor> GetPorDescricao(string descricao)
        {
            return GetSessaoAtual().Query<ProdutoVendedor>().Where(p => p.Descricao != null
                                                                             &&
                                                                             p.Descricao.Trim().ToLower() ==
                                                                             descricao.Trim().ToLower()).ToList<ProdutoVendedor>();
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

        public new void Excluir(ProdutoVendedor produtoVendedor)
        {
            produtoVendedor.Excluida = true;
            Salvar(produtoVendedor);
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

        public IList<ProdutoLojaPrecoContrato> AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContratos)
        {
            List<ProdutoLojaPrecoContrato> resultado = new List<ProdutoLojaPrecoContrato>();
            if (Listas.NulaOuVazia<ProdutoLojaPrecoContrato>(produtoLojaPrecoContratos))
            {
                return resultado;
            }

            IList<IMudancaProdutoVendedorFuncao> listaMudancaProdutoVendedor = _mudancaFactory.GetComBaseEm(produtoLojaPrecoContratos);
            foreach (IMudancaProdutoVendedorFuncao produtoVendedor in listaMudancaProdutoVendedor)
            {
                produtoVendedor.Aceitar(_visitorCriacaoFuncao);
            }

            string sql = _visitorCriacaoFuncao.GetResultado();
            return _produtoVendedorNativo.Get(sql);
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

        private IProdutoVendedorNativo _produtoVendedorNativo;

        private ISession _sessao;
    }
}
