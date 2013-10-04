using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class LojaMap : ClassMap<Loja>
    {
        public LojaMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome).Column(Constantes.NOME);
            Map(p => p.Logotipo).Column(Constantes.LOGOTIPO);
            HasMany(p => p.Enderecos).KeyColumn(Constantes.ID_LOJA).Cascade.All();
        }
    }
}
