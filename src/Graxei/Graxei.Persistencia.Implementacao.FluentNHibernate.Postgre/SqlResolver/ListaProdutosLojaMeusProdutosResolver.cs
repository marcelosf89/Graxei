using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver
{
    public class ListaProdutosLojaMeusProdutosResolver : IListaProdutosLojaSqlResolver
    {
        public string Get()
        {
            return @"SELECT pv.id_produto_vendedor ""Id"", p.codigo ""Codigo"", pv.descricao ""Descricao"", pv.id_produto_vendedor ""IdMeuProduto"", pv.preco ""Preco""
                             FROM produtos p
                             JOIN produtos_vendedores pv ON p.id_produto = pv.id_produto
                             JOIN enderecos e ON pv.id_endereco = e.id_endereco
                             JOIN lojas l ON e.id_loja = l.id_loja
                            WHERE l.id_loja = :id AND (lower(p.descricao) like :descricao OR lower(pv.descricao) like :descricao)
                         ORDER BY pv.descricao";
        }
    }
}
