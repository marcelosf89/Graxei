using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using System.Web.Mvc;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class LojasController : Controller
    {
        public LojasController(IGerenciamentoLojas gerenciamentoLojas, IConsultasLojas consultasLojas)
        {
            _gerenciamentoLojas = gerenciamentoLojas;
            _consultasLojas = consultasLojas;
        }

        #region ActionResults
        public ActionResult Index(EnderecoModel item)
        {
            LojaModel model = new LojaModel { LojaContrato = new LojaContrato(), EnderecoModel = item };
            return View("Novo", model);
        }

        [HttpPost]
        [LimpezaSessaoNovaLoja]
        public ActionResult Novo(UsuarioLogado usuario, LojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(item);
            }

            try
            {
                _gerenciamentoLojas.SalvarLoja(item.LojaContrato, usuario.Usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError(string.Empty, ee.Message);
                return PartialView("Cadastro", item);
            }
            ModelState.Clear();
            Loja loja = _consultasLojas.GetPorNome(item.LojaContrato.Nome);
            item.LojaContrato.Id = loja.Id;
            item.LojaContrato.Nome = loja.Nome;
            item.ListEnderecosModel.Clear();
            if (loja.Enderecos != null)
            {
                foreach (Endereco endereco in loja.Enderecos)
                {
                    EnderecoModel enderecoModel = new EnderecoModel();
                    enderecoModel.Id = endereco.Id;
                    enderecoModel.Logradouro = endereco.Logradouro;
                    enderecoModel.IdLoja = loja.Id;
                    enderecoModel.Complemento = endereco.Complemento;
                    enderecoModel.Bairro = endereco.Bairro.Nome;
                    enderecoModel.Cidade = endereco.Bairro.Cidade.Nome;
                    enderecoModel.IdEstado = endereco.Bairro.Cidade.Estado.Id;
                    item.ListEnderecosModel.Add(enderecoModel);
                }
            }
            ViewBag.OperacaoSucesso = Sucesso.LojaIncluida;
            return PartialView("Cadastro", item);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioLogado usuario, LojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(item);
            }
            try
            {
                _gerenciamentoLojas.SalvarLoja(item.LojaContrato, usuario.Usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError("", ee.Message);
                return PartialView(item);
            }
           return PartialView();
        }

        public FileContentResult GetImagem(int idLoja = 0)
        {
            if (idLoja != 0)
            {
                Loja loja = _consultasLojas.Get(idLoja);
                if (loja != null)
                {
                    return File(loja.Logotipo, "image/jpeg");
                }
            }

            return null;

        }

        #endregion
        #region Atributos Privados
        private readonly IGerenciamentoLojas _gerenciamentoLojas;
        private IConsultasLojas _consultasLojas;

        #endregion

    }
}
