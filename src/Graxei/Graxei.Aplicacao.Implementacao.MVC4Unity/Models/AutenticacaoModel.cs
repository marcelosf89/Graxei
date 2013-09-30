using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Models
{
    public class AutenticacaoModel
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "UsuarioEmailObrigatorio")]
        public string LoginOuEmail { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "SenhaObrigatoria")]
        public string Senha { get; set; }
    }
}