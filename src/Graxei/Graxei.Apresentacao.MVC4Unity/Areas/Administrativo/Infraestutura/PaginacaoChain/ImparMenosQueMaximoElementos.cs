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
    public class ImparMenosQueMaximoElementos : IPaginacaoChain
    {
        public ImparMenosQueMaximoElementos(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao, RouteValueDictionary routeValueDictionary)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _totalPaginas = _listaTotalElementos.Total/_maximoElementosPaginacao;
            _ajaxHelper = ajaxHelper;
            _routeValueDictionary = routeValueDictionary;
        }

        public MvcHtmlString Get()
        {
            bool impar = (_maximoElementosPaginacao%2 != 0);
            int meioLista = _maximoElementosPaginacao / 2 + 1;
            bool menosQueMaximoElementos = _listaElementoAtual.Atual < _maximoElementosPaginacao && _listaElementoAtual.Atual <= meioLista;
            bool criteriosAceitos = impar && menosQueMaximoElementos;
            if (criteriosAceitos)
            {
                StringBuilder stringBuilder = new StringBuilder();

                AjaxOptions ajaxOptions = new AjaxOptions();
                ajaxOptions.OnBegin = "openL()"; ajaxOptions.OnComplete = "closeL()"; ajaxOptions.UpdateTargetId = "myContent";
                ajaxOptions.HttpMethod = "GET";

                stringBuilder.Append("<div class=\"btn-group\">");

                List<string> links = new List<string>();
                string controller = _routeValueDictionary["Controller"].ToString();
                string action = _routeValueDictionary["Action"].ToString();

                int irAte = _maximoElementosPaginacao > _listaTotalElementos.Total
                    ? _listaTotalElementos.Total
                    : _maximoElementosPaginacao;
                for (int i = 1; i <= irAte; i++)
                {
                    RouteValueDictionary route = new RouteValueDictionary();
                    route.Add("numeroPagina", i);
                    links.Add(
                        _ajaxHelper.IconActionLink("", i.ToString(CultureInfo.InvariantCulture), action, controller, route, ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-default" } }).ToHtmlString());
                }

                int elementoSubstituir = this._listaElementoAtual.Atual - 1;
                links.RemoveAt(elementoSubstituir);
                string atual = _ajaxHelper.IconActionLink(string.Empty, _listaElementoAtual.Atual.ToString(), string.Empty, string.Empty, _routeValueDictionary, ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } }).ToHtmlString();
                links.Insert(elementoSubstituir, atual);
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
        private AjaxHelper _ajaxHelper;
        private RouteValueDictionary _routeValueDictionary;
        private int _totalPaginas;
    }
}