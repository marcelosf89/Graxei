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
        public LinkBuilderPequisaProdutosStrategy(PaginaAtualLista paginaAtualLista)
        {
            _paginaAtualLista = paginaAtualLista;
        }

        public List<string> Build(long de, long ate)
        {
            _links = new List<string>();
            for (long i = de; i <= ate; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("button");
                tagBuilder.AddCssClass("btn btn-default btn-pagina");
                tagBuilder.Attributes.Add("doc-type", "paginar");
                tagBuilder.InnerHtml = i.ToString();
                _links.Add(MvcHtmlString.Create(tagBuilder.ToString()).ToString());
            }
            return new List<string>(_links);
        }

        public List<string> SubstituirElementoAtual(long elemento)
        {
            elemento = elemento - 1;
            _links.RemoveAt((int)elemento);
            TagBuilder tagBuilder = new TagBuilder("button");
            tagBuilder.AddCssClass("btn btn-warning");
            tagBuilder.InnerHtml = _paginaAtualLista.Atual.ToString();
            tagBuilder.Attributes.Add("disabled", "disabled");
            _links.Insert((int)elemento, MvcHtmlString.Create(tagBuilder.ToString()).ToString());
            return new List<string>(_links);
        }

        private List<string> _links;
        private PaginaAtualLista _paginaAtualLista;
    }
}