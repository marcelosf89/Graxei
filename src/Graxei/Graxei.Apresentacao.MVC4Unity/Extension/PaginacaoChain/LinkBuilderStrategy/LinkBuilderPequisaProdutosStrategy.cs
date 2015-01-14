using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain.LinkBuilderStrategy
{
    public class LinkBuilderPequisaProdutosStrategy : ILinkBuilderStrategy
    {
        public LinkBuilderPequisaProdutosStrategy(AjaxHelper ajaxHelper, string controller, string action, PaginaAtualLista paginaAtualLista)
        {
            _ajaxHelper = ajaxHelper;
            _controller = controller;
            _action = action;
            _paginaAtualLista = paginaAtualLista;
        }

        public List<string> Build(long de, long ate)
        {
            SetUpAjaxOptions();
            _links = new List<string>();
            for (long i = de; i <= ate; i++)
            {
                RouteValueDictionary route = new RouteValueDictionary();
                route.Add("numeroPagina", i);
                _links.Add(
                    _ajaxHelper.IconActionLink("", i.ToString(CultureInfo.InvariantCulture), _action, _controller,
                        route, _ajaxOptions, new Dictionary<string, object> { { "class", "btn btn-default" } })
                        .ToHtmlString());
            }
            return new List<string>(_links);
        }

        public List<string> SubstituirElementoAtual(long elemento)
        {

            _links.RemoveAt((int)elemento);
            string atual =
                _ajaxHelper.IconActionLink(string.Empty,
                    _paginaAtualLista.Atual.ToString(CultureInfo.InvariantCulture), _action, _controller,
                    null, _ajaxOptions,
                    new Dictionary<string, object> { { "class", "btn btn-warning" }, { "disabled", "disabled" } })
                    .ToHtmlString();
            _links.Insert((int)elemento, atual);
            return new List<string>(_links);
        }

        private void SetUpAjaxOptions()
        {
            _ajaxOptions = new AjaxOptions();
            _ajaxOptions.OnBegin = "openL()";
            _ajaxOptions.OnComplete = "closeL()";
            _ajaxOptions.UpdateTargetId = "myContent";
            _ajaxOptions.HttpMethod = "GET";
        }

        private AjaxOptions _ajaxOptions;
        private AjaxHelper _ajaxHelper;
        private List<string> _links;
        private string _controller;
        private string _action;
        private PaginaAtualLista _paginaAtualLista;
    }
}