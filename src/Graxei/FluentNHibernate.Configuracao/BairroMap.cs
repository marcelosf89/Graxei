using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class BairroMap : ClassMap<Bairro>
    {
        public BairroMap()
        {
            Id(p => p.Id).Column(Constantes.ID_BAIRRO);
            Map(p => p.Nome, Constantes.NOME);
            References(p => p.Cidade);
        }
    }
}
