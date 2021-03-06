﻿using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Transversais.Comum.LogAplicacao;
using Graxei.Transversais.Comum.Api;
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
using System.Net;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class PesquisaProduto : IPesquisaProduto
    {
        public PesquisaProduto(HttpClient httpClient, ILogAplicacao log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task RegistrarAsync(HistoricoPesquisa historicoPesquisa)
        {
            string json = ApiHttpContent.CriarJson<HistoricoPesquisa>(historicoPesquisa);

            try
            {
                ApiSectionGroup api = (ApiSectionGroup)ConfigurationManager.GetSection("api");
                HttpContent content = ApiHttpContent.Criar(json);
                using (_httpClient)
                {
                    _httpClient.BaseAddress = new Uri(api.Servidor);
                    _httpClient.Timeout = TimeSpan.FromSeconds(2);
                    HttpResponseMessage message = await _httpClient.PostAsync(api.GetRotaTratandoBarraNoInicio("pesquisa-produto"), content);
                    if (!ApiHttpContent.ResponseOk(message))
                    {
                        _log.Registrar(json);
                        _log.Registrar("httpcall", json);
                    }
                }
            }
            catch (Exception)
            {
                _log.Registrar(json);
                _log.Registrar("httpcall", json);
            }

        }

        private ILogAplicacao _log;

        private HttpClient _httpClient;
    }
}
