using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graxei.Implementacao.UnitOfWork;
using Microsoft.Practices.Unity;
using FAST.Layers.UnitOfWork.Contrato;
using EDMManager.UnitOfWork.Implementation;

namespace Graxei.Implementacao.UnitOfWork
{
    public sealed class UnitOfWorkFactory
    {
        #region Singleton
        private static readonly UnitOfWorkFactory _instance = new UnitOfWorkFactory();

        IUnityContainer _container = new UnityContainer();
        private string _user;
        /// <summary>
        /// Instância do objeto singleton
        /// </summary>
        public static UnitOfWorkFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Instância do objeto singleton
        /// </summary>

        #endregion

        #region Fields

        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa o UnitOfWorkFactory
        /// </summary>
        private UnitOfWorkFactory()
        {
            _container.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWorkNHibernate));
        }


        public FAST.Layers.UnitOfWork.Contrato.IUnitOfWork UnitOfWorkNHibernate
        {
            get
            {
                return _container.Resolve<IUnitOfWork>();
            }
        }
        public FAST.Layers.UnitOfWork.Contrato.IUnitOfWork CreateUnitOfWorkNHibernate()
        {
            return _container.Resolve<IUnitOfWork>();
        }
        #endregion
    }
}
