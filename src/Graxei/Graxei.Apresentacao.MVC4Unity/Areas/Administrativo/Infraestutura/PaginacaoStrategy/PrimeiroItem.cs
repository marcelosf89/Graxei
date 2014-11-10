using System;
using System.Text;
using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoStrategy
{
    public class PrimeiroItem : IPaginacaoStrategy
    {
        public PrimeiroItem(ListaLojas listaLojas)
        {
            if (listaLojas == null)
            {
                throw new ArgumentNullException(ErrosInternos.ListasLojasNula);
            }
            _listaLojas = listaLojas;
        }

        public MvcHtmlString Get()
        {
            return null;
        }

        private ListaLojas _listaLojas;
    }
}