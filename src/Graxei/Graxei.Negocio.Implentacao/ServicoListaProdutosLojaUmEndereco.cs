﻿using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoListaProdutosLojaUmEndereco : IServicoListaProdutosLoja
    {
        private IRepositorioListaProdutosLoja _repositorioListaProdutosLoja;

        public ServicoListaProdutosLojaUmEndereco(IRepositorioListaProdutosLoja repositorioListaProdutosLoja)
        {
            _repositorioListaProdutosLoja = repositorioListaProdutosLoja;
        }

        public ListaProdutosLoja Get(string criterio, long idLoja, int pagina, int tamanhoPagina, int totalElementos)
        {
            return _repositorioListaProdutosLoja.GetSomenteUmEndereco(criterio, idLoja, pagina, tamanhoPagina, totalElementos);
        }
    }
}
