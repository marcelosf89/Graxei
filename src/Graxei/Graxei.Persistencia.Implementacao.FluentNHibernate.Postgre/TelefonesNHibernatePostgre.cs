using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class TelefonesNHibernatePostgre : PadraoNHibernatePostgre<Telefone>, IRepositorioTelefones
    {

        #region Implementa��o de IRepositorioExcluir<Telefone>

        public IList<Telefone> Todos(long idEndereco)
        {
            return SessaoAtual.Query<Telefone>().Where(p => p.Endereco.Id == idEndereco).ToList();
        }

        #endregion
    }
}