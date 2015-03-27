using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Factories;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaBairros : IConsultasBairros
    {
        public ConsultaBairros(IServicoBairros servicoBairros, IBairrosBuilder bairroBuilder)
        {
            _servicoBairros = servicoBairros;
            _bairroBuilder = bairroBuilder;
        }

        public Bairro Get(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _bairroBuilder.SetNome(nomeBairro)
                                 .SetCidade(nomeCidade)
                                 .SetIdEstado(idEstado)
                                 .Build();
        }

        public IList<Bairro> GetPorCidade(string nomeCidade, long idEstado)
        {
            return _servicoBairros.GetPorCidade(nomeCidade, idEstado);
        }

        public Bairro Get(long id)
        {
            return _servicoBairros.GetPorId(id);
        }

        private readonly IServicoBairros _servicoBairros;
        private readonly IBairrosBuilder _bairroBuilder;

    }
}