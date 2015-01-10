using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain;
using Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.MVC4Unity.Extension
{
    public static partial class MVCExtensionComponent
    {
        public static MvcHtmlString LinkPaginacao(this AjaxHelper ajaxHelper, string controller, string action, PaginaAtualLista paginaAtualLista, TotalElementosLista listaTotalElementos, int maximoElementosPaginacao)
        {
            if (listaTotalElementos.Total <= 0)
            {
                return MvcHtmlString.Create("<div>Nenhum resultado</div>");
            }
            LinkBuilderPadraoStrategy linkBuilder = new LinkBuilderPadraoStrategy(ajaxHelper, controller, action, paginaAtualLista);
            PaginacaoChainFactory paginacaoChainFactory = new PaginacaoChainFactory(ajaxHelper, listaTotalElementos, paginaAtualLista, maximoElementosPaginacao, controller, action, linkBuilder);
            return paginacaoChainFactory.ConstruirCadeiaDePaginacao().Get();
        }
    }
}