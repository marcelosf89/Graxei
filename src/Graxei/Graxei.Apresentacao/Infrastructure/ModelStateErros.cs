using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Infrastructure
{
    public class ModelStateErros
    {
        public static string[] Get(ModelStateDictionary modelState)
        {
            IList<string> retorno = (from model in modelState.Values
                                     where model.Errors != null && model.Errors.Count > 0
                                     select model.Errors)
                                                        .SelectMany(item => item)
                                                        .Select(modelError => modelError.ErrorMessage)
                                                        .ToList();
            return retorno.ToArray();
        }
    }
}