using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaListaProdutosLoja : IConsultaListaProdutosLoja
    {
        private IServicoListaProdutosLoja _servicoListaProdutosLoja;
        public ConsultaListaProdutosLoja(IServicoListaProdutosLoja servicoListaProdutosLoja)
        {
            _servicoListaProdutosLoja = servicoListaProdutosLoja;
        }

        public ListaProdutosLoja Get(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina)
        {
            return _servicoListaProdutosLoja.Get(pesquisaProdutoContrato, tamanhoPagina);
        }
    }
}
