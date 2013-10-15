﻿using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Fabrica;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
using Microsoft.Practices.Unity;
using System;
using TestesUnity.ConfiguracoesUnity;
using TestesUnity.Testes;

namespace TestesUnity
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            using (IUnityContainer container = new UnityContainer())
            {
                UnitOfWorkNHibernate.Instance.BindSession();
                BootstrapperAplicacao.RegisterTypes(container);
                IGerenciamentoProdutos gerenciamentoProdutos = container.Resolve<IGerenciamentoProdutos>();
                IServicoLojas servicoLojas = container.Resolve<IServicoLojas>();
                Categoria categoriaProduto = new Categoria() { Nome = "Pastilhas de Freio" };
                Fabricante fabricante = new Fabricante() { Nome = "OriginallParts" };
                string descricaoProduto = "Pastilha Freio Dianteiro Hyundai I30, Kia Carens, Cerato";
                UnidadeMedida entrada = new UnidadeMedida() { Sigla = "UN", Descricao = "Unidade"};
                Produto produto = new Produto()
                                      {
                                          Codigo = "103802-PP",
                                          Descricao = descricaoProduto,
                                          Fabricante = fabricante,
                                          Categoria = categoriaProduto
                                      };
                ProdutoVendedor pv = new ProdutoVendedor()
                                         {
                                             Descricao = descricaoProduto,
                                             UnidadeEntrada = entrada,
                                             UnidadeSaida = entrada,
                                             Produto = produto,
                                             Loja = servicoLojas.GetPorId(23)
                                         };

                try
                {
                    gerenciamentoProdutos.Salvar(pv);
                    Console.WriteLine("Salvou");
                }
                catch (OperacaoEntidadeException oe)
                {
                    Console.WriteLine("Ocorreu um erro: " + oe.Message);
                }
                UnitOfWorkNHibernate.Instance.UnBindSession();

                
                Console.ReadKey();
            }
        }

        /*
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            using (IUnityContainer container = new UnityContainer())
            {
                try
                {
                    ContainerGraxei.RegisterTypes(container);
                    IGerenciamentoLojas gerenciamentoLojas = container.Resolve<IGerenciamentoLojas>();
                    IConsultasUsuarios consultasUsuarios = container.Resolve<IConsultasUsuarios>();
                    IConsultasEnderecos consultasEnderecos = container.Resolve<IConsultasEnderecos>();
                    IServicoLojas servicoLojas = container.Resolve<IServicoLojas>();
                    Estado estado = consultasEnderecos.GetEstadoPorSigla("RJ");
                    Bairro bairro = consultasEnderecos.GetBairro("Centro", "Belford Roxo", estado.Id);
                    Endereco endereco = new Endereco() { Logradouro = "Rua ABCDEFGHIJ", Numero = "101010101", Bairro = bairro };
                    UnitOfWorkNHibernate.Instance.BindSession();
                    Loja loja = servicoLojas.Get("Mais Uma loja do Graxei");
                    loja.AdicionarEndereco(endereco);
                    gerenciamentoLojas.SalvarLoja(loja);
                    UnitOfWorkNHibernate.Instance.UnBindSession();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    Console.WriteLine("Rollbackou");
                }
                Console.ReadKey();
            }
        }*/
        /*
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            using (IUnityContainer container = new UnityContainer())
            {
                try
                {
                    ContainerGraxei.RegisterTypes(container);
                    IConsultasUsuarios consultasUsuarios = container.Resolve<IConsultasUsuarios>();
                    IConsultasEnderecos consultasEnderecos = container.Resolve<IConsultasEnderecos>();
                    IGerenciamentoLojas gerenciamentoLojas = container.Resolve<IGerenciamentoLojas>();
                    UnitOfWorkNHibernate.Instance.BindSession();
                    Usuario usuario = consultasUsuarios.GetPorLogin("admingraxei");
                    Loja loja = new Loja() {Nome = "Mais Uma loja do Graxei"};
                    Estado estado = consultasEnderecos.GetEstadoPorSigla("RJ");
                    Bairro bairro = consultasEnderecos.GetBairro("Centro", "Nova Iguaçu", estado.Id);
                    Endereco endereco = new Endereco()
                                            {Logradouro = "Rua NANANAAN", Numero = "989898", Bairro = bairro, Loja = loja};
                    endereco.Loja = loja;
                    IList<Endereco> enderecos = new List<Endereco>();
                    enderecos.Add(endereco);
                    loja.Enderecos = enderecos;
                    gerenciamentoLojas.SalvarLoja(loja, usuario);
                    UnitOfWorkNHibernate.Instance.UnBindSession();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    
                    Console.WriteLine("Rollbackou");
                }
                Console.ReadKey();
            }
        }
        */
        /*using (IUnityContainer container = new UnityContainer())
            {
                try
                {
                    ContainerGraxei.RegisterTypes(container);
                    UnitOfWorkNHibernate.Instance.GetCurrentSession().BeginTransaction();
                    IServicoEnderecos enderecos = container.Resolve<IServicoEnderecos>();
                    IServicoEstados estados = container.Resolve<IServicoEstados>();
                    IServicoLojas lojas = container.Resolve<IServicoLojas>();
                    Estado e = estados.GetPorSigla("RJ");
                    Cidade c = new Cidade() {Nome = "Janeiro de Rio", Estado = e};
                    //Cidade c = enderecos.GetCidade("Rio de Janeiro", e);
                    c.Nome = "Janeiro de Rio";
                    Bairro b = new Bairro() {Nome = "Centro", Cidade = c};
                    Endereco end = new Endereco() {Logradouro = "Rua a", Numero = "111", Bairro = b};
                    Loja loja = new Loja() {Nome = "Loja do Josué"};
                    end.Loja = loja;
                    //loja.Enderecos.Add(end);
                    lojas.Salvar(loja);
                    enderecos.Salvar(end);
                    UnitOfWorkNHibernate.Instance.GetCurrentSession().Transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    UnitOfWorkNHibernate.Instance.GetCurrentSession().Transaction.Rollback();
                    Console.WriteLine("Rollbackou");
                }
            }*/

        /*using (IUnityContainer container = new UnityContainer())
        {
            ContainerGraxei.RegisterTypes(container);
            UnitOfWorkNHibernate.GetCurrentSession().BeginTransaction();
            try
            {
            IServicoBairros bairros = container.Resolve<IServicoBairros>();
            IServicoCidades cidades = container.Resolve<IServicoCidades>();
            IServicoEstados estados = container.Resolve<IServicoEstados>();
            Estado estado = estados.GetPorSigla("RJ");
           // Cidade cidade = cidades.Get("Rio de Janeiro", estado);
            Bairro bairro = bairros.Get("Centro", "Janeiro de Rio", estado);
            bairro.Cidade.Nome = "Rio de Janeiro";
            bairros.Salvar(bairro);
            UnitOfWorkNHibernate.GetCurrentSession().Transaction.Commit();    
                                Console.WriteLine("Comitou");
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                UnitOfWorkNHibernate.GetCurrentSession().Transaction.Rollback();
                Console.WriteLine("Rollbackou");
            }
            Console.ReadKey();
            /*IServicoProdutos serv = container.Resolve<IServicoProdutos>();
            UnidadeMedida un1 = new UnidadeMedida();
            un1.Sigla = "UN"; un1.Descricao = "Unidade";
            UnidadeMedida un2 = new UnidadeMedida();
            un2.Sigla = "CX"; un2.Descricao = "Caixa";
            Categoria ct = new Categoria();
            ct.Nome = "Vela";
            Fabricante f = new Fabricante();
            f.Nome = "Bosch";
            Produto produto = new Produto();
            produto.Codigo = "AAA001"; produto.Descricao = "Jogo de Velas Fiat Punto";
            produto.UnidadeEntrada = un1; produto.UnidadeSaida = un2;
            produto.Categoria = ct; produto.Fabricante = f;
            serv.Salvar(produto);*/
        /*UnitOfWorkNHibernate.UnBindSession();

    }
    Console.WriteLine("Foi");
    Console.Read();
}*/
    }
}
