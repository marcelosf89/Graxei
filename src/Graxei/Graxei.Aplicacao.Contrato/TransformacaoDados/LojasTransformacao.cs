using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Contrato.TransformacaoDados
{
    public class LojasTransformacao : ITransformacaoMutua<Loja, LojaContrato>
    {
        public LojasTransformacao(IServicoLojas servicoLojas)
        {
            _servicoLojas = servicoLojas;
        }

        public Loja Transformar(LojaContrato contrato)
        {
            Loja retorno = new Loja();
            if (contrato == null)
            {
                return retorno;
            }
            if (contrato.Id > 0)
            {
                retorno = _servicoLojas.GetPorId(contrato.Id);
                retorno.Nome = contrato.Nome;
                retorno.Logotipo = contrato.Logotipo;
            }
            return retorno;
        }

        public LojaContrato Transformar(Loja entidade)
        {
            LojaContrato retorno = new LojaContrato();
            if (entidade == null)
            {
                return retorno;
            }
            retorno.Nome = entidade.Nome;
            retorno.Logotipo = entidade.Logotipo;
            /*foreach (Endereco endereco in entidade.Enderecos)
            {
                EnderecoContrato enderecoContrato = new EnderecoContrato();
                enderecoContrato.Logradouro = endereco.Logradouro;
                enderecoContrato.Numero = endereco.Numero;
                enderecoContrato.Complemento = endereco.Complemento;
                enderecoContrato.Bairro = endereco.Bairro.Nome;
                enderecoContrato.Cidade = endereco.Bairro.Cidade.Nome;
                enderecoContrato.IdEstado = endereco.Bairro.Cidade.Estado.Id;
                retorno.AdicionarEnderecoContrato(enderecoContrato);
            }*/
            return retorno;
        }

        private IServicoLojas _servicoLojas;
    }
}
