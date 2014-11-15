using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class MenosQueMaximoElementos : IPaginacaoChain
    {
        public MenosQueMaximoElementos(ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
        }

        public MvcHtmlString Get()
        {
            if (_listaTotalElementos.Total < _maximoElementosPaginacao)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<div class=\"btn-group\">");
                List<string> botoes = new List<string>();
                for (int i = 1; i <= _listaTotalElementos.Total; i++)
                {
                    botoes.Add(@"<button class=""btn btn-default"" name=""numeroPagina"" id=""btn-a" + i + @""" value=""" + i + @""">
                                " + (i + 1) + @"
                            </button>");
                }

                botoes.RemoveAt(_listaElementoAtual.Atual - 1);
                string atual = @"<button class=""btn btn-default btn-warning"" name=""numeroPagina"" id=""btn-a" +
                               _listaElementoAtual.Atual + @""" value=""1"" disabled=""disabled"">
                                <strong>1</strong>
                            </button>";
                botoes.Insert(_listaElementoAtual.Atual - 1, atual);
                sb.Append(botoes);
                return MvcHtmlString.Create(sb.ToString());
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
    }
}