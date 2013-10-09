using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoProdutos :ITransacional
    {
        void Salvar(ProdutoVendedor produtoVendedor);
    }
}
