using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public class ExcluirProdutoVendedor : IMudancaProdutoVendedorFuncao
    {
        public ExcluirProdutoVendedor(long idProdutoVendedor, Usuario usuario)
        {
            _idProdutoVendedor = idProdutoVendedor;
            _usuario = usuario;
        }

        public long IdProdutoVendedor
        {
            get { return _idProdutoVendedor; }
        }

        public Usuario Usuario
        {
            get { return _usuario; }
        }

        public void Aceitar(IVisitorCriacaoFuncao visitor)
        {
            visitor.Visit(this);
        }

        private long _idProdutoVendedor;

        private Usuario _usuario;
    }
}
