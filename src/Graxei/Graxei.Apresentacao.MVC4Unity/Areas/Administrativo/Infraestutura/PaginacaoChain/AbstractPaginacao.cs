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
    public abstract class AbstractPaginacao : IPaginacaoChain
    {
        public AbstractPaginacao(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos,
            ListaElementoAtual listaElementoAtual,
            int maximoElementosPaginacao, string controller, string action)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _totalPaginas = 1;
            if (_listaTotalElementos.Total / _maximoElementosPaginacao >= 1)
            {
                _totalPaginas = _listaTotalElementos.Total / 10;
            }
            _ajaxHelper = ajaxHelper;
            _controller = controller;
            _action = action;
            _routeValueDictionary = new RouteValueDictionary();
            _routeValueDictionary.Add("Controller", _controller);
            _routeValueDictionary.Add("Action", _action);
        }
        
        public abstract bool RegraAtende();

        public abstract int GetInicioLista();

        public abstract int GetFimLista();

        public abstract int GetElementoParaSubstituir();
        
        public MvcHtmlString Get()
        {
            bool tratadoPeloElementoDaCadeia = RegraAtende();
            if (tratadoPeloElementoDaCadeia)
            {
                StringBuilder stringBuilder = new StringBuilder();

                AjaxOptions ajaxOptions = new AjaxOptions();
                ajaxOptions.OnBegin = "openL()";
                ajaxOptions.OnComplete = "closeL()";
                ajaxOptions.UpdateTargetId = "myContent";
                ajaxOptions.HttpMethod = "GET";

                stringBuilder.Append("<div class=\"btn-group\">");

                List<string> links = new List<string>();
                int irDe = GetInicioLista();
                int irAte = GetFimLista();
                for (int i = irDe; i <= irAte; i++)
                {
                    RouteValueDictionary route = new RouteValueDictionary();
                    route.Add("numeroPagina", i);
                    links.Add(
                        _ajaxHelper.IconActionLink("", i.ToString(CultureInfo.InvariantCulture), _action, _controller,
                            route, ajaxOptions, new Dictionary<string, object> {{"class", "btn btn-default"}})
                            .ToHtmlString());
                }

                int elementoParaSubstituir = GetElementoParaSubstituir();
                links.RemoveAt(elementoParaSubstituir);
                string atual =
                    _ajaxHelper.IconActionLink(string.Empty,
                        _listaElementoAtual.Atual.ToString(CultureInfo.InvariantCulture), string.Empty, string.Empty,
                        _routeValueDictionary, ajaxOptions,
                        new Dictionary<string, object> {{"class", "btn btn-warning"}, {"disabled", "disabled"}})
                        .ToHtmlString();
                links.Insert(elementoParaSubstituir, atual);
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

        protected ListaTotalElementos _listaTotalElementos;
        protected ListaElementoAtual _listaElementoAtual;
        protected int _maximoElementosPaginacao;
        protected int _totalPaginas;
        protected AjaxHelper _ajaxHelper;
        protected string _controller;
        protected string _action;
        protected RouteValueDictionary _routeValueDictionary;
    }
}