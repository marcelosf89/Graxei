using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;

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
            }
            retorno.Nome = contrato.Nome;
            retorno.Logotipo = contrato.Logotipo;
            return retorno;
        }

        public LojaContrato Transformar(Loja entidade)
        {
            LojaContrato retorno = new LojaContrato();
            if (entidade == null)
            {
                return retorno;
            }
            retorno.Id = entidade.Id;
            retorno.Nome = entidade.Nome;
            retorno.Logotipo = entidade.Logotipo;
            if (entidade.Enderecos != null)
            {
                foreach (Endereco endereco in entidade.Enderecos)
                {
                    EnderecoListaContrato enderecoListaContrato = new EnderecoListaContrato(endereco.Id, endereco.ToString());
                    retorno.AdicionarEndereco(enderecoListaContrato);
                }
            }
            return retorno;    
        }

        private IServicoLojas _servicoLojas;
    }
}
