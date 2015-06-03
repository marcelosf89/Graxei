using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasProdutos : IConsultasProdutos
    {
        public ConsultasProdutos(IServicoProdutos servicoProdutos)
        {
            ServicoProdutos = servicoProdutos;
        }

        public IServicoProdutos ServicoProdutos { get; private set; }

        public IList<Produto> Get(string texto, long page = 0)
        {
            IList<Produto> lp = ServicoProdutos.Get(texto, page);
            if (!lp.Any())
            {
                long max = ServicoProdutos.GetMax(texto);
                if (max == 0) return new List<Produto>();

                long maxpage = max / 10;
                lp = ServicoProdutos.Get(texto, Convert.ToInt32(maxpage));
                throw new ProdutoForaDoLimiteException(lp, maxpage);
            }

            return lp;
        }

    }
}