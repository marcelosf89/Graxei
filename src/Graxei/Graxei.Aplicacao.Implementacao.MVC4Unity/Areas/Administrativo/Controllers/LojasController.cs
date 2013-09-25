using System.Collections.Generic;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models;
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
        public ActionResult Novo(string nomeLoja = "")
        {
            Loja loja = new Loja() {Nome = nomeLoja};
            LojaNovosEnderecosModel item = new LojaNovosEnderecosModel()
                                               {
                                                   Loja = loja,
                                                   NovosEnderecoModel = Enderecos
                                               };
            return View(item);
        }

        [HttpPost]
        public ActionResult Novo(Loja model)
        {
            foreach (Endereco endereco in model.Enderecos)
            {
                endereco.Loja = model;
                //model.Loja.AdicionarEndereco(endereco);
                _servicoEnderecos.Salvar(endereco);
            }
            _servicoLojas.Salvar(model);
            return View("Salvo");
        }

        #endregion  
        #region Atributos Privados
        private readonly IServicoLojas _servicoLojas;
        private readonly IServicoEnderecos _servicoEnderecos;
        #endregion

        private List<ItemListaNovosEnderecosModel> Enderecos
        {
            get
            {
                if (Session[ItensSessao.EnderecosNovaLoja] == null)
                {
                    Session[ItensSessao.EnderecosNovaLoja] = new List<ItemListaNovosEnderecosModel>();
                }
                return (List<ItemListaNovosEnderecosModel>)Session[ItensSessao.EnderecosNovaLoja];
            }
            set
            {
                Session[ItensSessao.EnderecosNovaLoja] = value;
            }
        }
        

    }
}
