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

        public EnderecosViewModelEntidade(IConsultasBairros consultasBairros)
        {
            _consultasBairros = consultasBairros;
        }

        public Endereco Transformar(EnderecoModel contrato)
        {
            Bairro bairro = _consultasBairros.Get(contrato.Bairro, contrato.Cidade, contrato.IdEstado);
            EnderecosBuilder enderecosBuilder = new EnderecosBuilder();
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
            return enderecoModel;
        }
    }
}