using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Models
{
    public class LojaEnderecoViewModel
    {
        public Loja Loja { get; set; }
        public IList<Endereco> Enderecos { get; set; }
    }
}