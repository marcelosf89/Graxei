using System.Web.Mvc;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Binders
{
    public class EnderecosBinder : IModelBinder
    {
        #region Implementation of IModelBinder

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Endereco endereco = (Endereco)controllerContext.HttpContext.Session[GerEndEmLoj];
            // create the Cart if there wasn't one in the session data
            if (endereco == null)
            {
                endereco = new Endereco();
                controllerContext.HttpContext.Session[GerEndEmLoj] = endereco;
            }
            // return the cart
            return endereco;
        }

        #endregion

        private const string GerEndEmLoj = "GerEndEmLoj";
    }
}