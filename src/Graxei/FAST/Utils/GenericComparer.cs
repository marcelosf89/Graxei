using System;
using System.Collections.Generic;

namespace FAST.Utils
{
    /// <summary>
    /// Enum com os tipos de direção de comparação
    /// </summary>
    public enum CompareDirection
    {
        /// <summary>
        /// Ordem ascendente
        /// </summary>
        Ascending,
        /// <summary>
        /// Ordem descendente
        /// </summary>
        Descending
    }

    /// <summary>
    /// Classe genérica para comparações de Collections
    /// </summary>
    /// <typeparam name="T">Tipo da collection</typeparam>
    public class GenericComparer<T> : IComparer<T>
    {
        #region Fields
        private string _sortExpression;
        private CompareDirection _sortDirection;
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor que inicializa a direção da comparação
        /// </summary>
        /// <param name="sortDirection">Direção da comparação (Ascending, Descending)</param>
        public GenericComparer(CompareDirection sortDirection)
        {
            this._sortDirection = sortDirection;
            this._sortExpression = string.Empty;
        }

        /// <summary>
        /// Construtor que inicializa a direção e a expressão da comparação
        /// </summary>
        /// <param name="sortDirection">Direção da comparação (Ascending, Descending)</param>
        /// <param name="sortExpression">Expressão de comparação (Propriedade da entidade)</param>
        public GenericComparer(CompareDirection sortDirection, string sortExpression)
            : this(sortDirection)
        {
            this._sortExpression = sortExpression;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Compara duas entidades
        /// </summary>
        /// <param name="x">Entidade para comparação</param>
        /// <param name="y">Entidade para comparação</param>
        /// <returns>-1 se for menor, 0 se for igual e 1 se for maior</returns>
        public int Compare(T x, T y)
        {
            Type type = typeof(T);
            object object1 = x;
            object object2 = y;
            foreach (string entity in this._sortExpression.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                object1 = type.GetProperty(entity).GetValue(object1, null);
                object2 = type.GetProperty(entity).GetValue(object2, null);
                type = object1.GetType();
            }
            IComparable comparable1 = (IComparable)object1;
            IComparable comparable2 = (IComparable)object2;
            int compareReturn = 0;
            switch (this._sortDirection)
            {
                case CompareDirection.Ascending:
                    compareReturn = comparable1.CompareTo(comparable2);
                    break;
                case CompareDirection.Descending:
                    compareReturn = comparable2.CompareTo(comparable1);
                    break;
            }
            return compareReturn;
        }

        /// <summary>
        /// Grava a expressão da comparação
        /// </summary>
        /// <param name="sortExpression">Expressão de comparação (Propriedade da entidade)</param>
        public void SetSortExpression(string sortExpression)
        {
            this._sortExpression = sortExpression;
        }
        #endregion
    }
}
