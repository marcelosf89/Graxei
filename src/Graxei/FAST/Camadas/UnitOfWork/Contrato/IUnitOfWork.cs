using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace FAST.Layers.UnitOfWork.Contrato
{
    /* TODO: resolver o que vai fazer com essa interface */
    public interface IUnitOfWork : IHttpModule, IDispatchMessageInspector, IServiceBehavior
    {

    }
}
