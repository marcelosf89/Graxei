using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoUsuarios : ServicoPadraoEntidades<Usuario>, IServicoUsuarios
    {
        public ServicoUsuarios(IRepositorioUsuarios reposUsuarios)
        {
            RepositorioEntidades = reposUsuarios;
        }

        #region Implementação de IServicoUsuarios

        public Usuario GetPorLogin(string login)
        {
            return RepositorioUsuarios.GetPorLogin(login);
        }

        public Usuario GetPorNome(string nome)
        {
            return RepositorioUsuarios.GetPorNome(nome);
        }

        public Usuario GetPorEmail(string email)
        {
            return RepositorioUsuarios.GetPorEmail(email);
        }

        public Usuario AutenticarPorLogin(string login, string senha)
        {
            Usuario usuario = RepositorioUsuarios.GetPorLogin(login);
            if (usuario == null)
            {
                return null;// throw new AutenticacaoException(Erros.AutenticacaoLogin);
            }
            if (usuario.Senha != senha)
            {
                throw new AutenticacaoException(Erros.AutenticacaoLogin);
            }
            return usuario;
        }

        public Usuario AutenticarPorEmail(string email, string senha)
        {
            Usuario usuario = RepositorioUsuarios.GetPorEmail(email);
            if (usuario == null)
            {
                throw new AutenticacaoException(Erros.AutenticacaoEmail);
            }
            if (usuario.Senha != senha)
            {
                throw new AutenticacaoException(Erros.AutenticacaoEmail);
            }
            return usuario;
        }

        #endregion

        #region Propriedades Privadas
        private IRepositorioUsuarios RepositorioUsuarios{ get { return (IRepositorioUsuarios)RepositorioEntidades; } }
        #endregion

        #region Overrides of ServicoPadraoEntidades<Usuario>

        public override void PreSalvar(Usuario t)
        {
            throw new System.NotImplementedException();
        }

        public override void PreAtualizar(Usuario t)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}