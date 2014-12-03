using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class LojaMap : ClassMap<Loja>
    {
        public LojaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome).Column("nome");
            Map(p => p.Logotipo).Column("logotipo");
            Map(p => p.Excluida).Column("excluida");
            References(p => p.Plano).Column(Constantes.ID_PLANO).Not.Nullable();
            HasMany(p => p.Enderecos).KeyColumn(Constantes.ID_LOJA).Cascade.None();
            HasManyToMany(p => p.Usuarios).ParentKeyColumn(Constantes.ID_LOJA).ChildKeyColumn(Constantes.ID_USUARIO).Cascade.All().Table(Constantes.LOJAS_USUARIOS).
            Where("excluida = false");
        }
    }
}
