using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasEnderecos
    {
        IList<Logradouro> GetLogradouros(string nomeBairro, string nomeCidade, long idEstado);
        Bairro GetBairro(string nomeBairro, string nomeCidade, long idEstado);
        IList<Bairro> GetBairros(string nomeCidade, long idEstado);
        IList<Cidade> GetCidades(long idEstado);
        Estado GetEstadoPorSigla(string p);
        Estado GetEstado(long idEstado);
        IList<Estado> GetEstados(EstadoOrdem ordem);
        IServicoEnderecos ServicoEnderecos { get; }
        IList<Endereco> EnderecosRepetidos(IList<Endereco> enderecos);
    }
}
