﻿using System.Collections.Generic;
using System.Dynamic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultaCidades
    {
        IList<Cidade> GetPorEstado(long idEstado);
        Cidade Get(string nome, long idEstado);
    }
}