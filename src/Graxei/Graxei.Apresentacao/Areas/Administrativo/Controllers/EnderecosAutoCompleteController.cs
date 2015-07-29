using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Modelo;
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
                
        public ActionResult EstadoSelecionado(string idEstado)
        {
            int id = int.Parse(idEstado);
            _cacheElementosEndereco.SetCidades(_consultasCidades.GetPorEstado(id));
            return null;
        }

        public ActionResult CidadeSelecionada(string idEstado, string cidade)
        {
            int id = int.Parse(idEstado);
            _cacheElementosEndereco.SetBairros(_consultasBairros.GetPorCidade(cidade, id));
            return null;
        }

        public ActionResult BairroSelecionado(long estado, string cidade, string bairro)
        {
            _cacheElementosEndereco.SetLogradouros(_consultasLogradouros.Get(bairro, cidade, estado));
            return null;
        }

        public ActionResult AutoCompleteCidade(string term)
        {
            string[] itens = _cacheElementosEndereco.GetCidades().Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteBairro(string term)
        {
            string[] itens = _cacheElementosEndereco.GetBairros().Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            _cacheElementosEndereco.SetLogradouros(new List<Logradouro>());
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteLogradouro(string term)
        {
            string[] itens = _cacheElementosEndereco.GetLogradouros().Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        private readonly IConsultasLogradouros _consultasLogradouros;
        private readonly IConsultasBairros _consultasBairros;
        private readonly IConsultaCidades _consultasCidades;
        private readonly IConsultaEstados _consultasEstados;
        private readonly ICacheElementosEndereco _cacheElementosEndereco;

    }
}