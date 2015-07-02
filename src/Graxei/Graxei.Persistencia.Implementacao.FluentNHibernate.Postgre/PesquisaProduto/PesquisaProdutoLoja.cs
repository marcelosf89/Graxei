using Graxei.Persistencia.Contrato.PesquisaProduto;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.PesquisaProduto
{
    public class PesquisaProdutoLoja : AbstractPesquisaProduto
    {
        public PesquisaProdutoLoja(string criterio, string loja) : base(criterio)
        {
            _loja = loja;
        }

        public override IList<PesquisaContrato> Get(int pagina)
        {
            string textos = _criterio.Replace(" ", "");
            double textoL = textos.Length * 0.0074;

            string lojasql = "and (l.nome = :loja or l.url = :loja)";
            string sql = @"
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
                                   .SetParameter<String>("descricao", _criterio)
                                   .SetParameter<double>("val", textoL)
                                   .SetParameter("loja", _loja)
                                   .SetFirstResult((pagina * 10) < 0 ? 1 : (pagina * 10))
                                   .SetMaxResults(10)
                                   .List<PesquisaContrato>();
        }

        private string _loja;
    }
}
