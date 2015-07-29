using Graxei.Transversais.Idiomas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados
{
    public class TelefoneContrato
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NumeroObrigatorio")]
        [StringLength(25, ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TamanhoMaximoNumeroTelefone")]
        public string Numero { get; set; }
    }
}
