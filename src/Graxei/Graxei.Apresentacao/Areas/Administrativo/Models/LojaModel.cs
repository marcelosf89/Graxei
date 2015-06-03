using System.Collections.Generic;
using System.Linq;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class LojaModel
    {
        public LojaModel()
        {
            LojaContrato = new LojaContrato();
            EnderecoModel = new EnderecoVistaContrato();
            ListEnderecosModel = new List<EnderecoVistaContrato>();
            ListaEnderecos = new List<EnderecoListaContrato>();
        }
        public LojaContrato LojaContrato { get; set; }
        public EnderecoVistaContrato EnderecoModel { get; set; }
        public List<EnderecoVistaContrato> ListEnderecosModel { get; set; }
        public List<EnderecoListaContrato> ListaEnderecos { get; set; }
        public bool HaEnderecos()
        {
            return (ListEnderecosModel != null && ListEnderecosModel.Any());
        }
    }
}