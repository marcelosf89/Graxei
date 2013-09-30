
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models
{
    public class NovaLojaModel
    {
        public NovaLojaModel()
        {
            Loja = new Loja();
            NovosEnderecosModel = new NovosEnderecosModel();
        }
        public Loja Loja { get; set; }
        public NovosEnderecosModel NovosEnderecosModel { get; set; }

    }
}