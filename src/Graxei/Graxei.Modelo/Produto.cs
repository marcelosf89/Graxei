using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public virtual string Codigo { get; set; }
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Descricao { get; set; }
        public virtual double Preco { get; set; }
        public virtual double FatorConversao { get; set; }
        [IndexedEmbedded(Depth = 1)]
        public virtual Fabricante Fabricante { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual UnidadeMedida UnidadeEntrada { get; set; }
        public virtual UnidadeMedida UnidadeSaida { get; set; }
    }
}
