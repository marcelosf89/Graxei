using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Infrastructure.Filters
{

    public class AjaxGenericExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                PartialViewResult result = new PartialViewResult { ViewName = "AjaxError" };
                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}