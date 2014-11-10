using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.MVC4Unity.Models
{
    public class PesquisarModel
    {
        public long? NumeroMaximoPagina { get; set; }
        public long PaginaSelecionada { get; set; }
        public String Texto { get; set; }
    }
}