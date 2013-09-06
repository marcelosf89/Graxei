using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class CidadeMap : ClassMap<Cidade>
    {

        public CidadeMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome);
            References(p => p.Estado).Column(Constantes.ID_ESTADO);
        }
    }
}
