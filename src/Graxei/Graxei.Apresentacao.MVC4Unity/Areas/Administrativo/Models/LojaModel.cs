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
            EnderecoContratoForm = new EnderecoContrato();
            EnderecosContrato = new List<EnderecoContrato>();
        }
        public LojaContrato LojaContrato { get; set; }
        public EnderecoContrato EnderecoContratoForm { get; set; }
        public List<EnderecoContrato> EnderecosContrato { get; set; }

        public bool HaEnderecos()
        {
            return (EnderecosContrato != null && EnderecosContrato.Any());
        }
    }
}