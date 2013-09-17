using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using System.Collections.Generic;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoProdutos : ServicoPadraoEntidades<Produto>, IServicoProdutos
    {
        public ServicoProdutos(IRepositorioProdutos reposProdutos)
        {
            _reposProdutos = reposProdutos;
        }

        #region Atributos privados
        private readonly IRepositorioProdutos _reposProdutos;
        #endregion

    }
}