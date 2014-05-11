using Graxei.Transversais.Idiomas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.ContratosDeDados
{
    /// <summary>
    /// Contrato de dados para cadastro de Endereços
    /// </summary>
    public class EnderecoContrato
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "EstadoObrigatoria")]
        public int IdEstado { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "CidadeObrigatoria")]
        public string Cidade { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "BairroObrigatorio")]
        public string Bairro { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NumeroObrigatorio")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}
