using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.Constantes
{
    public class ListaProdutosSqlResolverConstantes
    {
        public const string CamposMeusProdutos= @"SELECT p.id_produto ""Id"", p.codigo ""Codigo"", pv.descricao ""MinhaDescricao"", p.descricao ""DescricaoOriginal"", pv.id_produto_vendedor ""IdMeuProduto"", e.id_endereco ""IdEndereco"", pv.preco ""Preco"", pv.excluida ""Excluido""";

        public const string Campos =
                @"SELECT p.id_produto ""Id"", p.codigo ""Codigo"", p.minha_descricao ""MinhaDescricao"", p.descricao_original ""DescricaoOriginal"", 
                         p.id_meu_produto ""IdMeuProduto"", p.id_endereco ""IdEndereco"", p.preco ""Preco"", p.excluido ""Excluido""";
    }
}
