using System.Linq;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;

namespace Graxei.Apresentacao.MVC4Unity.Infrastructure
{
    public class EnderecosViewModelEntidade : ITransformacaoMutua<Endereco, EnderecoModel>
    {
        private readonly IConsultasBairros _consultasBairros;

        private readonly IConsultasEnderecos _consultasEnderecos;

        private readonly IConsultasTiposTelefone _consultasTiposTelefone;

        public EnderecosViewModelEntidade(IConsultasBairros consultasBairros, IConsultasEnderecos consultasEnderecos, IConsultasTiposTelefone consultasTiposTelefone)
        {
            _consultasBairros = consultasBairros;
            _consultasEnderecos = consultasEnderecos;
            _consultasTiposTelefone = consultasTiposTelefone;
        }

        public Endereco Transformar(EnderecoModel contrato)
        {
            Bairro bairro = _consultasBairros.Get(contrato.Bairro, contrato.Cidade, contrato.IdEstado);
            EnderecosBuilder enderecosBuilder = new EnderecosBuilder(_consultasEnderecos, _consultasTiposTelefone);
            Endereco endereco = enderecosBuilder.SetLogradouro(contrato.Logradouro)
                                                .SetComplemento(contrato.Complemento)
                                                .SetNumero(contrato.Numero)
                                                .SetBairro(bairro).Build();
            return endereco;
        }

        public EnderecoModel Transformar(Endereco entidade)
        {
            EnderecoModel enderecoModel = new EnderecoModel();
            enderecoModel.Id = entidade.Id;
            enderecoModel.IdLoja = entidade.Loja.Id;
            enderecoModel.Bairro = entidade.Bairro.Nome;
            enderecoModel.Cidade = entidade.Bairro.Cidade.Nome;
            enderecoModel.Logradouro = entidade.Logradouro;
            enderecoModel.Numero = entidade.Numero;
            enderecoModel.Complemento = entidade.Complemento;
            enderecoModel.IdEstado = entidade.Bairro.Cidade.Estado.Id;
            enderecoModel.Cnpj = entidade.Cnpj;
            
            if (entidade.Telefones != null && entidade.Telefones.Any())
            {
                foreach (Telefone telefone in entidade.Telefones)
                {
                    enderecoModel.Telefones += telefone.Numero + ", ";
                }
                enderecoModel.Telefones = enderecoModel.Telefones.Substring(0, enderecoModel.Telefones.Length - 2);
            }
            return enderecoModel;
        }
    }
}