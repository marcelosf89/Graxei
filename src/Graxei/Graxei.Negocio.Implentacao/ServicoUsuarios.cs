using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

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

        #endregion


        #region Atributos privados
        private readonly IRepositorioUsuarios _reposUsuarios;
        #endregion

    }
}