using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoAtributos : ServicoPadraoSomenteLeitura<Atributo>, IServicoAtributos
    {

        #region Construtor

        public ServicoAtributos(IRepositorioAtributos repositorioAtributos)
        {
            _repositorioEntidades = repositorioAtributos;
        }
        #endregion  

        #region Implementação de IServicoAtributos

        public void PreSalvar(ProdutoVendedor produtoVendedor)
        {
            IList<Atributo> atributos = Repositorio.Todos(produtoVendedor);
            ChecarRepeticoesSalvar(produtoVendedor, atributos);
        }

        private bool ChecarRepeticoesSalvar(ProdutoVendedor produtoVendedor, IList<Atributo> atributos)
        {
            IList<Atributo> atributosPassados = produtoVendedor.Atributos;
            int contador =(from a in atributosPassados
                           join b in atributos on a.Rotulo.Trim().ToLower() equals b.Rotulo.Trim().ToLower()
                         select a).Count();
            return (contador > 0);
        }

        public void PreAtualizar(ProdutoVendedor produtoVendedor)
        {
            IList<Atributo> atributos = Repositorio.Todos(produtoVendedor);
            ChecarRepeticoesAtualizar(produtoVendedor, atributos);
        }

        private bool ChecarRepeticoesAtualizar(ProdutoVendedor produtoVendedor, IList<Atributo> atributos)
        {
            IList<Atributo> atributosPassados = produtoVendedor.Atributos;
            int contador = (from a in atributosPassados
                            join b in atributos on a.Rotulo.Trim().ToLower() equals b.Rotulo.Trim().ToLower() 
                           where a.Id != b.Id
                          select a).Count();
            return (contador > 0);
        }
        #endregion

        #region Propriedades Privadas
        private IRepositorioAtributos Repositorio { get { return (IRepositorioAtributos) _repositorioEntidades;  } }
        #endregion
    }
}