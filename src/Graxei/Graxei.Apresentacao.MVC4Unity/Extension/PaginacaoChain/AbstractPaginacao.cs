using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Graxei.Apresentacao.MVC4Unity.Extension;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain
{
    public abstract class AbstractPaginacao : IPaginacaoChain
    {
        public AbstractPaginacao(AjaxHelper ajaxHelper, TotalElementosLista totalElementosLista,
            PaginaAtualLista elementoAtualLista,
            int quantidadeMaximaLinksPaginacaoPorVez, string controller, string action)
        {
            _totalElementosLista = totalElementosLista;
            _elementoAtualLista = elementoAtualLista;
            _quantidadeMaximaLinksPaginacaoPorVez = quantidadeMaximaLinksPaginacaoPorVez;
            _totalPaginas = 1;
            if (_totalElementosLista.Total / _quantidadeMaximaLinksPaginacaoPorVez > 1)
            {
                _totalPaginas = _totalElementosLista.Total / 10;
            }
            _ajaxHelper = ajaxHelper;
            _controller = controller;
            _action = action;
            _routeValueDictionary = new RouteValueDictionary();
            _routeValueDictionary.Add("Controller", _controller);
            _routeValueDictionary.Add("Action", _action);
        }
        
        public abstract bool RegraAtende();

        public abstract int GetPrimeiraPaginaGrupoAtual();

        public abstract int GetUltimaPaginaGrupoAtual();

        public abstract int GetElementoParaSubstituir();
        
        public MvcHtmlString Get()
        {
            bool tratadoPeloElementoDaCadeia = RegraAtende();
            if (tratadoPeloElementoDaCadeia)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class=\"btn-group\">");

                List<string> links = BuildLinks();

                SubstituirElementoAtual(links);
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

        private void SubstituirElementoAtual(List<string> links)
        {
            int elementoParaSubstituir = GetElementoParaSubstituir();
            links.RemoveAt(elementoParaSubstituir);
            string atual =
                _ajaxHelper.IconActionLink(string.Empty,
                    _elementoAtualLista.Atual.ToString(CultureInfo.InvariantCulture), string.Empty, string.Empty,
                    _routeValueDictionary, _ajaxOptions,
                    new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } })
                    .ToHtmlString();
            links.Insert(elementoParaSubstituir, atual);
        }

        public List<string> BuildLinks()
        {
            SetUpAjaxOptions();
            List<string> links = new List<string>();
            int irDe = GetPrimeiraPaginaGrupoAtual();
            int irAte = GetUltimaPaginaGrupoAtual();
            for (int i = irDe; i <= irAte; i++)
            {
                RouteValueDictionary route = new RouteValueDictionary();
                route.Add("numeroPagina", i);
                links.Add(
                    _ajaxHelper.IconActionLink("", i.ToString(CultureInfo.InvariantCulture), _action, _controller,
                        route, _ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-default" } })
                        .ToHtmlString());
            }
            return links;
        }

        private void SetUpAjaxOptions()
        {
            _ajaxOptions = new AjaxOptions();
            _ajaxOptions.OnBegin = "openL()";
            _ajaxOptions.OnComplete = "closeL()";
            _ajaxOptions.UpdateTargetId = "myContent";
            _ajaxOptions.HttpMethod = "GET";
        }

        public IPaginacaoChain ProximoElemento { get; private set; }

        public void SetProximoElemento(IPaginacaoChain paginacaoChain)
        {
            ProximoElemento = paginacaoChain;
        }

        public AjaxOptions AjaxOptions
        {
            get
            {
                return _ajaxOptions;
            }
        }

        protected TotalElementosLista _totalElementosLista;
        protected PaginaAtualLista _elementoAtualLista;
        protected int _quantidadeMaximaLinksPaginacaoPorVez;
        protected int _totalPaginas;
        protected AjaxHelper _ajaxHelper;
        protected AjaxOptions _ajaxOptions;
        protected string _controller;
        protected string _action;
        protected RouteValueDictionary _routeValueDictionary;
    }
}