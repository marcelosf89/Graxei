﻿using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLojas : IServicoEntidades<Loja>
    {
        Loja Get(string nome);
        void Salvar(Loja loja, Usuario usuario);
        void Salvar(Loja loja, IList<Usuario> usuario);
        void Salvar(Loja loja, Usuario usuario, IList<Endereco> enderecos);
        void Salvar(Loja loja, IList<Usuario> usuarios, IList<Endereco> enderecos);
        IServicoEnderecos ServicoEnderecos { get; }
    }
}
