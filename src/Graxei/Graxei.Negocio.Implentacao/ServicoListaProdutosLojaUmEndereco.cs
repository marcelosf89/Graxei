﻿using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoListaProdutosLojaUmEndereco : IServicoListaProdutosLoja
    {
        private IRepositorioListaProdutosLoja _repositorioListaProdutosLoja;

        public ServicoListaProdutosLojaUmEndereco(IRepositorioListaProdutosLoja repositorioListaProdutosLoja)
        {
            _repositorioListaProdutosLoja = repositorioListaProdutosLoja;
        }

        public ListaProdutosLoja Get(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina)
        {
            return _repositorioListaProdutosLoja.GetSomenteUmEndereco(pesquisaProdutoContrato, tamanhoPagina);
        }
    }
}
