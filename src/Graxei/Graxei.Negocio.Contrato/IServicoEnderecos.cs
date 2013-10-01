using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEnderecos : IExcluirEntidade<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(long idLoja);
        IList<Estado> GetEstados(EstadoOrdem ordem);
        IList<Cidade> GetCidades(Estado estado);
        IList<Cidade> GetCidades(long idEstado);
        IList<Bairro> GetBairros(Cidade cidade);
        IList<Bairro> GetBairros(long idCidade);
        IList<Bairro> GetBairros(string nomeCidade, long idEstado);
        IList<Logradouro> GetLogradouros(Bairro bairro);
        IList<Logradouro> GetLogradouros(long idBairro);
        IList<Logradouro> GetLogradouros(string nomeBairro, string nomeCidade);
        Estado GetEstado(long idEstado);
        Estado GetEstadoPorSigla(string sigla);
        Estado GetEstadoPorNome(string nome);
        Cidade GetCidade(long idCidade);
        Cidade GetCidade(string nome, long idEstado);
        Cidade GetCidade(string nome, Estado estado);
        Bairro GetBairro(long idBairro);
        Bairro GetBairro(string nomeBairro, string nomeCidade, long idEstado);
        Bairro GetBairro(string nomeBairro, Cidade cidade);
        Bairro GetBairro(string nomeBairro, long idCidade);
        Logradouro GetLogradouro(long idLogradouro);
        Logradouro GetLogradouro(string nomeLogradouro, string nomeBairro, long idCidade);
        Logradouro GetLogradouro(string nomeLogradouro, Bairro bairro);
        Logradouro GetLogradouro(string nomeLogradouro, long idBairro);
    }
}
