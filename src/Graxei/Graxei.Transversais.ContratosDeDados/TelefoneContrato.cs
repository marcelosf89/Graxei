using Graxei.Transversais.Idiomas;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Transversais.ContratosDeDados
{
    public class TelefoneContrato
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NumeroTelefoneObrigatorio")]
        [StringLength(25, ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TamanhoMaximoNumeroTelefone")]
        public string Numero { get; set; }
    }
}
