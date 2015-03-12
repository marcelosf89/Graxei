using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao.Excecoes;
using Graxei.Negocio.Contrato.Factories;

namespace Graxei.Negocio.Implementacao.Factories
{
    public class EnderecosBuilder : IEnderecosBuilder
    {
        private long _id;

        private string _logradouro;

        private string _numero;

        private string _complemento;

        private Loja _loja;

        private Bairro _bairro;

        private string _telefones;

        private string _cnpj;
        

        private IServicoEnderecos _servicoEnderecos;

        private IServicoTiposTelefone _servicosTiposTelefone;

        public EnderecosBuilder(IServicoEnderecos servicoEnderecos, IServicoTiposTelefone servicoTiposTelefone)
        {
            _servicoEnderecos = servicoEnderecos;
            _servicosTiposTelefone = servicoTiposTelefone;
        }

        public IEnderecosBuilder SetId(long id)
        {
            _id = id;
            return this;
        }

        public IEnderecosBuilder SetLogradouro(string logradouro)
        {
            _logradouro = logradouro;
            return this;
        }

        public IEnderecosBuilder SetNumero(string numero)
        {
            _numero = numero;
            return this;
        }

        public IEnderecosBuilder SetComplemento(string complemento)
        {
            _complemento = complemento;
            return this;
        }

        public IEnderecosBuilder SetBairro(Bairro bairro)
        {
            if (!bairro.Validar())
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: bairro inválido");
            }
            _bairro = bairro;
            return this;
        }

        public IEnderecosBuilder SetLoja(Loja loja)
        {
            _loja = loja;
            return this;
        }

        public IEnderecosBuilder SetTelefones(string telefones)
        {
            _telefones = telefones;
            return this;
        }

        public IEnderecosBuilder SetCnpj(string cnpj)
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
                endereco = _servicoEnderecos.Get(_id);
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
            
            TipoTelefone tipoTelefone = _servicosTiposTelefone.Get("Comercial");
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
