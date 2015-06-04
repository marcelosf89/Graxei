using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Transversais.Comum.SectionGroups;
using Graxei.Transversais.ContratosDeDados.Api.PesquisaProdutos;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class PesquisaProduto : IPesquisaProduto
    {
        public PesquisaProduto(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegistrarAsync(HistoricoPesquisa historicoPesquisa)
        {
            ApiSectionGroup api = (ApiSectionGroup)ConfigurationManager.GetSection("api");

            using (_httpClient)
            {
                _httpClient.BaseAddress = new Uri(api.Servidor);

                string json = JsonConvert.SerializeObject(historicoPesquisa, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.FFF'Z'" });
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _httpClient.PostAsync(api.GetRotaTratandoBarraNoInicio("pesquisa-produto"), content);
            }
        }

        private HttpClient _httpClient;
    }
}
