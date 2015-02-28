using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaFabricantes : IConsultaFabricantes
    {
        public ConsultaFabricantes(IServicoFabricantes servicoFabricantes)
        {
            ServicoFabricantes = servicoFabricantes;
        }

        public IServicoFabricantes ServicoFabricantes { get; private set; }

        public IList<string> TodosNomes()
        {
            return ServicoFabricantes.TodosNomes();
        }

    }
}