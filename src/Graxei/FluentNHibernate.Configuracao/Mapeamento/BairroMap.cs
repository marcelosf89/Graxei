using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class BairroMap : ClassMap<Bairro>
    {
        public BairroMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome).Column(Constantes.NOME); ;
            References(p => p.Cidade).Fetch.Join();
        }
    }
}