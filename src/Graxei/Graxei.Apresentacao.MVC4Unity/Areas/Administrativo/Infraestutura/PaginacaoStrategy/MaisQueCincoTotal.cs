using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoStrategy
{
    public class MaisQueCincoTotal : IPaginacaoStrategy
    {
        public MaisQueCincoTotal(ListaLojas listaLojas)
        {
            if (listaLojas == null)
            {
                throw new ArgumentNullException(ErrosInternos.ListasLojasNula);
            }
            _listaLojas = listaLojas;
        }

        public MvcHtmlString Get()
        {
            List<string> itens = new List<string>();
            if (_listaLojas.Atual.Atual <= 5)
            {
                for (int i = 1; i <= 5; i++)
                {
                    TagBuilder tagBuilder = new TagBuilder("button");
                    tagBuilder.AddCssClass("btn btn-default");
                    tagBuilder.InnerHtml = i.ToString();
                    itens.Add(tagBuilder.ToString());
                }
                itens.RemoveAt(_listaLojas.Atual.Atual - 1);
                itens.Insert(_listaLojas.Atual.Atual - 1, _listaLojas.Atual.Atual.ToString());
            }
            else if (_listaLojas.Total.Total > 5 && _listaLojas.Atual.Atual > 3 && (_listaLojas.Total.Total < (_listaLojas.Atual.Atual + 5)))
            {
                for (int i = _listaLojas.Atual.Atual; i <= _listaLojas.Total.Total; i++)
                {
                    TagBuilder tagBuilder = new TagBuilder("button");
                    tagBuilder.AddCssClass("btn btn-default");
                    tagBuilder.InnerHtml = i.ToString();
                    itens.Add(tagBuilder.ToString());
                }
                int indiceAtual = _listaLojas.Total.Total - _listaLojas.Atual.Atual;
                itens.RemoveAt(_listaLojas.Atual.Atual - 1);
                itens.Insert(indiceAtual - 1, _listaLojas.Atual.Atual.ToString());
            } else if (_listaLojas.Total.Total > 5 && (_listaLojas.Total.Total > (_listaLojas.Atual.Atual + 5)))
            {
                int indiceDe = _listaLojas.Atual.Atual - 2;
                int indiceAte = _listaLojas.Atual.Atual + 2;
                for (int i = indiceDe; i <= indiceAte; i++)
                {
                    TagBuilder tagBuilder = new TagBuilder("button");
                    tagBuilder.AddCssClass("btn btn-default");
                    tagBuilder.InnerHtml = i.ToString();
                    itens.Add(tagBuilder.ToString());
                }
                itens.RemoveAt(2);
                itens.Insert(2, _listaLojas.Atual.Atual.ToString());
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string s in itens)
            {
                stringBuilder.Append(s);
            }
            
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        private ListaLojas _listaLojas;
    }
}