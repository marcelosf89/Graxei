using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEnderecos : IServicoEntidades<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(int idLoja);
        IList<Estado> GetEstados(EstadoOrdem ordem);
        IList<Cidade> GetCidades(Estado estado);
        IList<Cidade> GetCidades(int idEstado);
        IList<Bairro> GetBairros(Cidade cidade);
        IList<Bairro> GetBairros(int idCidade);
        IList<Bairro> GetBairros(string nomeCidade, int idEstado);
        Estado GetEstado(int idEstado);
        Estado GetEstadoPorSigla(string sigla);
        Estado GetEstadoPorNome(string nome);
        Cidade GetCidade(int idCidade);
        Cidade GetCidade(string nome, int idEstado);
        Cidade GetCidade(string nome, Estado estado);
        Bairro GetBairro(int idBairro);
        Bairro GetBairro(string nomeBairro, string nomeCidade, int idEstado);
        Bairro GetBairro(string nomeBairro, Cidade cidade);
        Bairro GetBairro(string nomeBairro, int idCidade);
    }
}
