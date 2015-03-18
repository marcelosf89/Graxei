using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class UsuariosNHibernatePostgre : PadraoNHibernatePostgre<Usuario>, IRepositorioUsuarios
    {
        public Usuario GetPorLogin(string login)
        {
            return SessaoAtual.Query<Usuario>().SingleOrDefault<Usuario>(p => p.Login.Trim().ToLower().Equals(login.Trim().ToLower()));
        }

        public Usuario GetPorNome(string nome)
        {
            return SessaoAtual.Query<Usuario>()
                .Where(p => p.Nome.Trim().ToLower().Equals(nome.Trim().ToLower()))
                .SingleOrDefault<Usuario>();
        }

        public Usuario GetPorEmail(string email)
        {
            return SessaoAtual.Query<Usuario>()
                .Where(p => p.Email.Trim().ToLower().Equals(email.Trim().ToLower()))
                .SingleOrDefault<Usuario>();
        }

    }
}