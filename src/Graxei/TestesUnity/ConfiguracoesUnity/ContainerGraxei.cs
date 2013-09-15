using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao;
using Graxei.Persistencia.Implementacao.NHibernate;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesUnity.ConfiguracoesUnity
{
    public class ContainerGraxei
    {
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<IRepositorioProdutos, ProdutosNHibernateMySQL>()
                     .RegisterType<IServicoProdutos, ServicoProdutos>(
                            new InjectionFactory(p => new ServicoProdutos(container.Resolve<IRepositorioProdutos>())));
                     
        }
    }
}
