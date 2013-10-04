using System;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojaUsuarioNHibernateMySQL : PadraoNHibernateMySQL<LojaUsuario>, IRepositorioLojaUsuario
    {
        #region Implementation of IRepositorioLojaUsuario

        public bool Existe(Loja loja, Usuario usuario)
        {
            Validar(loja, usuario);
            return SessaoAtual.Query<LojaUsuario>().Count(
                p => p.Loja.Nome.Trim().ToLower() == loja.Nome.Trim().ToLower()
                     && p.Usuario.Nome.Trim().ToLower() == usuario.Login.Trim().ToLower()) > 0;
        }

        #endregion

        #region Métodos Privados
        private void Validar(Loja loja, Usuario usuario)
        {
            if (loja == null || string.IsNullOrEmpty(loja.Nome))
            {
                throw new InvalidOperationException(Erros.LojaNomeNulo);
            }
            if (usuario == null || string.IsNullOrEmpty(usuario.Login))
            {
                throw new InvalidOperationException(Erros.UsuarioLoginNulo);
            }
        }

        #endregion
    }
}