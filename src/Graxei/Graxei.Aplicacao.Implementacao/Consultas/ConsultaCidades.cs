using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasCidades : IConsultasCidades
    {
        public ConsultasCidades(IServicoCidades servicoCidades)
        {
            _servicoCidades = servicoCidades;
        }

        public IList<Cidade> GetPorEstado(long idEstado)
        {
            return _servicoCidades.GetPorEstado(idEstado);
        }

        private IServicoCidades _servicoCidades;
    }
}
