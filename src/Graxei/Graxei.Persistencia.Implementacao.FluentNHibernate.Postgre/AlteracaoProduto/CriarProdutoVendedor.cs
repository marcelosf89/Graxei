using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public class CriarProdutoVendedor : IMudancaProdutoVendedorFuncao
    {
        public CriarProdutoVendedor(long idProduto, string descricaoProdutoVendedor, double preco, long idEndereco, Usuario usuario)        
        {
            _idProduto = idProduto;
            _descricaoProdutoVendedor = descricaoProdutoVendedor;
            _preco = preco;
            _idEndereco = idEndereco;
            _usuario = usuario;
        }

        public long IdProduto
        {
            get { return _idProduto; }
        }

        public string DescricaoProdutoVendedor
        {
            get { return _descricaoProdutoVendedor; }
        }

        public long IdEndereco
        {
            get { return _idEndereco; }
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

        private long _idProduto;

        private string _descricaoProdutoVendedor;

        private long _idEndereco;

        private double _preco;

        private Usuario _usuario;
    }
}
