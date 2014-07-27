using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasLogradouros : IConsultasLogradouros
    {
        public ConsultasLogradouros(IServicoLogradouros servicoLogradouros)
        {
            _servicoLogradouros = servicoLogradouros;
        }

        public IList<Logradouro> Get(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _servicoLogradouros.GetPorBairro(nomeBairro, nomeCidade, idEstado);
        }

        private IServicoLogradouros _servicoLogradouros;
    }
}