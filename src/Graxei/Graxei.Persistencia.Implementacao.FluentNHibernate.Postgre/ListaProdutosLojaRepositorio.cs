using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
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

        public ListaProdutosLoja GetSomenteUmEndereco(string criterio, long idLoja, int pagina, int tamanhoPagina, int totalElementos)
        {
            int total = totalElementos;
            if (totalElementos == 0)
            {
                total = GetSessaoAtual().QueryOver<ProdutoVendedor>()
                                        .Where(Restrictions.InsensitiveLike(Projections.Property<ProdutoVendedor>(p => p.Descricao),
                                                                            criterio.ToLower(), MatchMode.Anywhere))
                                        .JoinQueryOver<Endereco>(p => p.Endereco)
                                        .JoinQueryOver<Loja>(p => p.Loja)
                                        .Where(p => p.Id == idLoja)
                                        .RowCount();
            }

            if (total == 0)
            {
                return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new ListaTotalElementos(0), new ListaElementoAtual(0));
            }

            string sql = @"SELECT pv.id_produto_vendedor ""Id"", p.codigo ""Codigo"", pv.descricao ""Descricao"", pv.preco ""Preco""
                             FROM produtos p
                             JOIN produtos_vendedores pv ON p.id_produto = pv.id_produto
                             JOIN enderecos e ON pv.id_endereco = e.id_endereco
                             JOIN lojas l ON e.id_loja = l.id_loja
                            WHERE l.id_loja = :id AND lower(p.descricao) like :descricao
                         ORDER BY pv.descricao";

            IList<ListaProdutosLojaContrato> lista = 
               GetSessaoAtual().CreateSQLQuery(sql)
                               .SetResultTransformer(Transformers.AliasToBean(typeof(ListaProdutosLojaContrato)))
                               .SetParameter<long>("id", idLoja)
                               .SetParameter<string>("descricao", string.Format("{0}{1}{2}", "%", criterio.ToLower(), "%"))
                               .SetFirstResult(pagina)
                               .SetMaxResults(tamanhoPagina)
                               .List<ListaProdutosLojaContrato>();

            ListaTotalElementos listaTotalElementos = new ListaTotalElementos(total);
            ListaElementoAtual listaElementoAtual = new ListaElementoAtual(pagina);
            if (!lista.Any())
            {
                return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new ListaTotalElementos(0), new ListaElementoAtual(0));
            }
            return new ListaProdutosLoja(lista, listaTotalElementos, listaElementoAtual);
        }

        public ListaProdutosLoja GetSomenteUmEndereco(string criterio, long idLoja, int pagina, int tamanhoPagina)
        {
            return GetSomenteUmEndereco(criterio, idLoja, pagina, tamanhoPagina, 0);
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
