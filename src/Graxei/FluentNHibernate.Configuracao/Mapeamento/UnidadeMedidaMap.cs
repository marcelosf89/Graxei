using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class UnidadeMedidaMap : ClassMap<UnidadeMedida>
    {
        public UnidadeMedidaMap()
        {
            Table(Constantes.UNIDADES_MEDIDA);
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Sigla);
            Map(p => p.Descricao);
        }
    }
}
