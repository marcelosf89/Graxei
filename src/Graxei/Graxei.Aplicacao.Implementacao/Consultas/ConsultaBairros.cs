using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Factories;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaBairros : IConsultasBairros
    {
        private readonly IServicoBairros _servicoBairros;
        private readonly IServicoCidades _servicoCidades;
        private readonly IServicoEstados _servicoEstados;
        private readonly IBairrosBuilder _bairroBuilder;

        public ConsultaBairros(IServicoBairros servicoBairros, IServicoCidades servicoCidades, IServicoEstados servicoEstados, IBairrosBuilder bairroBuilder)
        {
            _servicoBairros = servicoBairros;
            _servicoCidades = servicoCidades;
            _servicoEstados = servicoEstados;
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
    }
}