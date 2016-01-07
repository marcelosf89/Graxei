using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasProdutos
    {
        IList<Produto> Get(string txtSearch, long page);

    }
}