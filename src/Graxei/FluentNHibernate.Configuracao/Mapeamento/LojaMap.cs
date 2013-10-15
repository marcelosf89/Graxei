using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class LojaMap : ClassMap<Loja>
    {
        public LojaMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome);
            Map(p => p.Logotipo);
            Map(p => p.Excluida);
            HasMany(p => p.Enderecos).KeyColumn(Constantes.ID_LOJA).Cascade.All();
            Where("excluida = false");
        }
    }
}
