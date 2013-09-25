using System.Collections.Generic;
using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Binders
{
    public class NovosEnderecosBinder : IModelBinder
    {
        #region Implementation of IModelBinder

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return null;
            /*IList<NovoEnderecoModel> enderecos = (Endereco)controllerContext.HttpContext.Session[GerEndEmLoj];
            // create the Cart if there wasn't one in the session data
            if (endereco == null)
            {
                endereco = new Endereco();
                controllerContext.HttpContext.Session[GerEndEmLoj] = endereco;
            }
            // return the cart
            return endereco;*/
        }

        #endregion

        private const string GerEndEmLoj = "GerEndEmLoj";
    }
}