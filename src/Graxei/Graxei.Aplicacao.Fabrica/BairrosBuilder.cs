using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Fabrica.Excecoes;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Fabrica
{
    public class BairrosBuilder 
    {
        public BairrosBuilder(IConsultasBairros consultasBairros, IConsultaCidades consultasCidades,
            IConsultaEstados consultasEstados)
        {
            _consultasBairros = consultasBairros;
            _consultasCidades = consultasCidades;
            _consultasEstados = consultasEstados;
        }

        public BairrosBuilder SetBairro(string bairro)
        {
            if (string.IsNullOrEmpty(bairro))
            {
                throw new ArgumentException("Nome do bairro deve ser informado");
            }
            _nomeBairro = bairro;
            return this;
        }

        public BairrosBuilder SetCidade(string cidade)
        {
            if (string.IsNullOrEmpty(cidade))
            {
                throw new ArgumentException("Nome da cidade deve ser informado");
            }
            _nomeCidade = cidade;
            return this;
        }

        public BairrosBuilder SetEstado(long idEstado)
        {
            _idEstado = idEstado;
            return this;
        }

        public Bairro Build()
        {
            Bairro bairro = _consultasBairros.Get(_nomeBairro, _nomeCidade, _idEstado);
            if (bairro == null)
            {
                Cidade cidade = _consultasCidades.Get(_nomeCidade, _idEstado);
                if (cidade == null)
                {
                    Estado estado = _consultasEstados.Get(_idEstado);
                    if (estado == null)
                    {
                        throw new ModeloDominioConstrucaoException(string.Format("Estado com identificador {0} não pôde ser encontrado"));
                    }
                    cidade.Nome = _nomeCidade;
                    cidade.Estado = estado;
                }
                bairro.Nome = _nomeBairro;
                bairro.Cidade = cidade;
            }
            return bairro;
        }

        private IConsultasBairros _consultasBairros;
        private IConsultaCidades _consultasCidades;
        private IConsultaEstados _consultasEstados;
        private string _nomeBairro;
        private string _nomeCidade;
        private long _idEstado;
    }
}
