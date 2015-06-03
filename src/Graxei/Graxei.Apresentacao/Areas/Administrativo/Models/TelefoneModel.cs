using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class TelefoneModel
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TelefoneObrigatorio")]
        public string Telefone { get; set; }
    }
}