using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Transversais.ContratosDeDados;
using NHibernate;
using System.Collections.Generic;
using System.Data;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo
{
    public class ProdutoVendedorNativo : IProdutoVendedorNativo
    {
        public IList<ProdutoLojaPrecoContrato> Get(string sql)
        {
            List<ProdutoLojaPrecoContrato> resultado = new List<ProdutoLojaPrecoContrato>();
            if (string.IsNullOrEmpty(sql))
            {
                return resultado;
            }

            using (IDbCommand command = GetSessaoAtual().Connection.CreateCommand())
            {
                command.CommandText = sql;
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProdutoLojaPrecoContrato produto = new ProdutoLojaPrecoContrato();
                    produto.IdMeuProduto = (long)reader["id_produto_vendedor"];
                    produto.Id = (long)reader["id_produto"];
                    resultado.Add(produto);
                }
            };

            return resultado;

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


        private ISession _sessao;
    }
}
