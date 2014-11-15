using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre
{
    public class TelefonesNHibernatePostgre : PadraoNHibernatePostgre<Telefone>, IRepositorioTelefones
    {

        #region Implementação de IRepositorioExcluir<Telefone>

        public IList<Telefone> Todos(long idEndereco)
        {
            return SessaoAtual.Query<Telefone>().Where(p => p.Endereco.Id == idEndereco).ToList();
        }

        #endregion
    }
}