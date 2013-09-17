using FAST.Modelo;
using Graxei.Transversais.Idiomas;
using NHibernate.Search.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Modelo
{
    [Indexed]
    public class Fabricante : Entidade
    {
        [DocumentId]
        public new virtual long Id { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        [Display(ResourceType = typeof(Propriedades), Name = "Nome")]
        public virtual string Nome { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Fabricante))
            {
                return false;
            }
            Fabricante fb = (Fabricante)obj;
            return (fb.Nome == this.Nome);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(Nome)))
            {
                retorno += Nome.GetHashCode();
            }
            return retorno + 11;
        }
        #endregion

    }

}
