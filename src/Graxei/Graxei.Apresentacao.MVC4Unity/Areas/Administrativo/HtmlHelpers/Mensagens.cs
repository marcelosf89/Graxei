using System.Linq;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.HtmlHelpers
{
    public static class Mensagens
    {
        public static MvcHtmlString Sucesso(this HtmlHelper html)
        {
            if (html.ViewBag.OperacaoSucesso != null)
            {
                TagBuilder button = CreateCommonDismissableButton();
                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("alert alert-success alert-dismissable");
                div.InnerHtml = string.Concat(button.ToString(), html.ViewBag.OperacaoSucesso.ToString());
                return new MvcHtmlString(div.ToString());
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString Erro(this HtmlHelper html)
        {
            ModelState modelState = html.ViewData.ModelState[string.Empty];
            if (modelState != null && modelState.Errors != null && modelState.Errors.Any())
            {
                TagBuilder button = CreateCommonDismissableButton();
                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("alert alert-danger alert-dismissable");
                string errorMessage = modelState.Errors[0].ErrorMessage;
                div.InnerHtml = string.Concat(button.ToString(), errorMessage);
                return new MvcHtmlString(div.ToString());
            }
            return new MvcHtmlString(string.Empty);
        }

        private static TagBuilder CreateCommonDismissableButton()
        {
            TagBuilder button = new TagBuilder("button");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-dismiss", "alert");
            button.Attributes.Add("aria-hidden", "true");
            button.AddCssClass("close");
            button.InnerHtml = "&times;";
            return button;
        }
    }
}