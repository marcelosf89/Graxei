using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Extension
{
    public static partial class MVCExtensionComponent
    {
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

        public static MvcHtmlString LinkPaginacao(this AjaxHelper ajaxlHelper, string controller, string action, ListaElementoAtual listaElementoAtual, ListaTotalElementos listaTotalElementos, int maximoElementosPaginacao)
        {
            RouteValueDictionary route = new RouteValueDictionary();
            route.Add("Controller", controller);
            route.Add("Action", action);
            PaginacaoChainFactory paginacaoChainFactory = new PaginacaoChainFactory(ajaxlHelper, listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, route);
            return paginacaoChainFactory.ConstruirCadeiaDePaginacao().Get();
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