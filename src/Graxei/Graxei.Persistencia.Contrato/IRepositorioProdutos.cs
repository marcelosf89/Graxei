﻿using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioProdutos : IRepositorioEntidades<Produto>
    {
        Produto GetPorDescricao(string descricao);
    }
}
