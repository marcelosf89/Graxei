using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using NHibernate;
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
        public ListaProdutosLoja GetSomenteUmEndereco(int idLoja, int pagina, int tamanhoPagina, int totalElementos)
        {
            int total = totalElementos;
            if (totalElementos == 0)
            {
                total = SessaoAtual.QueryOver<ProdutoVendedor>()
                                       .JoinQueryOver<Endereco>(p => p.Endereco)
                                       .JoinQueryOver<Loja>(p => p.Loja).Where(p => p.Id == idLoja).RowCount();
            }

            if (total == 0)
            {
                return new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new ListaTotalElementos(0), new ListaElementoAtual(0));
            }

            string sql = @"SELECT pv.id_produto_vendedor ""Id"", l.nome ""Descricao"", pv.preco ""Preco""
                             FROM produtos p
                             JOIN produtos_vendedores pv ON p.id_produto = pv.id_produto
                             JOIN enderecos e ON pv.id_endereco = e.id_endereco
                             JOIN lojas l ON e.id_loja = l.id_loja
                            WHERE l.id = :id";

            IList<ListaProdutosLojaContrato> lista = 
                    SessaoAtual.CreateSQLQuery(sql)
                               .SetResultTransformer(Transformers.AliasToBean(typeof(ListaProdutosLojaContrato)))
                               .SetParameter<int>("id", idLoja)
                               .SetFirstResult(pagina)
                               .SetMaxResults(tamanhoPagina)
                               .List<ListaProdutosLojaContrato>();

            ListaTotalElementos listaTotalElementos = new ListaTotalElementos(total);
            ListaElementoAtual listaElementoAtual = new ListaElementoAtual(pagina);
            return new ListaProdutosLoja(lista, listaTotalElementos, listaElementoAtual);
        }

        public ListaProdutosLoja GetSomenteUmEndereco(int idLoja, int pagina, int tamanhoPagina)
        {
            return GetSomenteUmEndereco(idLoja, pagina, tamanhoPagina, 0);
        }

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
