﻿using FAST.Modelo;
namespace Graxei.Modelo
{
    public class Usuario : Entidade
    {
        public virtual string Login { get; set; }
        public virtual string Email { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Senha { get; set; }
    }
}
