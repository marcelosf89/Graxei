﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Administrativo/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}