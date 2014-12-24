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
    public class UltimoGrupoElementos : AbstractPaginacao
    {
        public UltimoGrupoElementos(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, string controller, string action) : base(ajaxHelper, listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, controller, action)
        {
        }

        public override bool RegraAtende()
        {
            int meioLista = _quantidadeMaximaLinksPaginacaoPorVez / 2;
            int inicioUltimoGrupo = _totalPaginas - _quantidadeMaximaLinksPaginacaoPorVez;
            return _elementoAtualLista.Atual > (inicioUltimoGrupo + meioLista);
        }

        public override int GetPrimeiraPaginaGrupoAtual()
        {
            return (_totalPaginas - _quantidadeMaximaLinksPaginacaoPorVez) + 1;
        }

        public override int GetUltimaPaginaGrupoAtual()
        {
            return _totalPaginas;
        }

        public override int GetElementoParaSubstituir()
        {
            return _elementoAtualLista.Atual - ((_totalPaginas - _quantidadeMaximaLinksPaginacaoPorVez) + 1);
        }
    }
}