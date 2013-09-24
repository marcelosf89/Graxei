using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TwitterBootstrapMVC.BootstrapMethods;

namespace TwitterBootstrapMVC
{
    public static class BootstrapAjaxExtension
    {
        public static AjaxBootstrap<TModel> Bootstrap<TModel>(this AjaxHelper<TModel> helper)
        {
            return new AjaxBootstrap<TModel>(helper);
        }
    }
}
