using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class TipoLogradouroMap : ClassMap<TipoLogradouro>
    {
        public TipoLogradouroMap()
        {
            Table(Constantes.TIPOS_LOGRADOURO);
            Id(p => p.Id);
            Map(p => p.Sigla);
            Map(p => p.Nome);
        }
    }
}
