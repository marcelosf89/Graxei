using System.Text;
using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class PrimeiroItem : IPaginacaoChain
    {
        public PrimeiroItem(ListaTotalElementos listaTotalElementos)
        {
            _listaTotalElementos = listaTotalElementos;
        }

        public MvcHtmlString Get()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"btn-group\">");
            sb.Append(@"<button class=""btn btn-default btn-warning"" name=""numeroPagina"" id=""btn-a" + 1 + @""" value=""1"" disabled=""disabled"">
                                <strong>1</strong>
                            </button>");
            for (int i = 2; i <= _listaTotalElementos.Total; i++)
            {
                    sb.Append(@"<button class=""btn btn-default"" name=""numeroPagina"" id=""btn-a" + i + @""" value=""" + i + @""">
                                " + (i + 1) + @"
                            </button>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public IPaginacaoChain ProximoElemento { get; private set; }

        public void SetProximoElemento(IPaginacaoChain paginacaoChain)
        {
            ProximoElemento = paginacaoChain;
        }

        private ListaTotalElementos _listaTotalElementos;

    }
}