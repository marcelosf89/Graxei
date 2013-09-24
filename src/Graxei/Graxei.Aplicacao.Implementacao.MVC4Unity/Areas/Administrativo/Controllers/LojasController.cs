using System.Collections.Generic;
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

        public ActionResult Novo(string nomeLoja = "")
        {
            Loja loja = new Loja() {Nome = nomeLoja};
            /* TODO: ver como se faz o tratamento de listas com o NHibernate*/
            loja.Enderecos = Enderecos;
            return View(loja);
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

        #region Atributos Privados
        private readonly IServicoLojas _servicoLojas;
        private readonly IServicoEnderecos _servicoEnderecos;
        #endregion

        private IList<Endereco> Enderecos
        {
            get
            {
                object enderecos = Session["EnderecosNovaLoja"];
                if (enderecos == null)
                {
                    enderecos = new List<Endereco>();
                }
                return (IList<Endereco>)Session["EnderecosNovaLoja"];
            }
            set
            {
                Session["EnderecosNovaLoja"] = value;
            }
        }
        

    }
}
