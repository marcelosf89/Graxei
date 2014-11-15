using System.Text;
using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoStrategy;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Apresentacao.MVC4Unity.Extension;
using System.Web.Routing;
using System.Collections.Generic;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura
{
    public static class PaginacaoHelper
    {
        public static MvcHtmlString LinkPaginacao(this HtmlHelper htmlHelper, ListaLojas listaLojas)
        {
            return new MaisQueCincoTotal(listaLojas).Get();
        }

        public static MvcHtmlString LinkPaginacaoRangePagina(this AjaxHelper ajaxHelper, string action, string controller, string requestName, int paginaSelecionada, long paginasMaxima, int quantidadePaginasAbaixoAtual)
        {
            StringBuilder sb = new StringBuilder();

            long idx = ((paginaSelecionada <= quantidadePaginasAbaixoAtual + 1) ? 0 : paginaSelecionada - quantidadePaginasAbaixoAtual);

            System.Web.Mvc.Ajax.AjaxOptions ao = new System.Web.Mvc.Ajax.AjaxOptions();
            ao.OnBegin = "openL()"; ao.OnComplete = "closeL()"; ao.UpdateTargetId = "myContent";
            ao.HttpMethod = "GET";

            sb.Append("<div class=\"btn-group\">");
            if (paginaSelecionada > 0)
            {
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("Controller", controller);
                rd.Add(requestName, (paginaSelecionada - 1));

                sb.Append(
                    ajaxHelper.IconActionLink("glyphicon glyphicon-chevron-left", "", action, controller, rd, ao, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString()
                    );
            }

            for (long i = idx; i <= paginasMaxima; i++)
            {
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("Controller", controller);
                rd.Add(requestName, i);

                if (paginaSelecionada == i)
                {
                    sb.Append(ajaxHelper.IconActionLink("", (i + 1).ToString(), "#", "#", rd, ao, new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } }).ToHtmlString());
                }
                else
                {
                    sb.Append(ajaxHelper.IconActionLink("", (i + 1).ToString(), action, controller, rd, ao, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString());
                }
            }
            if (paginasMaxima > 0 && paginaSelecionada < paginasMaxima)
            {
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("Controller", controller);
                rd.Add(requestName, (paginaSelecionada + 1));

                sb.Append(
                    ajaxHelper.IconActionLink("glyphicon glyphicon-chevron-right", "", action, controller, rd, ao, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString()
                );
            }
            sb.Append("</div>");
            return new MvcHtmlString(sb.ToString());
        }


        public static MvcHtmlString LinkPaginacao2(this AjaxHelper ajaxHelper, string action, string controller, string requestName, int paginaSelecionada, int valorTotal, int quantidadeApresentacao)
        {
            StringBuilder sb = new StringBuilder();

            System.Web.Mvc.Ajax.AjaxOptions ao = new System.Web.Mvc.Ajax.AjaxOptions();
            ao.OnBegin = "openL()"; ao.OnComplete = "closeL()"; ao.UpdateTargetId = "myContent";
            ao.HttpMethod = "GET";

            sb.Append("<div class=\"btn-group\">");
            if (paginaSelecionada > 0)
            {
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("Controller", controller);
                rd.Add(requestName, (paginaSelecionada - 1));
                rd.Add("tamanho", quantidadeApresentacao);

                sb.Append(
                    ajaxHelper.IconActionLink("glyphicon glyphicon-chevron-left", "", action, controller, rd, ao, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString()
                    );
            }
            int count = (valorTotal - 1) / quantidadeApresentacao;
            for (int i = 0; i <= count; i++)
            {
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("Controller", controller);
                rd.Add(requestName, i);
                rd.Add("tamanho", quantidadeApresentacao);

                if (paginaSelecionada == i)
                {
                    sb.Append(ajaxHelper.IconActionLink("", (i + 1).ToString(), "#", "#", rd, ao, new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } }).ToHtmlString());
                }
                else
                {
                    sb.Append(ajaxHelper.IconActionLink("", (i + 1).ToString(), action, controller, rd, ao, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString());
                }
            }
            if (count > 0 && paginaSelecionada < count)
            {
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("Controller", controller);
                rd.Add(requestName, (paginaSelecionada + 1));
                rd.Add("tamanho", quantidadeApresentacao);

                sb.Append(ajaxHelper.IconActionLink("glyphicon glyphicon-chevron-right", "", action, controller, rd, ao, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString());
            }
            sb.Append("</div>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}