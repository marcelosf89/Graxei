using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class TelefoneModel
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TelefoneObrigatorio")]
        public string Telefone { get; set; }
    }
}