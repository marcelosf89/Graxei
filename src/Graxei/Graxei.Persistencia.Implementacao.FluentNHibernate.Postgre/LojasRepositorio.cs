using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate.Postgre
{
    public class LojasRepositorio : PadraoNHibernatePostgre<Loja>, IRepositorioLojas
    {
        public LojasRepositorio(IRepositorioProdutoVendedor repositorioProdutoVendedor)
        {
            _repositorioProdutoVendedor = repositorioProdutoVendedor;
        }

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

        public IList<long> GetIdsEnderecos(long idLoja)
        {
            return SessaoAtual.QueryOver<Endereco>().Where(p => p.Loja.Id == idLoja).Select(p => p.Id).List<long>();
        }

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

        public bool UsuarioAssociado(Loja loja, Usuario usuario)
        {
            Usuario usuarioResult = null;
            return SessaoAtual.QueryOver<Loja>().Where(p => p.Id == loja.Id)
                              .JoinQueryOver(q => q.Usuarios, () => usuarioResult)
                              .Where(() => usuarioResult.Id == usuario.Id).RowCount() > 0;
        }

        private readonly IRepositorioProdutoVendedor _repositorioProdutoVendedor;
    }
}
