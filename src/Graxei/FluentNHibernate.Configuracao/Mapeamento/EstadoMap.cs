using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome);
            Map(p => p.Sigla);
        }
    }

}
