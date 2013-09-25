using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class CidadeMap : ClassMap<Cidade>
    {

        public CidadeMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome);
            References(p => p.Estado).Fetch.Join();
        }
    }
}
