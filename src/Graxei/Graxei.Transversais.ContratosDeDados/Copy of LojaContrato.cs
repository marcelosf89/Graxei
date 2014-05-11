using Graxei.Transversais.Idiomas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados
{
    public class LojaContratox
    {
        public LojaContratox()
        {
            _enderecosContrato = new List<EnderecoContrato>();
        }

        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeLojaObrigatorio")]
        public string Nome { get; set; }

        public virtual byte[] Logotipo { get; set; }

        public List<EnderecoContrato> EnderecosContrato
        {
            get
            {
                return new List<EnderecoContrato>(_enderecosContrato);
            }
        }

        public void AdicionarEnderecoContrato(EnderecoContrato enderecoContrato)
        {
            enderecoContrato.Indice = _enderecosContrato.Count() + 1;
            EnderecosContrato.Add(enderecoContrato);
        }

        public void ExcluirEnderecoContrato(int indice)
        {
            _enderecosContrato.RemoveAll(p => p.Id == indice);
        }

        private List<EnderecoContrato> _enderecosContrato;
    }
}