﻿using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class EnderecoIndiceModel
    {
        public int IdLista { get; set; }
        public int IdEstado { get; set; }
        public long IdBairro { get; set; }
        public Telefone Telefone { get; set; }
        public IList<Telefone> Telefones { get; set; }
        public Endereco Endereco { get; set; }
    }
}