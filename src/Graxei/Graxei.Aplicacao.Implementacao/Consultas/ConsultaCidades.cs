using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaCidades : IConsultaCidades
    {
        public ConsultaCidades(IServicoCidades servicoCidades)
        {
            _servicoCidades = servicoCidades;
        }

        public IList<Cidade> GetPorEstado(long idEstado)
        {
            return _servicoCidades.GetPorEstado(idEstado);
        }

        public Cidade Get(string nome, long idEstado)
        {
            return _servicoCidades.Get(nome, idEstado);
        }

        private IServicoCidades _servicoCidades;
    }
}
