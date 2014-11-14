using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class ProdutoVendedorMap : ClassMap<ProdutoVendedor>
    {
        public ProdutoVendedorMap()
        {
            Table(Constantes.PRODUTOS_VENDEDORES);
            Id(p => p.Id).Column(Constantes.ID_PRODUTO_VENDEDOR);
            Map(p => p.Descricao);
            Map(p => p.FatorConversao).Column(Constantes.FATOR_CONVERSAO);
            Map(p => p.Preco);
            Map(p => p.Excluida);
            References(p => p.Produto).Cascade.SaveUpdate();
            References(p => p.Endereco).Column(Constantes.ID_ENDERECO);
            References(p => p.UnidadeEntrada).Column(Constantes.ID_UNIDADE_ENTRADA).Cascade.All();
            References(p => p.UnidadeSaida).Column(Constantes.ID_UNIDADE_SAIDA).Cascade.All();
            HasMany(p => p.Atributos).Cascade.All();
            Where("excluida = false");
        }
    }
}
