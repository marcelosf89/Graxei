using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Id(p => p.Id);
            Map(p => p.Codigo);
            Map(p => p.Descricao);
            Map(p => p.FatorConversao).Column(Constantes.FATOR_CONVERSAO);
            References(p => p.Categoria).Cascade.All();
            References(p => p.Fabricante).Cascade.All();
        }
    }
}