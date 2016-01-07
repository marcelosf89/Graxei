using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class FabricanteMap : ClassMap<Fabricante>
    {
        public FabricanteMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome).Column(Constantes.NOME);
        }
    }
}
