﻿using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEnderecos : IRepositorioEntidades<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(int idLoja);
        /*IList<Estado> GetEstados();
        IList<Cidade> GetCidades(Estado estado);
        IList<Cidade> GetCidades(int idEstado);
        IList<Bairro> GetBairros(Cidade cidade);
        IList<Bairro> GetBairros(int idCidade);
        Estado GetEstado(int idEstado);
        Estado GetEstadoPorSigla(string sigla);
        Estado GetEstadoPorNome(string nome);
        Cidade GetCidade(int idCidade);
        Cidade GetCidade(string nome);
        Bairro GetBairro(int idBairro);
        Bairro GetBairro(string nome);*/
    }
}