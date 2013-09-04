using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Table(Constantes.ESTADOS);
            Id(p => p.Id);
            Map(p => p.Nome, Constantes.NOME);
            Map(p => p.Sigla, Constantes.SIGLA);
        }
    }

}
