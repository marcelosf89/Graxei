using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Fabrica.Excecoes;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Aplicacao.Fabrica
{
    public class EnderecosBuilder
    {
        private long _id;

        private string _logradouro;

        private string _numero;

        private string _complemento;

        private Loja _loja;

        private Bairro _bairro;

        private string _telefones;

        private string _cnpj;
        

        private IConsultaEnderecos _consultasEnderecos;

        private IConsultasTiposTelefone _consultasTiposTelefone;

        public EnderecosBuilder(IConsultaEnderecos consultasEnderecos, IConsultasTiposTelefone consultasTiposTelefone)
        {
            _consultasEnderecos = consultasEnderecos;
            _consultasTiposTelefone = consultasTiposTelefone;
        }

        public EnderecosBuilder SetId(long id)
        {
            _id = id;
            return this;
        }

        public EnderecosBuilder SetLogradouro(string logradouro)
        {
            _logradouro = logradouro;
            return this;
        }

        public EnderecosBuilder SetNumero(string numero)
        {
            _numero = numero;
            return this;
        }

        public EnderecosBuilder SetComplemento(string complemento)
        {
            _complemento = complemento;
            return this;
        }

        public EnderecosBuilder SetBairro(Bairro bairro)
        {
            if (!bairro.Validar())
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: bairro inválido");
            }
            _bairro = bairro;
            return this;
        }

        public EnderecosBuilder SetLoja(Loja loja)
        {
            _loja = loja;
            return this;
        }

        public EnderecosBuilder SetTelefones(string telefones)
        {
            _telefones = telefones;
            return this;
        }

        public EnderecosBuilder SetCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public Endereco Build()
        {
            Validar();
            List<Telefone> telefones = GetTelefones(_telefones);
            Endereco endereco = CreateOrGet();

            endereco.Logradouro = _logradouro;
            endereco.Numero = _numero;
            endereco.Complemento = _complemento;
            endereco.Bairro = _bairro;
            endereco.Loja = _loja;
            endereco.Cnpj = _cnpj;
            if (telefones.Any())
            {
                endereco.SubstituirTelefones(telefones);    
            }
            return endereco;
        }

        private Endereco CreateOrGet()
        {
            Endereco endereco;
            if (_id > 0)
            {
                endereco = _consultasEnderecos.Get(_id);
                if (endereco == null)
                {
                    throw new ModeloDominioConstrucaoException(
                        "Não foi possível construir endereço: endereço não foi encontrado com idenficador");
                }
            }
            else
            {
                endereco = new Endereco();
            }
            return endereco;
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(_logradouro))
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: logradouro deve ser informado");
            }
            if (string.IsNullOrEmpty(_numero))
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: número deve ser informado");
            }
            if (_bairro == null)
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: bairro deve ser informado");
            }
            if (_loja == null)
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: loja deve ser informada");
            }
            if (_cnpj == null)
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: CNPJ deve ser informada");
            }
        }

        private List<Telefone> GetTelefones(string telefones)
        {
            List<Telefone> retorno = new List<Telefone>();
            if (string.IsNullOrEmpty(telefones))
            {
                return retorno; 
            }
            
            TipoTelefone tipoTelefone = _consultasTiposTelefone.Get("Comercial");
            if (tipoTelefone == null)
            {
                throw new ObjetoNaoEncontradoException("O tipo de telefone 'Comercial' não está cadastrado");
            }
            string[] listaTelefones = telefones.Split(',');
            for (int i = 0; i < listaTelefones.Length; i++)
            {
                Telefone telefone = new Telefone();
                if (string.IsNullOrEmpty(listaTelefones[i].Trim()))
                {
                    throw new GraxeiException("O telefone não pode ser vazio");
                }
                if (listaTelefones[i].Length > 20)
                {
                    throw new GraxeiException(String.Format("O telefone {0} é muito grande", listaTelefones[i]));
                }
                telefone.Numero = listaTelefones[i].Trim();
                telefone.TipoTelefone = tipoTelefone;
                retorno.Add(telefone);
            }
            return retorno;
        }
    }
}
