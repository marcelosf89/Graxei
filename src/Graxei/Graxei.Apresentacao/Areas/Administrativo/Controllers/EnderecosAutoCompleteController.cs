using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Apresentacao.Areas.Administrativo.Models;
using Graxei.Apresentacao.Infrastructure.ActionResults;
using Graxei.Modelo;
using Graxei.Transversais.Idiomas;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Areas.Administrativo.Controllers
{
    public class EnderecosAutoCompleteController : Controller
    {
        public EnderecosAutoCompleteController(IConsultaEnderecos consultasEnderecos, IConsultasBairros consultasBairros, IConsultasLojas consultasLojas, IConsultaEstados consultasEstados, IConsultaCidades consultasCidades, IConsultasLogradouros consultasLogradouros, ICacheElementosEndereco cacheElementosEndereco)
        {
            _consultasBairros = consultasBairros;
            _consultasEstados = consultasEstados;
            _consultasCidades = consultasCidades;
            _consultasLogradouros = consultasLogradouros;
            _cacheElementosEndereco = cacheElementosEndereco;
        }

        public JsonNetResult EstadoSelecionado(string idEstado)
        {
            StatusOperacao statusOperacao = new StatusOperacao { Ok = true };
            try
            {
                int id = int.Parse(idEstado);
                _cacheElementosEndereco.SetCidades(_consultasCidades.GetPorEstado(id));
                _cacheElementosEndereco.SetBairros(new List<Bairro>());
                _cacheElementosEndereco.SetLogradouros(new List<Logradouro>());
                statusOperacao.Mensagem = "OK";
            }
            catch (Exception)
            {
                statusOperacao.Ok = false;
                statusOperacao.Mensagem = Erros.MensagemGenerica;
            }

            return new JsonNetResult(statusOperacao, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public ActionResult CidadeSelecionada(string idEstado, string cidade)
        {
            StatusOperacao statusOperacao = new StatusOperacao { Ok = true };
            try
            {
                int id = int.Parse(idEstado);
                _cacheElementosEndereco.SetBairros(_consultasBairros.GetPorCidade(cidade, id));
                _cacheElementosEndereco.SetLogradouros(new List<Logradouro>());
                statusOperacao.Mensagem = "OK";
            }
            catch (Exception)
            {
                statusOperacao.Ok = false;
                statusOperacao.Mensagem = Erros.MensagemGenerica;
            }

            return new JsonNetResult(statusOperacao, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public ActionResult BairroSelecionado(long estado, string cidade, string bairro)
        {
            StatusOperacao statusOperacao = new StatusOperacao { Ok = true };
            try
            {
                _cacheElementosEndereco.SetLogradouros(_consultasLogradouros.Get(bairro, cidade, estado));
                statusOperacao.Mensagem = "OK";
            }
            catch (Exception)
            {
                statusOperacao.Ok = false;
                statusOperacao.Mensagem = Erros.MensagemGenerica;
            }

            return new JsonNetResult(statusOperacao, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public JsonNetResult AutoCompleteCidade(string term)
        {
            string[] json = _cacheElementosEndereco.GetCidades()
                                                   .Where(
                                                          item => item.Nome.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                                    .Select(p => p.Nome).ToArray();
            return new JsonNetResult(json, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public JsonNetResult AutoCompleteBairro(string term)
        {
            string[] itens = _cacheElementosEndereco.GetBairros().Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            _cacheElementosEndereco.SetLogradouros(new List<Logradouro>());
            return new JsonNetResult(itensFiltrados, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public JsonNetResult AutoCompleteLogradouro(string term)
        {
            string[] itens = _cacheElementosEndereco.GetLogradouros().Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return new JsonNetResult(itensFiltrados, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        private readonly IConsultasLogradouros _consultasLogradouros;
        private readonly IConsultasBairros _consultasBairros;
        private readonly IConsultaCidades _consultasCidades;
        private readonly IConsultaEstados _consultasEstados;
        private readonly ICacheElementosEndereco _cacheElementosEndereco;

    }
}