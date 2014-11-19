using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Graxei.Apresentacao.MVC4Unity.Extension;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class UltimoGrupoElementos : IPaginacaoChain
    {
        public UltimoGrupoElementos(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual elementoAtual, int maximoElementosPaginacao, RouteValueDictionary route)
        {
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _ajaxHelper = ajaxHelper;
            _route = route;
            _elementoAtual = elementoAtual;
            _listaTotalElementos = listaTotalElementos;
        }

        public MvcHtmlString Get()
        {
            if (_elementoAtual.Atual > (_listaTotalElementos.Total - _maximoElementosPaginacao))
            {
                StringBuilder stringBuilder = new StringBuilder();

                AjaxOptions ajaxOptions = new AjaxOptions();
                ajaxOptions.OnBegin = "openL()"; ajaxOptions.OnComplete = "closeL()"; ajaxOptions.UpdateTargetId = "myContent";
                ajaxOptions.HttpMethod = "GET";

                stringBuilder.Append("<div class=\"btn-group\">");
                stringBuilder.Append(_ajaxHelper.IconActionLink(string.Empty, 1.ToString(), string.Empty, string.Empty, _route, ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } }).ToHtmlString());
                string controller = _route["Controller"].ToString();
                string action = _route["Action"].ToString();
                for (int i = 2; i <= _maximoElementosPaginacao; i++)
                {
                    RouteValueDictionary route = new RouteValueDictionary();
                    route.Add("numeroPagina", i);
                    stringBuilder.Append(_ajaxHelper.IconActionLink("", i.ToString(), controller, action, route, ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString());
                }

                return MvcHtmlString.Create(stringBuilder.ToString());
            }

            if (ProximoElemento != null)
            {
                return ProximoElemento.Get();
            }

            return MvcHtmlString.Create("<div></div>");
        }

        public IPaginacaoChain ProximoElemento { get; private set; }

        public void SetProximoElemento(IPaginacaoChain paginacaoChain)
        {
            ProximoElemento = paginacaoChain;
        }

        private AjaxHelper _ajaxHelper;

        private RouteValueDictionary _route;

        private int _maximoElementosPaginacao;

        private ListaElementoAtual _elementoAtual;

        private ListaTotalElementos _listaTotalElementos;

    }
}