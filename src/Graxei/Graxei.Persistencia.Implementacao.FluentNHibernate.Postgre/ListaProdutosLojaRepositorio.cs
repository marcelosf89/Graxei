using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre
{
    public class ListaProdutosLojaRepositorio : IRepositorioListaProdutosLoja
    {
        private ISession _sessao;
        private IListaProdutosLojaSqlResolverFactory _listaProdutosLojaSqlResolverFactory;

        public ListaProdutosLojaRepositorio(IListaProdutosLojaSqlResolverFactory listaProdutosLojaSqlResolverFactory)
        {
            _listaProdutosLojaSqlResolverFactory = listaProdutosLojaSqlResolverFactory;
        }

        public ListaProdutosLoja GetSomenteUmEndereco(string criterio, bool somenteMeusProdutos, long idLoja, int pagina, int tamanhoPagina, int totalElementos)
        {
            IListaProdutosLojaSqlResolver sqlResolver = _listaProdutosLojaSqlResolverFactory.Get(idLoja, criterio, somenteMeusProdutos);
            long total = totalElementos;
            if (totalElementos == 0)
            {
                ////total = GetSessaoAtual().QueryOver<Produto>()
                ////                        .Where(Restrictions.InsensitiveLike(Projections.Property<Produto>(p => p.Descricao),
                ////                                                            criterio.ToLower(), MatchMode.Anywhere))
                ////                        .JoinQueryOver<ProdutoVendedor>(p => p.)
                ////                        .Where(Restrictions.InsensitiveLike(Projections.Property<ProdutoVendedor>(p => p.Descricao),
                ////                                                            criterio.ToLower(), MatchMode.Anywhere))
                ////                        .JoinQueryOver<Endereco>(p => p.Endereco)
                ////                        .JoinQueryOver<Loja>(p => p.Loja)
                ////                        .Where(p => p.Id == idLoja)
                ////                        .RowCount();
                total = sqlResolver.GetConsultaDeContagem();

                if (total == 0)
                {
                    return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
                }
            }

            IList<ListaProdutosLojaContrato> lista = sqlResolver.Get(pagina, tamanhoPagina);

            TotalElementosLista listaTotalElementos = new TotalElementosLista(total);
            PaginaAtualLista listaElementoAtual = new PaginaAtualLista(pagina);
            if (!lista.Any())
            {
                return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
            }
            return new ListaProdutosLoja(lista, listaTotalElementos, listaElementoAtual);
        }

        public ListaProdutosLoja GetSomenteUmEndereco(string criterio, bool meusProdutos, long idLoja, int pagina, int tamanhoPagina)
        {
            return GetSomenteUmEndereco(criterio, meusProdutos, idLoja, pagina, tamanhoPagina, 0);
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

    }
}
