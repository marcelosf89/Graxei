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
    public class UltimoGrupoElementos : AbstractPaginacao
    {
        public UltimoGrupoElementos(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao, string controller, string action) : base(ajaxHelper, listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, controller, action)
        {
        }

        public override bool RegraAtende()
        {
            int meioLista = _maximoElementosPaginacao / 2;
            int inicioUltimoGrupo = _totalPaginas - _maximoElementosPaginacao;
            return _listaElementoAtual.Atual > (inicioUltimoGrupo + meioLista);
        }

        public override int GetInicioLista()
        {
            return (_totalPaginas - _maximoElementosPaginacao) + 1;
        }

        public override int GetFimLista()
        {
            return _totalPaginas;
        }

        public override int GetElementoParaSubstituir()
        {
            return _listaElementoAtual.Atual - ((_totalPaginas - _maximoElementosPaginacao) + 1);
        }
    }
}