using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class LogradouroMap : ClassMap<Logradouro>
    {
        public LogradouroMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome).Column(Constantes.NOME);
            Map(p => p.CEP).Column(Constantes.CEP);
            References(p => p.Bairro).Fetch.Join();
        }
    }
}