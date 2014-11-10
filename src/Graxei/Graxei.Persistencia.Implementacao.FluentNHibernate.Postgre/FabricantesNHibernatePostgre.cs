using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    /// <summary>
    /// Classe de implementação das funções relativas à entidade Fabricante
    /// </summary>
    public class FabricantesNHibernatePostgre : PadraoNHibernatePostgre<Fabricante>, IRepositorioFabricantes
    {

        /// <summary>
        /// Recupera todos os nomes de fabricantes existentes
        /// </summary>
        /// <returns></returns>
        public IList<string> TodosNomes()
        {
            return SessaoAtual.QueryOver<Fabricante>().Select(p => p.Nome).List<string>();
        }
    }
}