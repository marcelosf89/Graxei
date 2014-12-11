using System.Collections.Generic;
using Graxei.Transversais.Idiomas;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Transversais.ContratosDeDados
{
    public class LojaContrato
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeLojaObrigatorio")]
        public string Nome { get; set; }

        public List<EnderecoListaContrato> EnderecosListaContrato
        {
            get
            {
                if (_enderecosListaContrato == null)
                {
                    _enderecosListaContrato = new List<EnderecoListaContrato>();
                }
                List<EnderecoListaContrato> retorno = new List<EnderecoListaContrato>();
                retorno.AddRange(_enderecosListaContrato);
                return retorno;
            }
        }

        public void AdicionarEndereco(EnderecoListaContrato enderecoListaContrato)
        {
            if (_enderecosListaContrato == null)
            {
                _enderecosListaContrato = new List<EnderecoListaContrato>();
            }
            _enderecosListaContrato.Add(enderecoListaContrato);
        }

        private List<EnderecoListaContrato> _enderecosListaContrato;
    }
}