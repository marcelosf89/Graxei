using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Implementacao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    
                    ContainerGraxei.RegisterTypes(container);
                    IServicoProdutos serv = container.Resolve<IServicoProdutos>();
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
                    serv.Salvar(produto);
                    UnitOfWorkNHibernate.UnBindSession();

            }
            Console.WriteLine("Foi");
            Console.Read();
        }
    }
}
