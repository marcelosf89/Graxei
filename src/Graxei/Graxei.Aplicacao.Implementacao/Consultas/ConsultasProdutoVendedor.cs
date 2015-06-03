using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Transversais.ContratosDeDados.Api.PesquisaProdutos;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasProdutoVendedor : IConsultasProdutoVendedor
    {
        public ConsultasProdutoVendedor(IServicoProdutoVendedor servicoProdutoVendedor, IPesquisaProduto pesquisaProduto)
        {
            ServicoProdutoVendedor = servicoProdutoVendedor;
            _pesquisaProduto = pesquisaProduto;

        }
        
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

            HistoricoPesquisa historicoPesquisa = new HistoricoPesquisa
            {
                Criterio = texto,
                InternetProtocol = "192.198.0.1",
                DataPesquisa = DateTime.Now
            };
            _pesquisaProduto.RegistrarAsync(historicoPesquisa);


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

        private IPesquisaProduto _pesquisaProduto;
    }
}