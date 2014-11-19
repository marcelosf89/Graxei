using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Graxei.Apresentacao.MVC4Unity.Extension;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class ElementosPaginacaoImpar : IPaginacaoChain
    {
        public ElementosPaginacaoImpar(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao, RouteValueDictionary routeValueDictionary)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _routeValueDictionary = routeValueDictionary;
            _ajaxHelper = ajaxHelper;
        }

        public MvcHtmlString Get()
        {
            int mod = _maximoElementosPaginacao / 2;
            int limiteMaximoAtual = _listaTotalElementos.Total - _maximoElementosPaginacao;
            bool meioLista = (_listaElementoAtual.Atual > _maximoElementosPaginacao &&
                               limiteMaximoAtual > _listaElementoAtual.Atual);

            if (mod == 0 && meioLista)
            {
                StringBuilder stringBuilder = new StringBuilder();

                AjaxOptions ajaxOptions = new AjaxOptions();
                ajaxOptions.OnBegin = "openL()"; ajaxOptions.OnComplete = "closeL()"; ajaxOptions.UpdateTargetId = "myContent";
                ajaxOptions.HttpMethod = "GET";

                stringBuilder.Append("<div class=\"btn-group\">");

                List<string> links = new List<string>();
                string controller = _routeValueDictionary["Controller"].ToString();
                string action = _routeValueDictionary["Action"].ToString();
                int primeiroElemento = (_listaElementoAtual.Atual - _maximoElementosPaginacao);
                for (int i = primeiroElemento; i <= limiteMaximoAtual; i++)
                {
                    RouteValueDictionary route = new RouteValueDictionary();
                    route.Add("numeroPagina", i);
                    links.Add(
                        _ajaxHelper.IconActionLink("", i.ToString(CultureInfo.InvariantCulture), action, controller, route, ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString());
                }

                int elementoRemover = (limiteMaximoAtual - _listaElementoAtual.Atual);
                links.RemoveAt(elementoRemover);
                string atual = _ajaxHelper.IconActionLink(string.Empty, _listaElementoAtual.Atual.ToString(), string.Empty, string.Empty, _routeValueDictionary, ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } }).ToHtmlString();
                links.Insert(elementoRemover, atual);
                for (int i = 0; i < links.Count; i++)
                {
                    stringBuilder.Append(links[i]);
                }
                return MvcHtmlString.Create(stringBuilder.ToString());
            }
            if (ProximoElemento != null)
            {
                return ProximoElemento.Get();
            }

            return new MvcHtmlString("<div></div>");
        }

        public IPaginacaoChain ProximoElemento { get; private set; }

        public void SetProximoElemento(IPaginacaoChain paginacaoChain)
        {
            ProximoElemento = paginacaoChain;
        }

        private ListaTotalElementos _listaTotalElementos;
        private ListaElementoAtual _listaElementoAtual;
        private int _maximoElementosPaginacao;
        private RouteValueDictionary _routeValueDictionary;
        private AjaxHelper _ajaxHelper;
    }
}