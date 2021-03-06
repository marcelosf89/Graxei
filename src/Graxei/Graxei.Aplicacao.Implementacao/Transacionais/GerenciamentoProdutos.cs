using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.Excecoes;
using System;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class GerenciamentoProdutos : PadraoTransacao, IGerenciamentoProdutos
    {
        public GerenciamentoProdutos(IServicoProdutoVendedor servicoProdutoVendedor)
        {
            Servico = servicoProdutoVendedor;
        }

        #region Implementação de IGerenciamentoProdutos
        public void Salvar(ProdutoVendedor produtoVendedor)
        {
            IniciarTransacao();
            try
            {
                Servico.Salvar(produtoVendedor);
                Confirmar();
            }
            catch (OperacaoEntidadeException oe)
            {
                Desfazer();
                throw;
            }
        }

        public void Excluir(ProdutoVendedor produtoVendedor)
        {
            IniciarTransacao();
            try
            {
                Servico.Excluir(produtoVendedor);
                Confirmar();
            }
            catch (OperacaoEntidadeException oe)
            {
                Desfazer();
                throw;
            }
        }

        public IList<ProdutoLojaPrecoContrato> SalvarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato)
        {
            IniciarTransacao();
            IList<ProdutoLojaPrecoContrato> resultado = new List<ProdutoLojaPrecoContrato>();
            try
            {
                if (produtoLojaPrecoContrato != null)
                {
                    resultado = Servico.AtualizarLista(produtoLojaPrecoContrato);
                    Confirmar();
                }
            }
            catch (Exception ex)
            {
                Desfazer();
                throw;
            }

            return resultado;
        }

        public IServicoProdutoVendedor Servico { get; private set; }

        #endregion
    }
}