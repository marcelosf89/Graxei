using Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain.LinkBuilderStrategy;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain
{
    public interface IPaginacaoChain
    {
        ILinkBuilderStrategy LinkBuilderStrategy { get; }
        MvcHtmlString Get();
        IPaginacaoChain ProximoElemento { get; }
        void SetProximoElemento(IPaginacaoChain paginacaoChain);
    }
}
