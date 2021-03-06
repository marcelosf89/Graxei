using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;
using Graxei.Persistencia.Implementacao.NHibernate;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre
{
    public class EnderecosRepositorio : PadraoNHibernatePostgre<Endereco>, IRepositorioEnderecos
    {

        public EnderecosRepositorio(IRepositorioEstados repoEstados, IRepositorioCidades repoCidades, IRepositorioBairros repoBairros)
        {
            _repoEstados = repoEstados;
            _repoCidades = repoCidades;
            _repoBairros = repoBairros;
        }

        public IList<Endereco> Todos(Loja loja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja.Equals(loja))
                              .ToList();
        }

        public IList<Endereco> Todos(long idLoja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja != null && p.Loja.Id == idLoja)
                              .ToList();
        }

        public Endereco Get(long idLoja, string logradouro, string numero, string complemento, long idBairro)
        {
            return
                SessaoAtual.Query<Endereco>().SingleOrDefault(p => p.Loja.Id == idLoja && p.Logradouro == logradouro && p.Numero == numero &&
                                                                   p.Complemento == complemento && p.Bairro.Id == idBairro);
        }

        public List<Endereco> GetPorLoja(long idLoja)
        {
            return SessaoAtual.Query<Endereco>().Where(p => p.Loja.Id == idLoja).ToList();
        }

        public Endereco Get(long idEndereco)
        {
            return SessaoAtual.Query<Endereco>().FirstOrDefault(p => p.Id == idEndereco);
        }

        public bool UsuarioAssociado(Endereco endereco, Usuario usuario)
        {
            Usuario usuarioResult = null;
            return SessaoAtual.QueryOver<Loja>().Where(p => p.Id == endereco.Loja.Id)
                              .JoinQueryOver(q => q.Usuarios, () => usuarioResult)
                              .Where(() => usuarioResult.Id == usuario.Id).RowCount() > 0;
        }

        private readonly IRepositorioEstados _repoEstados;
        private readonly IRepositorioCidades _repoCidades;
        private readonly IRepositorioBairros _repoBairros;
    }
}