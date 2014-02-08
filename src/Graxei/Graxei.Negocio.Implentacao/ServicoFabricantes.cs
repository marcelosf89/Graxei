using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using System.Collections.Generic;
namespace Graxei.Negocio.Implementacao
{
    public class ServicoFabricantes : ServicoPadraoEntidades<Fabricante>, IServicoFabricantes
    {

         public ServicoFabricantes(IRepositorioFabricantes reposFabricantes)
        {
             /*** TODO: iniciar RepositorioEntidades */
            _reposFabricante = reposFabricantes;
        }

        #region Métodos sobrescritos
        public Fabricante GetPorId(long id)
        {
            return _reposFabricante.GetPorId(id);
        }

        public IList<Fabricante> Todos()
        {
            return _reposFabricante.Todos();
        }

        public IList<string> TodosNomes()
        {
            return _reposFabricante.TodosNomes();
        }

        #endregion

        #region Atributos privados
        private readonly IRepositorioFabricantes _reposFabricante;
        #endregion

        #region Overrides of ServicoPadraoEntidades<Fabricante>

        public override void PreSalvar(Fabricante t)
        {
            throw new System.NotImplementedException();
        }

        public override void PreAtualizar(Fabricante t)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}