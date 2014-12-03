using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class AtributoMap : ClassMap<Atributo>
    {
        public AtributoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity(); ;
            Map(p => p.Nome);
            Map(p => p.Rotulo);
            Map(p => p.Tamanho);
            Map(p => p.Excluida);
            References(p => p.ProdutoVendedor).Column(Constantes.ID_PRODUTO_VENDEDOR);
            Where("excluida = false");
        }
    }
}