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
            return GetSessaoAtual().Query<Usuario>().SingleOrDefault<Usuario>(p => p.Login.Trim().ToLower().Equals(login.Trim().ToLower()));
        }

        public Usuario GetPorNome(string nome)
        {
            return GetSessaoAtual().Query<Usuario>()
                .Where(p => p.Nome.Trim().ToLower().Equals(nome.Trim().ToLower()))
                .SingleOrDefault<Usuario>();
        }

        public Usuario GetPorEmail(string email)
        {
            return GetSessaoAtual().Query<Usuario>()
                .Where(p => p.Email.Trim().ToLower().Equals(email.Trim().ToLower()))
                .SingleOrDefault<Usuario>();
        }

        #endregion
    }
}