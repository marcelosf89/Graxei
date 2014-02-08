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
            _servicoEnderecos = servicoEnderecos;
        }
        #endregion

        #region Implementação de IConsultasEnderecos

        public IList<Logradouro> GetLogradouros(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _servicoEnderecos.GetLogradouros(nomeBairro, nomeCidade, idEstado);
        }

        public Bairro GetBairro(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _servicoEnderecos.GetBairro(nomeBairro, nomeCidade, idEstado);
        }

        public IList<Bairro> GetBairros(string nomeCidade, long idEstado)
        {
            return _servicoEnderecos.GetBairros(nomeCidade, idEstado);
        }

        public IList<Cidade> GetCidades(long idEstado)
        {
            return _servicoEnderecos.GetCidades(idEstado);
        }

        public Estado GetEstadoPorSigla(string sigla)
        {
            return _servicoEnderecos.GetEstadoPorSigla(sigla);
        }

        public Estado GetEstado(long idEstado)
        {
            return _servicoEnderecos.GetEstado(idEstado);
        }

        public IList<Estado> GetEstados(EstadoOrdem ordem)
        {
            return _servicoEnderecos.GetEstados(ordem);
        }

        public IServicoEnderecos ServicoEnderecos { get; private set; }

        public IList<Endereco> EnderecosRepetidos(IList<Endereco> enderecos)
        {
            return _servicoEnderecos.EnderecosRepetidos(enderecos);
        }

        private IServicoEnderecos _servicoEnderecos;

        #endregion

    }
}