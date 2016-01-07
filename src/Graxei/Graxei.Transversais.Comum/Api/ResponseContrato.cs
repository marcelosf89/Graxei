using Newtonsoft.Json;

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
