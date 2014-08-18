using FAST.Modelo;
namespace Graxei.Modelo
{
    public sealed class Usuario : Entidade
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }
    }
}
