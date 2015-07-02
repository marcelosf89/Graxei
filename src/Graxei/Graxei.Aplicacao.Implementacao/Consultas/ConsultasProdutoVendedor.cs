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
using System.Threading.Tasks;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.Comum.Data;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasProdutoVendedor : IConsultasProdutoVendedor
    {
        public ConsultasProdutoVendedor(IServicoProdutoVendedor servicoProdutoVendedor, IPesquisaProduto pesquisaProduto, IDataSistema dataSistema)
        {
            _servicoProdutoVendedor = servicoProdutoVendedor;
            _pesquisaProduto = pesquisaProduto;
            _dataSistema = dataSistema;
        }

        public ListaPesquisaContrato Get(string texto)
        {
            return _servicoProdutoVendedor.Get(texto);
        }

        public ListaPesquisaContrato Get(string texto, string pais, string cidade, int pagina, string ip)
        {
            HistoricoPesquisa historicoPesquisa = new HistoricoPesquisa
            {
                Criterio = texto,
                InternetProtocol = ip,
                DataPesquisa = _dataSistema.Agora
            };

            Task.Run(() => _pesquisaProduto.RegistrarAsync(historicoPesquisa));

            ListaPesquisaContrato retorno = _servicoProdutoVendedor.Get(texto, pagina);
            if (!retorno.Lista.Any())
            {
                retorno = _servicoProdutoVendedor.GetUltimaPagina(texto);
            }

            return retorno;
        }

        public long GetQuantidadeProduto()
        {
            return _servicoProdutoVendedor.GetQuantidadeProduto();
        }

        public long GetQuantidadeProduto(long lojaId)
        {
            return _servicoProdutoVendedor.GetQuantidadeProduto(lojaId);
        }

        private IPesquisaProduto _pesquisaProduto;

        private IServicoProdutoVendedor _servicoProdutoVendedor { get; set; }

        private IDataSistema _dataSistema;

    }
}