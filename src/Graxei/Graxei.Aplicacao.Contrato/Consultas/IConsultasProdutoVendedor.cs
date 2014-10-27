using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasProdutoVendedor
    {
        IList<ProdutoVendedor> Get(string texto);
    }
}