using Graxei.Modelo.Generico;

namespace Graxei.Modelo
{

    public class Telefone : Entidade
    {
        public virtual string Numero { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual TipoTelefone TipoTelefone { get; set; }
    }

}