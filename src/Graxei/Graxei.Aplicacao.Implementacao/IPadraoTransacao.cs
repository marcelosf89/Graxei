using System;
using Graxei.Aplicacao.Contrato;
using Graxei.FluentNHibernate.UnitOfWork;

namespace Graxei.Aplicacao.Implementacao
{
    public abstract class PadraoTransacao : ITransacional
    {
        #region Implementation of ITransacional

        public void IniciarTransacao()
        {
            UnitOfWorkNHibernate.Instance.IniciarTransacao();
        }

        public void Confirmar()
        {
            UnitOfWorkNHibernate.Instance.ConfirmarTransacao();
        }

        public void Desfazer()
        {
            UnitOfWorkNHibernate.Instance.DesfazerTransacao();
        }

        #endregion
    }
}
