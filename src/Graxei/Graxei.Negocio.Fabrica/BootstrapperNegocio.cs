using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Microsoft.Practices.Unity;
using System.Configuration;

namespace Graxei.Negocio.Fabrica
{
    public static class BootstrapperNegocio
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IServicosFabrica, ServicoFabricaUnity>(new ContainerControlledLifetimeManager());
            // Unidades de Medida
            container.RegisterType<IServicoUnidadeMedida, ServicoUnidadeMedida>();

            switch (_type)
            {
                //case "MYSQL":
                //   BootstrapperNegocioMySQL.Register(container);
                //    break;
                case "POSTGRESQL":
                default:
                   BootstrapperNegocioPostgre.Register(container);
                    break;
            }
        }

        private static string _type = ConfigurationManager.AppSettings["dbtype"];
    }
}