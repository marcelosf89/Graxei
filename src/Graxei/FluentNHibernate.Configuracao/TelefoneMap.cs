using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class TelefoneMap : ClassMap<Telefone>
    {
        public TelefoneMap()
        {
            Id(p => p.Id).Column(Constantes.ID_TELEFONE);
            Map(P => P.Numero).Column(Constantes.NUMERO);
            References(p => p.Endereco).Column(Constantes.ID_ENDERECO);
            References(p => p.TipoTelefone).Column(Constantes.ID_TIPO_TELEFONE);
        }
    }
}
