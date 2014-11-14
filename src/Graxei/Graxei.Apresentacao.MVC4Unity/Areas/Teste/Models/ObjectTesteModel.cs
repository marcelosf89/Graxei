using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Teste.Models
{
    public class ObjectTesteModel
    {
        public String View { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public RouteValueDictionary Route { get; set; }
    }
}