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
            } if (string.IsNullOrEmpty(_numero))
            {
                throw new ModeloDominioConstrucaoException("Não foi possível construir endereço: número deve ser informado");
            }
            Endereco endereco = new Endereco();
            endereco.Logradouro = _logradouro;
            endereco.Numero = _numero;
            endereco.Complemento = _complemento;
            endereco.Bairro = _bairro;
            endereco.Loja = _loja;
            return endereco;
        }
    }
}
