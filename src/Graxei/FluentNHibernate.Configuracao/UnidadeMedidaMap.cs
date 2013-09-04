using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class UnidadeMedidaMap : UnidadeMedida
    {
        public virtual long Id { get; set; }
        public virtual string Sigla { get; set; }
        public virtual string Descricao { get; set; }
    }
}
