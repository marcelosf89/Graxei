﻿using System;
using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Models;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Binders
{
    public class UsuariosLogadoBinder : IModelBinder
    {
        #region Implementation of IModelBinder

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            /* TODO: Checar se o fato de o usuário não existir na sessão deve ser aceito*/
            UsuarioLogado usuario = (UsuarioLogado)controllerContext.HttpContext.Session[Constantes.UsuarioAtual];
            if (usuario == null)
            {
                usuario = new UsuarioLogado();
                controllerContext.HttpContext.Session[Constantes.UsuarioAtual] = usuario;
            }
            return usuario;
       }

        #endregion
    }
}