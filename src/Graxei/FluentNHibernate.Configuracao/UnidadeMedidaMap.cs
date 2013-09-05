using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Configuracao
{
    public class UnidadeMedidaMap : ClassMap<UnidadeMedida>
    {
        public UnidadeMedidaMap()
        {
            Table(Constantes.UNIDADES_MEDIDA);
            Id(p => p.Id).Column(Constantes.ID_UNIDADE_MEDIDA);
            Map(p => p.Sigla, Constantes.SIGLA);
            Map(p => p.Descricao, Constantes.DESCRICAO);
        }
    }
}
