using Graxei.Modelo.Generico;

namespace Graxei.Modelo
{
    public class Atributo : ExclusaoLogica
    {
        public virtual string Nome { get; set; }
        public virtual string Rotulo { get; set; }
        public virtual int Tamanho { get; set; }
        public virtual ProdutoVendedor ProdutoVendedor { get; protected internal set; }
    }
}
