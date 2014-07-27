using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasBairros : IConsultasBairros
    {
        private readonly IServicoBairros _servicoBairros;

        public ConsultasBairros(IServicoBairros servicoBairros)
        {
            _servicoBairros = servicoBairros;
        }
        public Bairro Get(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _servicoBairros.Get(nomeBairro, nomeBairro, idEstado);
        }

        public IList<Bairro> GetPorCidade(string nomeCidade, long idEstado)
        {
            return _servicoBairros.GetPorCidade(nomeCidade, idEstado);
        }

        public Bairro Get(long id)
        {
            return _servicoBairros.GetPorId(id);
        }
    }
}