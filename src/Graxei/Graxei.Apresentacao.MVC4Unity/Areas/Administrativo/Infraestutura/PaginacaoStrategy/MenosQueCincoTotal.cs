using System;
using System.Text;
using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoStrategy
{
    public class MenosQueCincoTotal : IPaginacaoStrategy
    {
        public MenosQueCincoTotal(ListaLojas listaLojas)
        {
            if (listaLojas == null)
            {
                throw new ArgumentNullException(ErrosInternos.ListasLojasNula);
            }
            _listaLojas = listaLojas;
        }

        public MvcHtmlString Get()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i <= _listaLojas.Total.Total; i++)
            {
                if (i == _listaLojas.Atual.Atual)
                {
                    stringBuilder.Append(i.ToString());
                }
                else
                {
                    TagBuilder tagBuilder = new TagBuilder("button");
                    tagBuilder.AddCssClass("btn btn-link");
                    tagBuilder.InnerHtml = _listaLojas.Atual.Atual.ToString();
                    stringBuilder.Append(tagBuilder.ToString());    
                }
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        private ListaLojas _listaLojas;
    }
}