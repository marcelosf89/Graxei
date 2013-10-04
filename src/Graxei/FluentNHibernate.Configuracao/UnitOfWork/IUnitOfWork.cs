namespace Graxei.FluentNHibernate.UnitOfWork
{
    public interface IUnitOfWork
    {
        void IniciarTransacao();
        void ConfirmarTransacao();
        void DesfazerTransacao();
    }
}
