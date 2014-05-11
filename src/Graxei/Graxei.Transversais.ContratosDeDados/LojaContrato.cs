using Graxei.Transversais.Idiomas;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Transversais.ContratosDeDados
{
    public class LojaContrato
    {
       public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeLojaObrigatorio")]
        public string Nome { get; set; }

        public virtual byte[] Logotipo { get; set; }

    }
}