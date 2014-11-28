using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class ImparMenosQueMaximoElementos : AbstractPaginacao
    {
        public ImparMenosQueMaximoElementos(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao, string controller, string action) : base(ajaxHelper, listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, controller, action)
        {
        }

        public override bool RegraAtende()
        {
            bool impar = (_maximoElementosPaginacao % 2 != 0);
            int meioLista = _maximoElementosPaginacao / 2 + 1;
            bool menosQueMaximoElementos = _listaElementoAtual.Atual < _maximoElementosPaginacao && _listaElementoAtual.Atual <= meioLista;
            return impar && menosQueMaximoElementos;
        }

        public override int GetInicioLista()
        {
            return 1;
        }

        public override int GetFimLista()
        {
            return _maximoElementosPaginacao > _listaTotalElementos.Total
                    ? _listaTotalElementos.Total
                    : _maximoElementosPaginacao;
        }

        public override int GetElementoParaSubstituir()
        {
            return _listaElementoAtual.Atual - 1;
        }

    }
}