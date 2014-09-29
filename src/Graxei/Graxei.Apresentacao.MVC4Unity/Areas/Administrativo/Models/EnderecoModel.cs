using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    /// <summary>
    /// Contrato de dados para cadastro de Endereços
    /// </summary>
    public class EnderecoModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "EstadoObrigatorio")]
        public long IdEstado { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "CidadeObrigatoria")]
        public string Cidade { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "BairroObrigatorio")]
        public string Bairro { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "LogradouroObrigatorio")]
        [StringLength(250, ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TamanhoMaximo250")]
        public string Logradouro { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NumeroObrigatorio")]

        [StringLength(150, ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TamanhoMaximo250")]
        public string Numero { get; set; }

        [StringLength(250, ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TamanhoMaximo150")]
        public string Complemento { get; set; }

        public long IdLoja { get; set; }
    }
}
