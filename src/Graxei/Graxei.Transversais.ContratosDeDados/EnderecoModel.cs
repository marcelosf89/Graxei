using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Transversais.ContratosDeDados
{
    /// <summary>
    /// Contrato de dados para cadastro de Endereços
    /// </summary>
    public class EnderecoVistaContrato
    {
        private long _id;

        public string HashId { get; set; }

        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "EstadoObrigatorio")]
        [Display(ResourceType = typeof(Rotulos), Name = "Estado")]
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
        
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "LogradouroObrigatorio")]
        [StringLength(50, ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "TamanhoMaximoTelefones")]
        public string Cnpj { get; set; }

        public IList<TelefoneContrato> Telefones { get; set; }

    }
}
