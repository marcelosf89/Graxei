using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Comum.Teste
{
    public class PesquisaContratoComum
    {
        public static IList<PesquisaContrato> GetLista()
        {
            IList<PesquisaContrato> retorno = new List<PesquisaContrato>();
            PesquisaContrato pesquisaContrato = new PesquisaContrato
            {
                Id = 1,
                Codigo = "Codigo"
            };
            retorno.Add(pesquisaContrato);
            pesquisaContrato = new PesquisaContrato
            {
                Id = 2,
                Codigo = "Codigo2"
            };
            retorno.Add(pesquisaContrato);
            return retorno;
        }

        public static bool AssertListaPesquisaContrato(ListaPesquisaContrato esperado, ListaPesquisaContrato real)
        {
            return esperado.Lista.Select(p => p.Id).SequenceEqual(real.Lista.Select(p => p.Id)) && real.Lista.Select(p => p.Id).SequenceEqual(esperado.Lista.Select(p => p.Id))
                && esperado.Total.Valor == real.Total.Valor && esperado.Atual.Valor == real.Atual.Valor;
        }
    }
}
