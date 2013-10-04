
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class NovaLoja
    {
        public NovaLoja()
        {
            Loja = new Loja();
            NovosEnderecosModel = new NovosEnderecosModel();
        }
        public Loja Loja { get; set; }
        public NovosEnderecosModel NovosEnderecosModel { get; set; }

    }
}