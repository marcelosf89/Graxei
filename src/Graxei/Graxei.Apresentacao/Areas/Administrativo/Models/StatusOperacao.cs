using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class StatusOperacao
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }

    }
}