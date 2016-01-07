using System.Collections.Generic;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Models
{
    public class EnderecoVistaModel
    {
        public long IdEndereco { get; set; }

        public long IdLoja { get; set; }

        public IList<SelectListItem> Estados { get; set; }
    }
}