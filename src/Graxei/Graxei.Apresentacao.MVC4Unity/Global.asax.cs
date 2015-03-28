﻿using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Binders;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Binders;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using Microsoft.Practices.Unity;
using StackExchange.Profiling;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Graxei.Apresentacao.MVC4Unity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ModelBinders.Binders.Add(typeof (Usuario), new UsuariosLogadoBinder());
            ModelBinders.Binders.Add(typeof(LogotipoNovaLojaModel), new LogoNovaLojaBinder());
            /* TODO: retirar esse trecho de código */
            IUnityContainer cont = Bootstrapper.Initialise();
            /*IServicoUsuarios usu = cont.Resolve<IServicoUsuarios>();
            Usuario usuario = usu.GetPorLogin("graxeiadmin");
            Session[Constantes.UsuarioAtual] = usuario;*/
            
            //BundleTable.EnableOptimizations = true;
            //HttpCachePolicy cachePolicy = HttpContext.Current.Response.Cache;
            //cachePolicy.SetCacheability(BundleTable);
            //cachePolicy.SetOmitVaryStar(true);
            //cachePolicy.SetExpires(DateTime.Now.AddDays(1));
            //cachePolicy.SetValidUntilExpires(true);
            //cachePolicy.SetLastModified(DateTime.Now);
            //cachePolicy.VaryByHeaders["User-Agent"] = true;
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                //// MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            ////MiniProfiler.Stop();
        }
    }
}