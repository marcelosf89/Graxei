using Graxei.Transversais.ContratosDeDados.Serializacao;
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

        [JsonProperty(PropertyName = "dataPesquisa", ItemConverterType = typeof(DataRFC3339Converter))]
        public DateTime DataPesquisa { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is HistoricoPesquisa))
            {
                return false;
            }

            HistoricoPesquisa that = (HistoricoPesquisa)obj;
            return this.Criterio.Equals(that.Criterio) && this.InternetProtocol.Equals(that.InternetProtocol)
                && this.DataPesquisa.Equals(that.DataPesquisa);
        }

    }
}
