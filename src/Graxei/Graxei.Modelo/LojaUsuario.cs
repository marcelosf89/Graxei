using FAST.Modelo;
using System;
namespace Graxei.Modelo
{
    public class LojaUsuario : Entidade
    {
        public override long Id{ get; set; }
        public virtual Loja Loja { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual DateTime DataRegistro { get; set; }
        /// <summary>
        /// Usuário responsável por associar um usuário à loja. Para efeito de log.
        /// </summary>
        public virtual Usuario UsuarioLog { get; set; }
    }
}
