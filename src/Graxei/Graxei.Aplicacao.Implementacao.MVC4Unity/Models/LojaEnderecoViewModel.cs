using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Models
{
    public class LojaEnderecoViewModel
    {
        public string NomeLoja { get; set; }
        public Endereco Endereco { get; set; }
        public int IdEstado { get; set; }

    }
}