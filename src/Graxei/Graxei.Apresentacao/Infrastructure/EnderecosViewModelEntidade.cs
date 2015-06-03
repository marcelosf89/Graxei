using System.Linq;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.Areas.Administrativo.Models;
using Graxei.Modelo;
using Graxei.Transversais.Comum.TransformacaoDados.Interface;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Apresentacao.Infrastructure
{
    public class EnderecosViewModelEntidade : ITransformacaoMutua<Endereco, EnderecoVistaContrato>
    {
        public EnderecosViewModelEntidade(IOperacaoEndereco operacaoEndereco)
        {
            _operacaoEndereco = operacaoEndereco;
        }

        public Endereco Transformar(EnderecoVistaContrato contrato)
        {
            return _operacaoEndereco.GetComBaseEm(contrato);
        }

        public EnderecoVistaContrato Transformar(Endereco entidade)
        {
            EnderecoVistaContrato enderecoModel = new EnderecoVistaContrato();
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

        private readonly IOperacaoEndereco _operacaoEndereco;

    }
}