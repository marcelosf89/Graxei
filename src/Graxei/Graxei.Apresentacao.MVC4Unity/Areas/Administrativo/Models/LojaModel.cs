
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class LojaModel
    {
        public LojaModel()
        {
            Loja = new Loja();
            EnderecosModel = new EnderecosModel();
        }
        public Loja Loja { get; set; }
        public EnderecosModel EnderecosModel { get; set; }

    }
}