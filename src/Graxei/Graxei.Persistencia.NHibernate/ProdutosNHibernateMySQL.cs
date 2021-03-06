﻿using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    /// <summary>
    /// Classe de implementação das funções relativas à entidade Produto
    /// </summary>
    public class ProdutosNHibernateMySQL : PadraoNHibernateMySQLLeitura<Produto>, IRepositorioProdutos
    {
        #region Implementação de IRepositorioProdutos

        public Produto GetPorDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                throw new ArgumentNullException("descricao");
            }
            return GetSessaoAtual().Query<Produto>()
                              .SingleOrDefault<Produto>(p => p.Descricao.Trim().ToLower() == descricao);
        }

        public long GetMax(string texto)
        {
            throw new NotImplementedException();
        }

        public IList<Produto> Get(string texto, long page)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
