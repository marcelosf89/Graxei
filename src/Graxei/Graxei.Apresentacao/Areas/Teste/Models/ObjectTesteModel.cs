﻿using System;
using System.Web.Routing;

namespace Graxei.Apresentacao.Areas.Teste.Models
{
    public class ObjectTesteModel
    {
        public String View { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public RouteValueDictionary Route { get; set; }
    }
}