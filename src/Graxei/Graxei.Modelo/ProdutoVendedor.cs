using Graxei.Transversais.Idiomas;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Modelo
{
    public class ProdutoVendedor
    {
        [Display(ResourceType = typeof(Propriedades), Name="Preco")]
        public virtual double Preco { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual UnidadeMedida UnidadeEntrada { get; set; }
        public virtual UnidadeMedida UnidadeSaida { get; set; }

    }
}
