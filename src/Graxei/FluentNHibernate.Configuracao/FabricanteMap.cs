using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class FabricanteMap : ClassMap<Fabricante>
    {
        public FabricanteMap()
        {
            Id(p => p.Id).Column(Constantes.ID_FABRICANTE);
            Map(p => p.Nome).Column(Constantes.NOME);
        }
    }
}
