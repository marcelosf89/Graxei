using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Microsoft.Practices.Unity;
using System;
using TestesUnity.ConfiguracoesUnity;

namespace TestesUnity
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            using (IUnityContainer container = new UnityContainer())
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
            }
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
            Console.WriteLine("Foi");*/
            Console.Read();
        }
    }
}
