using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.Api
{
    public class ResponseContrato
    {
        [JsonProperty(PropertyName = "ok")]
        public bool Ok { get; set; }

        [JsonProperty(PropertyName = "mensagem")]
        public string Mensagem { get; set; }

        [JsonProperty(PropertyName = "conteudo")]
        public object Conteudo { get; set; }

    }
}
