using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Factories;
using Graxei.Transversais.Utilidades.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implementacao.Factories
{
    public class BairroBuilder : IBairroBuilder
    {
        public BairroBuilder(IServicoBairros servicoBairros, IServicoCidades servicoCidades, IServicoEstados servicoEstados)
        {
            _servicoBairros = servicoBairros;
            _servicoCidades = servicoCidades;
            _servicoEstados = servicoEstados;
        }

        public IBairroBuilder SetNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public IBairroBuilder SetCidade(string cidade)
        {
            _nomeCidade = cidade;
            return this;
        }

        public IBairroBuilder SetIdEstado(long id)
        {
            _idEstado = id;
            return this;
        }

        public Bairro Build()
        {
            Validar();
            Bairro retorno = _servicoBairros.Get(_nome, _nomeCidade, _idEstado);
            if (retorno != null)
            {
                return retorno;
            }

            retorno = new Bairro();
            Cidade cidade = _servicoCidades.Get(_nomeCidade, _idEstado);
            if (cidade == null)
            {
                Estado estado = _servicoEstados.GetPorId(_idEstado);
                if (estado == null)
                {
                    throw new ObjetoNaoEncontradoException(string.Format("Estado com identificador {0} não encontrado", _idEstado));
                }
                cidade = new Cidade();
                cidade.Nome = _nomeCidade;
                cidade.Estado = estado;
            }
            retorno.Nome = _nome;
            retorno.Cidade = cidade;
            return retorno;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nome))
            {
                throw new ObjetoConstrucaoException("Nome do bairro deve ser informado");
            }
            if (string.IsNullOrEmpty(_nomeCidade))
            {
                throw new ObjetoConstrucaoException("Nome da cidade deve ser informado");
            }
        }

        private string _nome;

        private string _nomeCidade;

        private long _idEstado;

        private readonly IServicoBairros _servicoBairros;

        private readonly IServicoCidades _servicoCidades;

        private readonly IServicoEstados _servicoEstados;
    }
}
