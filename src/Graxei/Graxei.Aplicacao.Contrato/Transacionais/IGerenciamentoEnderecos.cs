﻿using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoEnderecos : ITransacional
    {
        Endereco Salvar(Endereco endereco);
    }
}
