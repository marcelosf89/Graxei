using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaListaProdutosLoja : IConsultaListaProdutosLoja
    {
        private IServicoListaProdutosLoja _servicoListaProdutosLoja;
        public ConsultaListaProdutosLoja(IServicoListaProdutosLoja servicoListaProdutosLoja)
        {
            _servicoListaProdutosLoja = servicoListaProdutosLoja;
        }

        public ListaProdutosLoja Get(string criterio, long idLoja, int pagina, int tamanhoPagina, int totalElementos)
        {
            return _servicoListaProdutosLoja.Get(criterio, idLoja, pagina, tamanhoPagina, totalElementos);
        }
    }
}
