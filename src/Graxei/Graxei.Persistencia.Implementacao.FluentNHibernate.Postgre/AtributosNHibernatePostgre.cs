using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Comum.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class AtributosNHibernatePostgre : PadraoNHibernatePostgreLeitura<Atributo>, IRepositorioAtributos
    {
        #region Implementation of IRepositorioAtributos
        public IList<Atributo> Todos(string descricaoProduto, string nomeLoja)
        {
            return SessaoAtual.Query<Atributo>()
                .Where(
                    p =>
                    p.ProdutoVendedor.Descricao.Trim().ToLower() == descricaoProduto.Trim().ToLower() &&
                    p.ProdutoVendedor.Endereco.Loja.Nome == nomeLoja).ToList<Atributo>();
        }

        public IList<Atributo> Todos(ProdutoVendedor produtoVendedor)
        {
            if (!UtilidadeEntidades.IsTransiente(produtoVendedor) && produtoVendedor.Validar())
            {
                return Todos(produtoVendedor.Descricao, produtoVendedor.Endereco.Loja.Nome);
            }
            return SessaoAtual.Query<Atributo>()
                .Where(p => p.ProdutoVendedor.Id == produtoVendedor.Id).ToList<Atributo>();
        }
        #endregion
    }
}