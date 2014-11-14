using FluentNHibernate.Mapping;
using Graxei.Modelo;
namespace Graxei.FluentNHibernate.Mapeamento
{
    public class PlanoMap : ClassMap<Plano>
    {
        public PlanoMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome);
            Map(p => p.QuantidadeProduto);
            Map(p => p.QuantidadeFilial);
            Map(p => p.Valor);
            Map(p => p.Meses);
            Map(p => p.EstaAtivo);            
        }
    }
}
