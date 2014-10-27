using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasBairros : IConsultasBairros
    {
        private readonly IServicoBairros _servicoBairros;
        private readonly IServicoCidades _servicoCidades;
        private readonly IServicoEstados _servicoEstados;

        public ConsultasBairros(IServicoBairros servicoBairros, IServicoCidades servicoCidades, IServicoEstados servicoEstados)
        {
            _servicoBairros = servicoBairros;
            _servicoCidades = servicoCidades;
            _servicoEstados = servicoEstados;
        }

        public Bairro Get(string nomeBairro, string nomeCidade, long idEstado)
        {
            Bairro retorno = _servicoBairros.Get(nomeBairro, nomeCidade, idEstado);
            if (retorno == null)
            {
                retorno = new Bairro();
                Cidade cidade = _servicoCidades.Get(nomeCidade, idEstado);
                if (cidade == null)
                {
                    Estado estado = _servicoEstados.GetPorId(idEstado);
                    cidade = new Cidade();
                    cidade.Nome = nomeCidade;
                    cidade.Estado = estado;
                }
                retorno.Nome = nomeBairro;
                retorno.Cidade = cidade;
            }
            return retorno;
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