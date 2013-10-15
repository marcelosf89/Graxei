using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojasNHibernateMySQL : PadraoNHibernateMySQL<Loja>, IRepositorioLojas
    {
        public LojasNHibernateMySQL(IRepositorioProdutoVendedor repositorioProdutoVendedor)
        {
            _repositorioProdutoVendedor = repositorioProdutoVendedor;
        }

        #region Implementação de IRepositorioLojas

        public void Salvar(IList<LojaUsuario> lojasUsuarios)
        {
            foreach (LojaUsuario lojaUsuario in lojasUsuarios)
            {
                SessaoAtual.SaveOrUpdate(lojaUsuario);
            }
        }

        public Loja Get(string nome)
        {
            return SessaoAtual.Query<Loja>()
                              .SingleOrDefault<Loja>(loja => loja.Nome.Trim().ToLower() == nome.Trim().ToLower());
        }

        #endregion

        public new void Excluir(Loja loja)
        {
            loja.Excluida = true;
            foreach (Endereco e in loja.Enderecos)
            {
                e.Excluida = true;
            }
            _repositorioProdutoVendedor.ExcluirDe(loja);
            Salvar(loja);
        }

        #region Atributos Privados

        private readonly IRepositorioProdutoVendedor _repositorioProdutoVendedor;

        #endregion
    }
}
