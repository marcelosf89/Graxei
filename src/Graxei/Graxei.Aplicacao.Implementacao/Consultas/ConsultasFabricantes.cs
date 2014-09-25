using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasFabricantes : IConsultasFabricantes
    {
        #region Construtor
        public ConsultasFabricantes(IServicoFabricantes servicoFabricantes)
        {
            ServicoUsuarios = servicoFabricantes;
        }
        #endregion

        #region Implementação de IConsultasUsuarios
        public IServicoFabricantes ServicoUsuarios { get; private set; }
        public IList<string> TodosNomes()
        {
            return ServicoUsuarios.TodosNomes();
        }
        #endregion

    }
}