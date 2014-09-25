﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Graxei.Apresentacao.MVC4Unity
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new
                          {
                              controller = "Login",
                              action = "Index",
                              id = UrlParameter.Optional
                          },
                          namespaces: new string[] { "Graxei.Apresentacao.MVC4Unity.Controllers" });
        //namespaces: new string[] {"Graxei.Apresentacao.MVC4Unity.Controllers"});

            routes.MapRoute(null,
                            "Administrativo/Enderecos/Excluir/{id}",
                            new {controller = "Enderecos", action = "Excluir", idList = (int) 0});



        }
    }
}