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
            _reposFabricante = reposFabricantes;
        }

        #region Métodos sobrescritos
        public void Salvar(Fabricante fabricante)
        {
            _reposFabricante.Salvar(fabricante);
        }

        public void Excluir(Fabricante fabricante)
        {
            _reposFabricante.Excluir(fabricante);
        }

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
    }
}