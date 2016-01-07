using Graxei.Transversais.Idiomas;
using System;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Apresentacao.Models
{
    public class ContatoModel
    {
        public String Assunto { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeObrigatorio")]
        public String Nome { get; set; }
        public String Mensagem { get; set; }
        public String Email { get; set; }
    }
}