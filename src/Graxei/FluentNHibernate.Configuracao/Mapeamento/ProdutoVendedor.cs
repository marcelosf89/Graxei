﻿using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class ProdutoVendedorMap : ClassMap<ProdutoVendedor>
    {
        public ProdutoVendedorMap()
        {
            Id(p => p.Id);
            Map(p => p.Preco);
            References(p => p.Produto);
            References(p => p.UnidadeEntrada).Column(Constantes.ID_UNIDADE_ENTRADA).Cascade.All();
            References(p => p.UnidadeSaida).Column(Constantes.ID_UNIDADE_SAIDA).Cascade.All();
        }
    }
}