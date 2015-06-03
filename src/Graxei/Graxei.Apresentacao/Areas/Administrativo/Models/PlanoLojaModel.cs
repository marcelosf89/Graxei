using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class PlanoLojaModel
    {
        public Graxei.Modelo.Plano PlanoLojaContratado { get; set; }
        public IList<Graxei.Modelo.Plano> Planos { get; set; }
    }
}