
using Graxei.Apresentacao.MVC4Unity.Areas.Teste.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Teste.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Teste/Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarregarPagina(String txtRota)
        {
            String[] obj = txtRota.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            String[] vals = obj[0].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);


            ObjectTesteModel obm = new ObjectTesteModel();
            obm.Area = vals.Length > 2 ? vals[0] : "";
            obm.Controller = vals.Length > 2 ? vals[1] : vals[0];
            obm.View = vals.Length > 2 ? vals[2] : vals[1]; ;

            RouteValueDictionary rvd = new RouteValueDictionary();

           rvd.Add("Controller", obm.Controller);
            rvd.Add("Area", obm.Area);

            if (obj.Length > 1)
            {
                vals = obj[1].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (String item in vals)
                {
                    string[] splitValor = item.Split('=');
                    rvd.Add(splitValor[0], splitValor[1]);
                }
            }
            obm.Route = rvd;
            return View(obm);
        }


    }
}
