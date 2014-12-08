using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class TelefonesNHibernateMySQL : PadraoNHibernateMySQL<Telefone>, IRepositorioTelefones
    {

        #region Implementação de IRepositorioExcluir<Telefone>

        public IList<Telefone> Todos(long idEndereco)
        {
            return GetSessaoAtual().Query<Telefone>().Where(p => p.Endereco.Id == idEndereco).ToList();
        }

        #endregion
    }
}