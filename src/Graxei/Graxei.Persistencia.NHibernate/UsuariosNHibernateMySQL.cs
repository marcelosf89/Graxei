using System.Linq;
using FAST.Modelo;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class UsuariosNHibernateMySQL : PadraoNHibernateMySQL<Usuario>, IRepositorioUsuarios
    {

        #region Implementação of IRepositorioUsuarios<T>

        public Usuario GetPorLogin(string login)
        {
            return SessaoAtual.Query<Usuario>().SingleOrDefault<Usuario>(p => p.Login.Trim().ToLower() == login.Trim().ToLower());
        }

        public Usuario GetPorNome(string nome)
        {
            return SessaoAtual.QueryOver<Usuario>()
                .Where(p => p.Nome.Trim().ToLower() == nome.Trim().ToLower())
                .SingleOrDefault<Usuario>();
        }

        public Usuario GetPorEmail(string email)
        {
            return SessaoAtual.QueryOver<Usuario>()
                .Where(p => p.Email.Trim().ToLower() == email.Trim().ToLower())
                .SingleOrDefault<Usuario>();
        }

        #endregion
    }
}