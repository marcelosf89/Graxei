using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasProdutoVendedor
    {
        IList<PesquisaContrato> Get(string texto);
        IList<PesquisaContrato> Get(string txtSearch, string pais, string cidade, int page);
    }
}