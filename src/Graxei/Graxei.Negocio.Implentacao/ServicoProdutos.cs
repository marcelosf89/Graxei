using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using System.Collections.Generic;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoProdutos : IServicoProdutos
    {
        public ServicoProdutos(IRepositorioProdutos reposProdutos)
        {
            _reposProdutos = reposProdutos;
        }

        #region Métodos sobrescritos
        public void Salvar(Produto produto)
        {
            _reposProdutos.Salvar(produto);
        }

        public void Excluir(Produto produto)
        {
            _reposProdutos.Excluir(produto);
        }

        public Produto GetPorId(long id)
        {
            return _reposProdutos.GetPorId(id);
        }

        public IList<Produto> Todos()
        {
            return _reposProdutos.Todos();
        }

        #endregion

        #region Atributos privados
        private readonly IRepositorioProdutos _reposProdutos;
        #endregion

    }
}