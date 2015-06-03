using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.Api.PesquisaProdutos
{
    public class HistoricoPesquisa
    {
        [JsonProperty(PropertyName="criterio")]
        public string Criterio { get; set; }

        [JsonProperty(PropertyName = "internetProtocol")]
        public string InternetProtocol { get; set; }

        [JsonProperty(PropertyName = "dataPesquisa", ItemConverterType = typeof(IsoDateTimeConverter))]
        public DateTime DataPesquisa { get; set; }

    }
}
