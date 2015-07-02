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
    public class PesquisaProdutoSimples : AbstractPesquisaProduto
    {
        public PesquisaProdutoSimples(string criterio) : base(criterio)
        {
        }

        public override IList<PesquisaContrato> Get(int pagina)
        {
            string textos = _criterio.Replace(" ", "");
            double textoL = textos.Length * 0.0074;

            string sql = @"
                select pv.id_produto_vendedor as ""Id"", p.descricao ""DescricaoPadrao"", pv.Descricao ""MinhaDescricao"",  p.Codigo ""Codigo"",
                       pv.Preco ""Preco"", pv.id_produto ""ProdutoId"", pv.id_endereco ""EnderecoId""
                  from produtos p 
                  join produtos_vendedores pv on p.id_produto = pv.id_produto
                 where similarity(p.descricao || ' ' || p.codigo,:descricao)  > :val
              order by similarity(p.descricao || ' ' || p.codigo,:descricao) desc";

            return SessaoAtual.CreateSQLQuery(sql)
                                   .SetResultTransformer(Transformers.AliasToBean(typeof(PesquisaContrato)))
                                   .SetParameter<String>("descricao", _criterio)
                                   .SetParameter<double>("val", textoL)
                                   .SetFirstResult((pagina * 10) < 0 ? 1 : (pagina * 10))
                                   .SetMaxResults(10)
                                   .List<PesquisaContrato>();
        }

    }
}
