using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class StatusOperacao
    {
        public StatusOperacao()
        {
            Ok = false;
        }

        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }

        [JsonProperty("errosValidacao")]
        public string[] ErrosValidacao { get; set; }

        public void SetOkTrue(string mensagem)
        {
            Ok = true;
            Mensagem = mensagem;
        }
    }
}