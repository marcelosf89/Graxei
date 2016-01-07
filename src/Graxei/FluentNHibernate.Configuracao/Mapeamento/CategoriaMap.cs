using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity(); ;
            Map(p => p.Nome).Column(Constantes.NOME);
            References(p => p.CategoriaPai).Column(Constantes.ID_CATEGORIA_PAI);
        }
    }
}
