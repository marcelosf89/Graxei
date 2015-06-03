using System.Linq;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Areas.Administrativo.HtmlHelpers
{
    public static class Mensagens
    {
        public static MvcHtmlString Sucesso(this HtmlHelper html)
        {
            if (html.ViewBag.OperacaoSucesso != null)
            {
                TagBuilder button = CreateCommonDismissableButton();

                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("alert alert-success row");
                div.Attributes.Add("role", "alert");

                TagBuilder div2 = new TagBuilder("div");
                div2.AddCssClass("col-md-1");

                TagBuilder img = new TagBuilder("i");
                img.AddCssClass("glyphicon glyphicon-ok");
                div2.InnerHtml = img.ToString();

                TagBuilder div3 = new TagBuilder("div");
                div3.AddCssClass("col-md-10");

                div3.InnerHtml = html.ViewBag.OperacaoSucesso.ToString();

                div.InnerHtml = button.ToString();
                div.InnerHtml += div2.ToString();
                div.InnerHtml += div3.ToString();

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
                div.AddCssClass("alert alert-danger row");
                div.Attributes.Add("role", "alert");

                TagBuilder div2 = new TagBuilder("div");
                div2.AddCssClass("col-md-1");

                TagBuilder img = new TagBuilder("i");
                img.AddCssClass("glyphicon glyphicon-exclamation-sign");
                div2.InnerHtml = img.ToString();

                TagBuilder div3 = new TagBuilder("div");
                div3.AddCssClass("col-md-10");

                string errorMessage = modelState.Errors[0].ErrorMessage;
                div3.InnerHtml = errorMessage;

                div.InnerHtml = button.ToString();
                div.InnerHtml += div2.ToString();
                div.InnerHtml += div3.ToString();

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