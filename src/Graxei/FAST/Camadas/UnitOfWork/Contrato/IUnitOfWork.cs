using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace FAST.Layers.UnitOfWork.Contrato
{
    public interface IUnitOfWork : IHttpModule, IDispatchMessageInspector, IServiceBehavior
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
