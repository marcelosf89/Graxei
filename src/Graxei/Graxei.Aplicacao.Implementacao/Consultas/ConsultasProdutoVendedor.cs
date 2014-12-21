using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IList<PesquisaContrato> Get(string texto)
        {
            return ServicoProdutoVendedor.Get(texto);
        }

        public IList<PesquisaContrato> Get(string texto, string pais, string cidade, int page)
        {
            IList<PesquisaContrato> lp = ServicoProdutoVendedor.Get(texto, pais, cidade, page);
            if (!lp.Any())
            {
                long max = ServicoProdutoVendedor.GetMax(texto, pais, cidade, page);
                if (max == 0) return new List<PesquisaContrato>();

                long maxpage = max / 10;
                lp = ServicoProdutoVendedor.Get(texto, pais, cidade, Convert.ToInt32(maxpage));
                throw new ForaDoLimiteException(lp, maxpage);
            }
            return lp;
        }

        public long GetQuantidadeProduto()
        {
            return ServicoProdutoVendedor.GetQuantidadeProduto();
        }

        public long GetQuantidadeProduto(long lojaId)
        {
            return ServicoProdutoVendedor.GetQuantidadeProduto(lojaId);
        }

        #endregion
    }
}