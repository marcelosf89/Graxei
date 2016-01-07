using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{

    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome).Column(Constantes.NOME);
            Map(p => p.Sigla).Column(Constantes.SIGLA);
        }
    }

}
