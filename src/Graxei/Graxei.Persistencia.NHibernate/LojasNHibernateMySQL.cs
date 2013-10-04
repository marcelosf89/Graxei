using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojasNHibernateMySQL : PadraoNHibernateMySQL<Loja>, IRepositorioLojas
    {

        #region Implementação de IRepositorioLojas

        public void Salvar(IList<LojaUsuario> lojasUsuarios)
        {
            foreach (LojaUsuario lojaUsuario in lojasUsuarios)
            {
                SessaoAtual.SaveOrUpdate(lojaUsuario);
            }
        }

        public Loja Get(string nome)
        {
            return SessaoAtual.Query<Loja>()
                              .SingleOrDefault<Loja>(loja => loja.Nome.Trim().ToLower() == nome.Trim().ToLower());
        }

        #endregion
    }
}
