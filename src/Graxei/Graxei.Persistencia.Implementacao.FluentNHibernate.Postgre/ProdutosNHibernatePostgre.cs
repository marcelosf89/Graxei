using System;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    /// <summary>
    /// Classe de implementação das funções relativas à entidade Produto
    /// </summary>
    public class ProdutosNHibernatePostgre : PadraoNHibernatePostgreLeitura<Produto>, IRepositorioProdutos
    {
        #region Implementação de IRepositorioProdutos

        public Produto GetPorDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                throw new ArgumentNullException("descricao");
            }
            return SessaoAtual.Query<Produto>()
                              .SingleOrDefault<Produto>(p => p.Descricao.Trim().ToLower() == descricao);
        }

        #endregion


        public System.Collections.Generic.IList<Produto> Get(string texto, long page)
        {
            String textos = texto.Replace(" ", "");
            double textoL = textos.Length * 0.0074;


            String sql = @"
                select p.* from produtos p 
                join fabricantes f on p.id_fabricante = f.id_fabricante 
                where similarity(p.descricao || ' ' || p.codigo || ' ' || f.nome,:descricao)  > :val
                order by similarity(p.descricao || ' ' || p.codigo || ' ' || f.nome || ' ' || p.carros || ' ' || p.observacao,:descricao) desc";
            return SessaoAtual.CreateSQLQuery(sql)
                .AddEntity(typeof(Produto))
                .SetParameter<String>("descricao", texto)
                .SetParameter<double>("val", textoL)
                .SetFirstResult(Convert.ToInt32((page * 10) < 0 ? 1 : (page * 10)))
          .SetMaxResults(10)
                .List<Produto>();
        }

        public long GetMax(string texto)
        {
            String textos = texto.Replace(" ", "");
            double textoL = textos.Length * 0.0074;
            String sql = @"
                select count(p.id_produto) from produtos p 
                join fabricantes f on p.id_fabricante = f.id_fabricante 
                where similarity(p.descricao || ' ' || p.codigo || ' ' || f.nome || ' ' || p.carros || ' ' || p.observacao,:descricao)  > :val
                ";
            return SessaoAtual.CreateSQLQuery(sql)
                .SetParameter<String>("descricao", texto)
                .SetParameter<double>("val", textoL)
                .UniqueResult<long>();
        }
    }

}
