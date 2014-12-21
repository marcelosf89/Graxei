using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojasNHibernatePostgre : PadraoNHibernatePostgre<Loja>, IRepositorioLojas
    {
        public LojasNHibernatePostgre(IRepositorioProdutoVendedor repositorioProdutoVendedor)
        {
            _repositorioProdutoVendedor = repositorioProdutoVendedor;
        }

        #region Implementação de IRepositorioLojas

        public Loja Get(string nome)
        {
            return SessaoAtual.Query<Loja>()
                              .SingleOrDefault<Loja>(loja => loja.Nome.Trim().ToLower() == nome.Trim().ToLower());
        }

        public List<Usuario> GetUsuarios(Loja loja)
        {
            List<Usuario> usuarios = new List<Usuario>();
            Loja lojaSelecionada = SessaoAtual.Query<Loja>().Fetch(l => l.Usuarios).FirstOrDefault(p => p.Id == loja.Id);
            if (lojaSelecionada != null)
            {
                usuarios = lojaSelecionada.Usuarios.ToList();
            }
            return usuarios;
        }

        public List<Usuario> GetUsuarios(long idLoja)
        {
            throw new System.NotImplementedException();
        }

        public Loja GetComEnderecos(long id)
        {
            return SessaoAtual.QueryOver<Loja>().Where(p => p.Id == id).Fetch(p => p.Enderecos).Eager.SingleOrDefault();
        }

        public Loja GetPorUrl(string nome)
        {
            return SessaoAtual.QueryOver<Loja>().Where(p => p.Url == nome).SingleOrDefault();
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
