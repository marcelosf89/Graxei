using Graxei.Transversais.Idiomas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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