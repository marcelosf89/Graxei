using System.Collections.Generic;
using System.Linq;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class LojaModel
    {
        public LojaModel()
        {
            LojaContrato = new LojaContrato();
            EnderecoModel = new EnderecoModel();
            ListEnderecosModel = new List<EnderecoModel>();
            ListaEnderecos = new List<EnderecoListaContrato>();
        }
        public LojaContrato LojaContrato { get; set; }
        public EnderecoModel EnderecoModel { get; set; }
        public List<EnderecoModel> ListEnderecosModel { get; set; }
        public List<EnderecoListaContrato> ListaEnderecos { get; set; }
        public bool HaEnderecos()
        {
            return (ListEnderecosModel != null && ListEnderecosModel.Any());
        }
    }
}