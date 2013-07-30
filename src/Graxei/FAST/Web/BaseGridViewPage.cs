using System.Collections.Generic;
using System.Web.UI.WebControls;
using FAST.Utils;

namespace FAST.Web
{
    /// <summary>
    /// Classe básica para páginas que contenham GridView com Sort e Paging
    /// </summary>
    /// <typeparam name="T">Tipo da entidade que popula o GridView</typeparam>
    public class BaseGridViewPage<T> : BasePage
    {
        #region Constants
        private const string EXPRESSION = "BASEGRIDVIEWPAGESORTEXPRESSION";
        private const string DATASOURCE = "BASEGRIDVIEWPAGEDATASOURCE";
        #endregion

        #region Fields
        private GridView _gridView;
        #endregion

        #region Properties
        /// <summary>
        /// Recupera as entidades do GridView, em sessão
        /// </summary>
        protected ICollection<T> Entities
        {
            get { return this.Session[DATASOURCE] as ICollection<T>; }
        }

        /// <summary>
        /// Grava e recupera o GridView
        /// </summary>
        protected GridView GridView
        {
            get { return this._gridView; }
            set { this._gridView = value; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Grava o nome da propriedade que serve como chave primária
        /// </summary>
        /// <param name="keyName">Nome da propriedade</param>
        protected void SetDataKeyName(string keyName)
        {
            if (this.GridView != null)
            {
                this.GridView.DataKeyNames = new string[] { keyName };
            }
        }

        /// <summary>
        /// Grava o DataSource do GridView
        /// </summary>
        /// <param name="entities">Collection das entidades</param>
        protected void SetDataSource(ICollection<T> entities)
        {
            if (this.GridView != null)
            {
                this.Session[DATASOURCE] = entities;
                this.GridView.DataSource = entities;
                this.GridView.DataBind();
            }
        }

        /// <summary>
        /// Atualiza o DataSource com o que está em sessão
        /// </summary>
        protected void RefreshDataSource()
        {
            if (this.GridView != null)
            {
                this.GridView.DataSource = this.Entities;
                this.GridView.DataBind();
            }
        }
        #endregion

        #region GridView Events
        /// <summary>
        /// Evento de mudança de página do GridView
        /// </summary>
        /// <param name="sender">O objeto que gerou o evento</param>
        /// <param name="e">Argumento do evento</param>
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.PageIndex = e.NewPageIndex;
            this.RefreshDataSource();
        }

        /// <summary>
        /// Evento de classificação do GridView
        /// </summary>
        /// <param name="sender">O objeto que gerou o evento</param>
        /// <param name="e">Argumento do evento</param>
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            CompareDirection compareDirection = CompareDirection.Ascending;
            string expression = this.ViewState[EXPRESSION] != null ? this.ViewState[EXPRESSION].ToString() : string.Empty;
            if (expression == e.SortExpression)
            {
                compareDirection = CompareDirection.Descending;
                this.ViewState[EXPRESSION] = null;
            }
            else
            {
                this.ViewState[EXPRESSION] = e.SortExpression;
            }
            GridView gridView = sender as GridView;
            List<T> entities = this.Entities as List<T>;
            entities.Sort(new GenericComparer<T>(compareDirection, e.SortExpression));
            this.RefreshDataSource();
        }
        #endregion
    }
}
