using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Fabrica.Excecoes;
using Graxei.Modelo;

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

        private IConsultasEnderecos _consultasEnderecos;

        public EnderecosBuilder(IConsultasEnderecos consultasEnderecos)
        {
            _consultasEnderecos = consultasEnderecos;
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

        public Endereco Build()
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
            endereco.Logradouro = _logradouro;
            endereco.Numero = _numero;
            endereco.Complemento = _complemento;
            endereco.Bairro = _bairro;
            endereco.Loja = _loja;
            return endereco;
        }
    }
}
