using Graxei.Modelo.Generico;

namespace Graxei.Modelo
{
    public class Plano : Entidade
    {
        public virtual string Nome { get; set; }

        public virtual int QuantidadeProduto { get; set; }

        public virtual int QuantidadeFilial { get; set; }

        public virtual decimal Valor { get; set; }

        public virtual int Meses { get; set; }

        public virtual bool EstaAtivo { get; set; }
    }
}
