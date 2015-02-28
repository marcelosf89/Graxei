using System.ComponentModel.DataAnnotations;
using Graxei.Modelo.Generico;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{
    public class Produto : ExclusaoLogica
    {
        public override long Id { get; set; }

        [Display(ResourceType = typeof(Propriedades), Name = "Codigo")]
        public virtual string Codigo { get; set; }

        [Display(ResourceType = typeof(Propriedades), Name = "Descricao")]
        public virtual string Descricao { get; set; }

        [Display(ResourceType = typeof(Propriedades), Name = "FatorConversao")]
        public virtual double FatorConversao { get; set; }

        public virtual Fabricante Fabricante { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual string Carros { get; set; }

        public virtual string Observacao { get; set; }

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
            return (!string.IsNullOrEmpty(Codigo) && (!string.IsNullOrEmpty(Descricao)));
        }
    }
}
