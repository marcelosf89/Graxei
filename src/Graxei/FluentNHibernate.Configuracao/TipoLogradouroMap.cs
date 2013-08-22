using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class TipoLogradouroMap : ClassMap<TipoLogradouro>
    {
        public TipoLogradouroMap()
        {
            Id(p => p.Id).Column(Constantes.ID_TIPO_LOGRADOURO);
            Map(p => p.Sigla).Column(Constantes.SIGLA);
            Map(p => p.Nome).Column(Constantes.NOME);
        }
    }
}
