using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasEnderecos : IConsultasEnderecos
    {

        #region Construtor
        public ConsultasEnderecos(IServicoEnderecos servicoEnderecos)
        {
            ServicoEnderecos = servicoEnderecos;
        }
        #endregion

        #region Implementação de IConsultasEnderecos

        public IList<Logradouro> GetLogradouros(string nomeBairro, string nomeCidade, long idEstado)
        {
            return ServicoEnderecos.GetLogradouros(nomeBairro, nomeCidade, idEstado);
        }

        public Bairro GetBairro(string nomeBairro, string nomeCidade, long idEstado)
        {
            return ServicoEnderecos.GetBairro(nomeBairro, nomeCidade, idEstado);
        }

        public IList<Bairro> GetBairros(string nomeCidade, long idEstado)
        {
            return ServicoEnderecos.GetBairros(nomeCidade, idEstado);
        }

        public IList<Cidade> GetCidades(long idEstado)
        {
            return ServicoEnderecos.GetCidades(idEstado);
        }

        public Estado GetEstadoPorSigla(string sigla)
        {
            return ServicoEnderecos.GetEstadoPorSigla(sigla);
        }

        public Estado GetEstado(long idEstado)
        {
            return ServicoEnderecos.GetEstado(idEstado);
        }

        public IList<Estado> GetEstados(EstadoOrdem ordem)
        {
            return ServicoEnderecos.GetEstados(ordem);
        }

        public IServicoEnderecos ServicoEnderecos { get; private set; }
        #endregion

    }
}