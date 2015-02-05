﻿using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.Constantes;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
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
                     @"FROM produtos p
                  LEFT JOIN produtos_vendedores pv ON p.id_produto = pv.id_produto
                  LEFT JOIN enderecos e ON pv.id_endereco = e.id_endereco
                  LEFT JOIN lojas l ON e.id_loja = l.id_loja AND l.id_loja = :id 
                      WHERE (lower(p.descricao) like :descricao OR lower(pv.descricao) like :descricao)";

        private const string Ordem = "ORDER BY pv.descricao";

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

        public ListaProdutosLojaCompletoResolver(long idLoja, string criterio)
        {
            _idLoja = idLoja;
            if (string.IsNullOrEmpty(criterio))
            {
                throw new ArgumentException("Critério deve ser preenchido");
            }
            _criterio = criterio;
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
