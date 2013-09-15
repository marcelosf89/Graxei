using System.ComponentModel.DataAnnotations;
using FAST.Modelo;
using NHibernate.Search.Attributes;

namespace Graxei.Modelo
{
    [Indexed]
    public class Produto : Entidade
    {
        [DocumentId]
        public override long Id { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        [Display(Name = "Código")]
        public virtual string Codigo { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        [Display(Name = "Descrição")]
        public virtual string Descricao { get; set; }
        [Display(Name = "Preço")]
        public virtual double Preco { get; set; }
        [Display(Name = "Fator de Conversão")]
        public virtual double FatorConversao { get; set; }
        [IndexedEmbedded(Depth = 1)]
        public virtual Fabricante Fabricante { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual UnidadeMedida UnidadeEntrada { get; set; }
        public virtual UnidadeMedida UnidadeSaida { get; set; }
    }
}
