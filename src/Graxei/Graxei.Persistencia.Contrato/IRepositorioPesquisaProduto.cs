using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioPesquisaProduto
    {
        IList<PesquisaContrato> GetPorDescricaoPesquisa(string descricao, string pais, string cidade, int page);
        
        ListaPesquisaContrato GetUltimaPagina(int tamanhoPagina, string texto, string pais, string cidade);
    }
}
