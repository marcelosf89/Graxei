using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasProdutoVendedor : IConsultasProdutoVendedor
    {
        #region Construtor
        public ConsultasProdutoVendedor(IServicoProdutoVendedor servicoProdutoVendedor)
        {
            ServicoProdutoVendedor = servicoProdutoVendedor;
        }
        #endregion

        #region Implementação de IConsultasUsuarios
        public IServicoProdutoVendedor ServicoProdutoVendedor { get; private set; }

        public IList<ProdutoVendedor> Get(string texto)
        {
            return ServicoProdutoVendedor.Get(texto);
        }
        #endregion


    
    }
}