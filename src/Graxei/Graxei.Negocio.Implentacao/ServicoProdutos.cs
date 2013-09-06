using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implentacao
{
    public class ServicoProdutos : IServicoProdutos
    {

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

        #endregion

        #region Injeção de Dependência
        public IRepositorioProdutos RepositorioProdutos
        {
            set { _reposProdutos = value; }
        }
        #endregion

        #region Atributos privados
        private IRepositorioProdutos _reposProdutos;
        #endregion

    }
}
