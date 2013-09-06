using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class UnidadeMedidaMap : ClassMap<UnidadeMedida>
    {
        public UnidadeMedidaMap()
        {
            Id(p => p.Id);
            Map(p => p.Sigla).Column(Constantes.SIGLA);
            Map(p => p.Descricao).Column(Constantes.DESCRICAO);
        }
    }
}
