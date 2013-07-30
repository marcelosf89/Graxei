namespace FAST.Layers.UnitOfWork.Contrato
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
