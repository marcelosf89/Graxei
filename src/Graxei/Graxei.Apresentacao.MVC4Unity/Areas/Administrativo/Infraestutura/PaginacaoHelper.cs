using System.Text;
using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoStrategy;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura
{
    public static class PaginacaoHelper
    {
        public static MvcHtmlString LinkPaginacao(this HtmlHelper htmlHelper, ListaLojas listaLojas)
        {
            return new MaisQueCincoTotal(listaLojas).Get();
        }

        public static MvcHtmlString LinkPaginacaoRangePagina(this HtmlHelper htmlHelper, string requestName, int paginaSelecionada, long paginasMaxima, int quantidadePaginasAbaixoAtual, int valorMaximoDePaginaApresentacao)
        {
            StringBuilder sb = new StringBuilder();

            long idx = ((paginaSelecionada <= quantidadePaginasAbaixoAtual + 1) ? 0 : paginaSelecionada - quantidadePaginasAbaixoAtual);

            sb.Append("<div class=\"btn-group\">");
            if (paginaSelecionada > 0)
            {
                sb.Append(@"<button class=""btn btn-default"" name=""" + requestName + @""" id=""btn-a"" value=""" + (paginaSelecionada - 1) + @""" tooltip=""Anterior"" >
                                <i class=""glyphicon glyphicon-chevron-left""></i>Anterior
                            </button>");
            }

            for (long i = idx; i <= paginasMaxima; i++)
            {
                if (paginaSelecionada == i)
                {
                    sb.Append(@"<button class=""btn btn-default btn-warning"" name=""" + requestName + @""" id=""btn-a"+i+@""" value=""" + i + @""" disabled=""disabled"">
                                <strong>" + (i + 1) + @"</strong>
                            </button>");
                }
                else
                {
                    sb.Append(@"<button class=""btn btn-default"" name=""" + requestName + @""" id=""btn-a" + i + @""" value=""" + i + @""">
                               " + (i + 1) + @"
                            </button>");
                }
            }
            if (paginasMaxima > 0 && paginaSelecionada < paginasMaxima)
            {
                sb.Append(@"<button class=""btn btn-default"" name=""" + requestName + @""" id=""btn-p"" value=""" + (paginaSelecionada + 1) + @""" tooltip=""Proximo"">
                                <i class=""glyphicon glyphicon-chevron-right""></i>Proximo
                            </button>");
            }
            sb.Append("</div>");
            return new MvcHtmlString(sb.ToString());
        }


        public static MvcHtmlString LinkPaginacao2(this HtmlHelper htmlHelper, string requestName, int paginaSelecionada, int valorTotal, int quantidadeApresentacao)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"btn-group\">");
            if (paginaSelecionada > 0)
            {
                sb.Append(@"<button class=""btn btn-default"" name=""" + requestName + @""" id=""btn-a"" value=""" + (paginaSelecionada - 1) + @""" tooltip=""Anterior"" >
                                <i class=""glyphicon glyphicon-chevron-left""></i>Anterior
                            </button>");
            }
            int count = valorTotal / quantidadeApresentacao;
            for (int i = 0; i <= count; i++)
            {
                if (paginaSelecionada == i)
                {
                    sb.Append(@"<button class=""btn btn-default btn-warning"" name=""" + requestName + @""" id=""btn-a" + i + @""" value=""" + i + @""" disabled=""disabled"">
                                <strong>" + (i + 1) + @"</strong>
                            </button>");
                }
                else
                {
                    sb.Append(@"<button class=""btn btn-default"" name=""" + requestName + @""" id=""btn-a" + i + @""" value=""" + i + @""">
                                " + (i + 1) + @"
                            </button>");
                }
            }
            if (count > 0 && paginaSelecionada < count)
            {
                sb.Append(@"<button class=""btn btn-default"" name=""" + requestName + @""" id=""btn-p"" value=""" + (paginaSelecionada + 1) + @""" tooltip=""Proximo"">
                                <i class=""glyphicon glyphicon-chevron-right""></i>Proximo
                            </button>");
            }
            sb.Append("</div>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}