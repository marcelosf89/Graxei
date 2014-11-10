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
    }
}