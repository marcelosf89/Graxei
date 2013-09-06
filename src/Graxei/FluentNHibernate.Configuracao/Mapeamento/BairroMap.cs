using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class BairroMap : ClassMap<Bairro>
    {
        public BairroMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome).Column(Constantes.NOME); ;
            References(p => p.Cidade).Column(Constantes.ID_CIDADE);
        }
    }
}