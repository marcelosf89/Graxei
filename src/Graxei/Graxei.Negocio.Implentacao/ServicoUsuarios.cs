using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoUsuarios : ServicoPadraoEntidades<Usuario>, IServicoUsuarios
    {
        public ServicoUsuarios(IRepositorioUsuarios reposUsuarios)
        {
            _reposUsuarios = reposUsuarios;
        }

        #region Implementação de IServicoUsuarios

        public Usuario GetPorLogin(string login)
        {
            return _reposUsuarios.GetPorLogin(login);
        }

        public Usuario GetPorNome(string nome)
        {
            return _reposUsuarios.GetPorNome(nome);
        }

        public Usuario GetPorEmail(string email)
        {
            return _reposUsuarios.GetPorEmail(email);
        }

        public Usuario AutenticarPorLogin(string login, string senha)
        {
            Usuario usuario = _reposUsuarios.GetPorLogin(login);
            if (usuario == null)
            {
                throw new AutenticacaoException(Erros.AutenticacaoLogin);
            }
            if (usuario.Senha != senha)
            {
                throw new AutenticacaoException(Erros.AutenticacaoLogin);
            }
            return usuario;
        }

        public Usuario AutenticarPorEmail(string email, string senha)
        {
            Usuario usuario = _reposUsuarios.GetPorEmail(email);
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


        #region Atributos privados
        private readonly IRepositorioUsuarios _reposUsuarios;
        #endregion

    }
}