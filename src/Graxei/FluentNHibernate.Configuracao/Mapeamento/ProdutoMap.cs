using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Codigo);
            Map(p => p.Descricao);
            Map(p => p.FatorConversao).Column(Constantes.FATOR_CONVERSAO);
            References(p => p.Categoria).Cascade.All();
            References(p => p.Fabricante).Cascade.All();
            Map(p => p.Carros);
            Map(p => p.Observacao);

        }
    }
}