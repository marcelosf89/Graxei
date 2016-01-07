using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Graxei.Apresentacao.Extension
{
    public static partial class MVCExtensionComponent
    {
        public static MvcHtmlString IconActionLink(this AjaxHelper helper, string icon, string text, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            var builder = new TagBuilder("i");
            builder.MergeAttribute("class", icon);
            var link = helper.ActionLink("[replaceme] " + text, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString()));
        }

        public static MvcHtmlString IconActionLink(this AjaxHelper helper, string icon, string text, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var builder = new TagBuilder("i");
            builder.MergeAttribute("class", icon);
            var link = helper.ActionLink("[replaceme] " + text, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString()));
        }

        public static MvcHtmlString ImageActionLink(this AjaxHelper helper, ImageTag img , string text, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", img.Src);
            builder.MergeAttribute("style", "height: "+img.Height+"px; width: "+img.Width+"px;");
            var link = helper.ActionLink("[replaceme] " + text, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString()));
        }


    }

    public class ImageTag{
        public String Src { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}