using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.NHibernate;
using Graxei.Transversais.ContratosDeDados;
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
    public class PesquisaProdutoRepositorio : IRepositorioPesquisaProduto
    {
        public ListaPesquisaContrato GetUltimaPagina(int tamanhoPagina, string texto, string pais, string cidade)
        {
            string descricaoTrim = texto.Replace(" ", "");
            double criterioSimilaridade = descricaoTrim.Length * 0.0074;

            string sql = @"
                select count(p.id_produto) from produtos p 
                  join produtos_vendedores pv on p.id_produto = pv.id_produto
                 where similarity(p.descricao || ' ' || p.codigo,:descricao)  > :val";
              long total = SessaoAtual.CreateSQLQuery(sql)
                .SetParameter<String>("descricao", descricaoTrim)
                .SetParameter<double>("val", criterioSimilaridade)
                .UniqueResult<long>();

            int ultimaPagina = Convert.ToInt32(total / tamanhoPagina);
            IList<PesquisaContrato> lista = GetPorDescricaoPesquisa(texto, pais, cidade, ultimaPagina);

            TotalElementosLista totalElementos = new TotalElementosLista(total);
            PaginaAtualLista elementoAtual = new PaginaAtualLista(ultimaPagina);
            return new ListaPesquisaContrato(lista, totalElementos, elementoAtual);
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
                select pv.id_produto_vendedor as ""Id"", p.descricao ""DescricaoPadrao"", pv.Descricao ""MinhaDescricao"",  p.Codigo ""Codigo"",
                       pv.Preco ""Preco"", pv.id_produto ""ProdutoId"", pv.id_endereco ""EnderecoId""
                  from produtos p 
                  join produtos_vendedores pv on p.id_produto = pv.id_produto
                  join enderecos en on en.id_endereco = pv.id_endereco
                  join lojas l on l.id_loja = en.id_loja
                 where similarity(p.descricao || ' ' || p.codigo,:descricao)  > :val {0}
              order by similarity(p.descricao || ' ' || p.codigo,:descricao) desc
                ";

            sql = string.Format(sql, lojasql);

            return SessaoAtual.CreateSQLQuery(sql)
                                   .SetResultTransformer(Transformers.AliasToBean(typeof(PesquisaContrato)))
                                   .SetParameter<String>("descricao", descricao)
                                   .SetParameter<double>("val", textoL)
                                   .SetFirstResult((page * 10) < 0 ? 1 : (page * 10))
                                   .SetMaxResults(10)
                                   .List<PesquisaContrato>();
        }

        private static string GetLojaNaDescricao(String descricao)
        {
            String loja = "";
            if (descricao.ToLower().Contains("loja:"))
            {
                int idxOfLoja = descricao.ToLower().IndexOf("loja:");
                int nIdxOf = descricao.Substring(idxOfLoja).IndexOf(' ');

                if (nIdxOf <= 0)
                    loja = descricao.Substring(idxOfLoja + 5);
                else
                    loja = descricao.Substring(idxOfLoja + 5, nIdxOf - 5);
            }
            return loja;
        }

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
