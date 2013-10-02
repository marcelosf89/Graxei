using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class EnderecoIndiceModel
    {
        public int IdLista { get; set; }
        public int IdEstado { get; set; }
        public long IdBairro { get; set; }
        public Endereco Endereco { get; set; }
    }
}