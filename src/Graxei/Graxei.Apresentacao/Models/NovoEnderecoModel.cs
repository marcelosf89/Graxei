using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Models
{
    public class NovoEnderecoModel
    {
        public long IdLoja { get; set; }

        public IList<SelectListItem> Estados { get; set; }
    }
}