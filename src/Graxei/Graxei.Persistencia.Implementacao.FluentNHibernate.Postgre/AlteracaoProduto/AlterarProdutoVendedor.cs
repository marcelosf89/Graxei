using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public class AlterarProdutoVendedor : IMudancaProdutoVendedorFuncao
    {
        public AlterarProdutoVendedor(long idProdutoVendedor, string descricaoProdutoVendedor, double preco, Usuario usuario)
        {
            _idProdutoVendedor = idProdutoVendedor;
            _descricaoProdutoVendedor = descricaoProdutoVendedor;
            _preco = preco;
            _usuario = usuario;
        }

        public long IdProdutoVendedor
        {
            get { return _idProdutoVendedor; }
        }

        public string DescricaoProdutoVendedor
        {
            get { return _descricaoProdutoVendedor; }
        }

        public double Preco
        {
            get { return _preco; }
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

        private string _descricaoProdutoVendedor;

        private double _preco;

        private Usuario _usuario;

    }
}
