using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TwitterBootstrapMVC.Controls;

namespace TwitterBootstrapMVC.BootstrapMethods
{
    public partial class AjaxBootstrap<TModel>
    {
        public AjaxBootstrapActionLinkButton ActionLinkButton(string linkText, AjaxOptions result)
        {
            return new AjaxBootstrapActionLinkButton(Ajax, linkText, result);
        }

        public AjaxBootstrapActionLinkButton ActionLinkButton(string linkText, string actionName)
        {
            return new AjaxBootstrapActionLinkButton(Ajax, linkText, actionName);
        }

        public AjaxBootstrapActionLinkButton ActionLinkButton(string linkText, string actionName, string controllerName)
        {
            return new AjaxBootstrapActionLinkButton(Ajax, linkText, actionName, controllerName);
        }

        public AjaxBootstrapActionLinkButton ActionLinkButton(string linkText, string actionName, string controllerName, AjaxOptions result)
        {
            return new AjaxBootstrapActionLinkButton(Ajax, linkText, actionName, controllerName, result);
        }


        public AjaxBootstrapActionLink ActionLink(string linkText, AjaxOptions result)
        {
            return new AjaxBootstrapActionLink(Ajax, linkText, result);
        }

        public AjaxBootstrapActionLink ActionLink(string linkText, string actionName)
        {
            return new AjaxBootstrapActionLink(Ajax, linkText, actionName);
        }

        public AjaxBootstrapActionLink ActionLink(string linkText, string actionName, string controllerName)
        {
            return new AjaxBootstrapActionLink(Ajax, linkText, actionName, controllerName);
        }

        public AjaxBootstrapActionLink ActionLink(string linkText, string actionName, string controllerName, AjaxOptions result)
        {
            return new AjaxBootstrapActionLink(Ajax, linkText, actionName, controllerName, result);
        }
    }
}
