using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.Models
{
    public class AutenticacaoModel
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "UsuarioEmailObrigatorio")]
        [Display(ResourceType = typeof(Propriedades), Name = "LoginOuEmail")]
        public string LoginOuEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "SenhaObrigatoria")]
        [Display(ResourceType = typeof(Propriedades), Name = "Senha")]
        public string Senha { get; set; }
    }
}