using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Web.Mvc;


namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class LojasController : Controller
    {
        public LojasController(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos)
        {
            _servicoLojas = servicoLojas;
            _servicoEnderecos = servicoEnderecos;
        }

        #region ActionResults
        public ActionResult Index(NovaLojaModel item)
        {
            return View("Novo", item);
        }

        [HttpPost]
        public ActionResult Novo(UsuarioLogado usuario, NovaLojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _servicoLojas.Salvar(item.Loja, usuario.Usuario);
            foreach (EnderecoIndiceModel end in item.NovosEnderecosModel.Enderecos)
            {
                Endereco endereco = end.Endereco;
                endereco.Loja = item.Loja;
                _servicoEnderecos.Salvar(endereco);
            }
            return View("Salvo");
        }

        public RedirectToRouteResult NovoEndereco(NovosEnderecosModel enderecos,  NovaLojaModel model)
        {
            enderecos.Loja = model.Loja;
            return RedirectToAction("Index", "Enderecos");
        }

        #endregion  
        #region Atributos Privados
        private readonly IServicoLojas _servicoLojas;
        private readonly IServicoEnderecos _servicoEnderecos;
        #endregion

        /*private List<Novo> Enderecos
        {
            get
            {
                if (Session[ItensSessao.EnderecosNovaLoja] == null)
                {
                    Session[ItensSessao.EnderecosNovaLoja] = new List<EnderecosNovaLoja>();
                }
                return (List<EnderecosNovaLoja>)Session[ItensSessao.EnderecosNovaLoja];
            }
            set
            {
                Session[ItensSessao.EnderecosNovaLoja] = value;
            }
        }*/
        

    }
}
