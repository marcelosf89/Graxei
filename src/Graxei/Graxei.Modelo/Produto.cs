using System.ComponentModel.DataAnnotations;
using FAST.Modelo;
using NHibernate.Search.Attributes;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{
    [Indexed]
    public class Produto : Entidade
    {
        [DocumentId]
        public override long Id { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        [Display(ResourceType = typeof(Propriedades), Name = "Codigo")]
        public virtual string Codigo { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        [Display(ResourceType = typeof(Propriedades), Name = "Descricao")]
        public virtual string Descricao { get; set; }
        [Display(ResourceType = typeof(Propriedades), Name = "FatorConversao")]
        public virtual double FatorConversao { get; set; }
        [IndexedEmbedded(Depth = 1)]
        public virtual Fabricante Fabricante { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual UnidadeMedida UnidadeEntrada { get; set; }
        public virtual UnidadeMedida UnidadeSaida { get; set; }
    }
}
