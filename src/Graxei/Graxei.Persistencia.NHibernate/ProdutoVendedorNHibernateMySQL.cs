using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using NHibernate.Linq;
using System.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{

    /// <summary>
    /// Classe de implementação das funções relativas à entidade ProdutoVendedor
    /// </summary>
    public class ProdutoVendedorNHibernateMySQL : PadraoNHibernateMySQL<ProdutoVendedor>, IRepositorioProdutoVendedor
    {
        #region Implementação de IRepositorioProdutoVendedor

        public IList<ProdutoVendedor> GetPorDescricao(string descricao)
        {
            return SessaoAtual.Query<ProdutoVendedor>().Where(p => p.Descricao != null
                                                                             &&
                                                                             p.Descricao.Trim().ToLower() ==
                                                                             descricao.Trim().ToLower()).ToList<ProdutoVendedor>();
        }

        public ProdutoVendedor GetPorDescricaoAndLoja(string descricao, string nomeLoja)
        {
            ProdutoVendedor pvl = SessaoAtual.Query<ProdutoVendedor>()
                                                 .SingleOrDefault(p => p.Descricao.Trim().ToLower() == descricao.Trim().ToLower()
                                                                    && p.Loja.Nome.Trim().ToLower() == nomeLoja.Trim().ToLower());
            return pvl;
        }

        public ProdutoVendedor GetPorDescricaoAndLoja(string descricao, Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentNullException("loja", Erros.LojaNula);
            }
            if (!loja.Validar())
            {
                throw new EntidadeInvalidaException(Erros.LojaInvalida);
            }
            ProdutoVendedor pvl = SessaoAtual.Query<ProdutoVendedor>()
                                             .SingleOrDefault(p => p.Descricao.Trim().ToLower() == descricao.Trim().ToLower() && p.Loja.Nome.Trim().ToLower() == loja.Nome.Trim().ToLower());
            return pvl;
        }

        #endregion
    }

}
