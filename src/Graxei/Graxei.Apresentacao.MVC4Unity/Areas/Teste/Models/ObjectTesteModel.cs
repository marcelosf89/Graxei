using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Teste.Models
{
    public class ObjectTesteModel
    {
        public String View { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public object Route { get; set; }
    }
}