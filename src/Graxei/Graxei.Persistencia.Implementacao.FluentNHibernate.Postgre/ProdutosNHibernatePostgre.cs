﻿using System;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    /// <summary>
    /// Classe de implementação das funções relativas à entidade Produto
    /// </summary>
    public class ProdutosNHibernatePostgre : PadraoNHibernatePostgreLeitura<Produto>, IRepositorioProdutos
    {
        #region Implementação de IRepositorioProdutos

        public Produto GetPorDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                throw new ArgumentNullException("descricao");
            }
            return SessaoAtual.Query<Produto>()
                              .SingleOrDefault<Produto>(p => p.Descricao.Trim().ToLower() == descricao);
        }

        #endregion
    }

}