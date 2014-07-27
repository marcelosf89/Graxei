using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasEstados : IConsultasEstados
    {
        public ConsultasEstados(IServicoEstados servicoEstados)
        {
            _servicoEstados = servicoEstados;
        }

        public Estado Get(long id)
        {
            return _servicoEstados.GetPorId(id);
        }

        public IList<Estado> GetEstados(EstadoOrdem ordem)
        {
            return _servicoEstados.Todos(ordem);
        }

        private IServicoEstados _servicoEstados;
    }
}
