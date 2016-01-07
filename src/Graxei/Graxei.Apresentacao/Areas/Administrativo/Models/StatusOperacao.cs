using Newtonsoft.Json;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class StatusOperacao
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }

        [JsonProperty("errosValidacao")]
        public string[] ErrosValidacao { get; set; }
    }
}