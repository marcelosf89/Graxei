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
    public class ImparCentralizar : AbstractPaginacao
    {
        public ImparCentralizar(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao, string controller, string action) : base(ajaxHelper, listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, controller, action)
        {
            _meioLista = _maximoElementosPaginacao / 2;
        }

        public override bool RegraAtende()
        {
            bool impar = (_maximoElementosPaginacao%2 != 0);
            int meioLista = _maximoElementosPaginacao/2;
            bool ficarNoCentro = _listaElementoAtual.Atual < (_totalPaginas - meioLista) && _listaElementoAtual.Atual > (meioLista + 1);
            return (impar && ficarNoCentro);
        }

        public override int GetInicioLista()
        {
            return _listaElementoAtual.Atual - _meioLista;
        }

        public override int GetFimLista()
        {
            return _listaElementoAtual.Atual + _meioLista;
        }

        public override int GetElementoParaSubstituir()
        {
            return _meioLista;
        }

        private int _meioLista;
    }
}