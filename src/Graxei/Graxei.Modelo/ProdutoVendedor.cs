using System.Collections.Generic;
using FAST.Modelo;
using Graxei.Transversais.Idiomas;
using System.ComponentModel.DataAnnotations;
using Graxei.Search.Attributes;

namespace Graxei.Modelo
{
    [Indexed]
    public class ProdutoVendedor : ExclusaoLogica
    {

        [DocumentId]
        public override long Id { get; protected set; }

        [Display(ResourceType = typeof(Propriedades), Name = "Descricao")]
        [Required(ErrorMessageResourceName = "DescricaoNula", ErrorMessageResourceType = typeof(Erros))]
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Descricao { get; set; }

        [Display(ResourceType = typeof(Propriedades), Name="Preco")]
        [Required(ErrorMessageResourceName = "PrecoNulo", ErrorMessageResourceType = typeof(Erros))]
        [Range(0, double.MaxValue, ErrorMessageResourceName = "ValorMaiorZero", ErrorMessageResourceType = typeof(Erros))]
        public virtual double Preco { get; set; }

        [Display(ResourceType = typeof(Propriedades), Name = "FatorConversao")]
        public virtual double FatorConversao { get; set; }

        [IndexedEmbedded(Depth = 1)]
        public virtual Produto Produto { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual UnidadeMedida UnidadeEntrada { get; set; }
        public virtual UnidadeMedida UnidadeSaida { get; set; }
        public virtual IList<Atributo> Atributos { get; protected internal set; }

        public virtual void AdicionarAtributo(Atributo atributo)
        {
            if (Atributos == null)
            {
                Atributos = new List<Atributo>();
            }
            Atributos.Add(atributo);
            atributo.ProdutoVendedor = this;
        }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Produto))
            {
                return false;
            }
            Produto p = (Produto)obj;
            return this.Validar() && (p.Descricao == this.Descricao);
        }
        #endregion

        public virtual bool Validar()
        {
            return (!string.IsNullOrEmpty(Descricao)) &&
                   (this.Endereco != null && this.Endereco.Validar()) && (this.Produto != null && this.Produto.Validar());
        }

    }
}
