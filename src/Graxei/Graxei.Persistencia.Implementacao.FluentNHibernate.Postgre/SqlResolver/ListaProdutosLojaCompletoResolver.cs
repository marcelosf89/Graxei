using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.Constantes;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver
{
    public class ListaProdutosLojaCompletoResolver : IListaProdutosLojaSqlResolver
    {
        private const string Consulta =
                  @"FROM (
                  SELECT p.id_produto, p.codigo, null minha_descricao, p.descricao descricao_original, 
                         null id_meu_produto, null id_endereco, null preco, false excluido
                    FROM produtos p 
                   WHERE p.excluida = false AND lower(p.descricao) LIKE :descricao 
                     AND NOT EXISTS (SELECT 1 FROM produtos_vendedores pv WHERE pv.id_produto = p.id_produto)
                   UNION
                  SELECT p.id_produto, p.codigo, pv.descricao minha_descricao, p.descricao descricao_original, 
                         pv.id_produto_vendedor id_meu_produto, pv.id_endereco, pv.preco, pv.excluida
                    FROM produtos p
                    JOIN produtos_vendedores_ativos pv on p.id_produto = pv.id_produto
                    JOIN enderecos e ON pv.id_endereco = e.id_endereco
                    JOIN lojas l ON e.id_loja = l.id_loja 
                   WHERE l.id_loja = :id AND (lower(pv.descricao) like :descricao OR lower(p.descricao) like :descricao)) p";

        private const string Ordem = "ORDER BY p.minha_descricao";

        private ISession _sessao;
        
        private long _idLoja;
        
        private string _criterio;

        public ISession SessaoAtual
        {
            get
            {
                if (_sessao == null)
                {
                    _sessao = UnitOfWorkNHibernate.GetInstancia().SessaoAtual;
                }

                return _sessao;
            }
            set
            {
                _sessao = value;
            }
        }

        public ListaProdutosLojaCompletoResolver(PesquisaProdutoContrato pesquisaProdutoContrato)
        {
            _idLoja = pesquisaProdutoContrato.IdLoja;
            if (string.IsNullOrEmpty(pesquisaProdutoContrato.DescricaoProduto))
            {
                throw new ArgumentException("Critério deve ser preenchido");
            }
            _criterio = pesquisaProdutoContrato.DescricaoProduto;
        }

        public IList<ListaProdutosLojaContrato> Get(int pagina, int tamanhoPagina)
        {
            string sql = string.Format("{0} {1} {2}", ListaProdutosSqlResolverConstantes.Campos, Consulta, Ordem);
            int primeiroResultado = (tamanhoPagina * pagina) - tamanhoPagina;
            IList<ListaProdutosLojaContrato> lista = 
                 SessaoAtual.CreateSQLQuery(sql)
                            .SetResultTransformer(Transformers.AliasToBean(typeof(ListaProdutosLojaContrato)))
                            .SetParameter<long>("id", _idLoja)
                            .SetParameter<string>("descricao", string.Format("{0}{1}{2}", "%", _criterio.ToLower(), "%"))
                            .SetFirstResult(primeiroResultado)
                            .SetMaxResults(tamanhoPagina)
                            .List<ListaProdutosLojaContrato>();
            return lista;
        }

        public long GetConsultaDeContagem()
        {
            string campos = @"SELECT COUNT(p.id_produto) contador";
            string sql =  string.Format("{0} {1}", campos, Consulta);
            long total =  SessaoAtual.CreateSQLQuery(sql)
                                    .SetParameter<long>("id", _idLoja)
                                    .SetParameter<string>("descricao", string.Format("{0}{1}{2}", "%", _criterio.ToLower(), "%"))
                                    .UniqueResult<long>();
            return total;
        }
    }
}
