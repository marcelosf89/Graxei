using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class EnderecoIndiceModel
    {
        public EnderecoIndiceModel()
        {
            Telefones = new List<string>();
        }

        public long IdLista { get; set; }
        public long IdEstado { get; set; }
        public long IdBairro { get; set; }
        public IList<string> Telefones { get; set; }
        public Endereco Endereco { get; set; }

    }
}