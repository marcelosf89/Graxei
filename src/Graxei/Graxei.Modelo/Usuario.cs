using Graxei.Modelo.Generico;
namespace Graxei.Modelo
{
    public class Usuario : Entidade
    {
        public virtual string Login { get; set; }

        public virtual string Email { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Senha { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Usuario))
            {
                return false;
            }
            Usuario that = (Usuario)obj;
            return this.Login.Equals(that.Login);
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode() * (99 / 3);
        }
    }
}
