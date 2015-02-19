using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados;
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
        private IVisitorCriacaoFuncao _visitorCriacaoFuncao;

        public ListaProdutosLojaRepositorio(IListaProdutosLojaSqlResolverFactory listaProdutosLojaSqlResolverFactory, IVisitorCriacaoFuncao visitorCriacaoFuncao)
        {
            _listaProdutosLojaSqlResolverFactory = listaProdutosLojaSqlResolverFactory;
            _visitorCriacaoFuncao = visitorCriacaoFuncao;
        }

        public ListaProdutosLoja GetSomenteUmEndereco(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina)
        {
            IListaProdutosLojaSqlResolver sqlResolver = _listaProdutosLojaSqlResolverFactory.Get(pesquisaProdutoContrato);
            long total = pesquisaProdutoContrato.TotalElementosLista;
            if (total == 0)
            {
                total = sqlResolver.GetConsultaDeContagem();

                if (total == 0)
                {
                    return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
                }
            }

            IList<ListaProdutosLojaContrato> lista = sqlResolver.Get(pesquisaProdutoContrato.PaginaAtualLista, tamanhoPagina);

            TotalElementosLista listaTotalElementos = new TotalElementosLista(total);
            PaginaAtualLista listaElementoAtual = new PaginaAtualLista(pesquisaProdutoContrato.PaginaAtualLista);
            if (!lista.Any())
            {
                return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
            }
            return new ListaProdutosLoja(lista, listaTotalElementos, listaElementoAtual);
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
