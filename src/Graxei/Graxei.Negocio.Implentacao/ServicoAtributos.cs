using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoAtributos : ServicoPadraoSomenteLeitura<Atributo>, IServicoAtributos
    {

        #region Construtor

        public ServicoAtributos(IRepositorioAtributos repositorioAtributos)
        {
            RepositorioEntidades = repositorioAtributos;
        }
        #endregion  

        #region Implementação de IServicoAtributos

        public void PreSalvar(ProdutoVendedor produtoVendedor)
        {
            IList<Atributo> atributos = Repositorio.Todos(produtoVendedor);
            if (ChecarRepeticoesSalvar(produtoVendedor, atributos))
            {
                throw new OperacaoEntidadeException(Erros.ProdutoAtributosRepetidos);
            }
        }

        private bool ChecarRepeticoesSalvar(ProdutoVendedor produtoVendedor, IList<Atributo> atributos)
        {
            IList<Atributo> atributosPassados = produtoVendedor.Atributos;
            if (atributosPassados == null || !atributosPassados.Any())
            {
                return false;
            }
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
        private IRepositorioAtributos Repositorio { get { return (IRepositorioAtributos) RepositorioEntidades;  } }
        #endregion
    }
}