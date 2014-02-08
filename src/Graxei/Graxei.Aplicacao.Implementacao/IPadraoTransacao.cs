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
            UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
        }

        public void Confirmar()
        {
            UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
        }

        public void Desfazer()
        {
            UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
        }

        #endregion
    }
}
