using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class CidadeMap : ClassMap<Cidade>
    {

        public CidadeMap()
        {
            Table(Constantes.CATEGORIAS);
            Id(p => p.Id).Column(Constantes.ID_CIDADE);
            Map(p => p.Nome, Constantes.NOME);
            References(p => p.Estado).Column("ID_ESTADO");
        }
    }
}
