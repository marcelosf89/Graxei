using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoAtributos : IServicoEntidades<Atributo>
    {
        void PreSalvar(ProdutoVendedor produtoVendedor);
        void PreAtualizar(ProdutoVendedor produtoVendedor);
    }
}
