﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class HomeController : Controller
    {
        private DateTime date;
        //
        // GET: /Administrativo/Home/
        
        public ActionResult Index()
        {
            return View();
        }

    }
}
