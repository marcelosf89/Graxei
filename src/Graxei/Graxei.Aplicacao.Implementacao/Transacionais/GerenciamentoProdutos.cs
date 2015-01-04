using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.Excecoes;
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

        public void SalvarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato)
        {
            IniciarTransacao();
            try
            {
                if (produtoLojaPrecoContrato != null)
                {
                    Servico.AtualizarLista(produtoLojaPrecoContrato);
                    Confirmar();
                }
            }
            catch (Exception ex)
            {
                Desfazer();
                throw;
            }
        }

        public IServicoProdutoVendedor Servico { get; private set; }

        #endregion
    }
}