using System.Web.UI.WebControls;

namespace FAST.Utils
{
    /// <summary>
    /// Classe estática para bindar um datasource em um ListControl
    /// </summary>
    public static class ListControlBind
    {
        #region Public Static Methods
        /// <summary>
        /// Binda um datasource a um ListControl
        /// </summary>
        /// <param name="textField">Campo de texto do ListControl</param>
        /// <param name="valueField">Campo de valor do ListControl</param>
        /// <param name="listControl">Controle de lista</param>
        /// <param name="dataSource">DataSource para ser inserido no ListControl</param>
        public static void GenericBind(string textField, string valueField, ListControl listControl, object dataSource)
        {
            listControl.DataTextField = textField;
            listControl.DataValueField = valueField;
            listControl.DataSource = dataSource;
            listControl.DataBind();
        }

        /// <summary>
        /// Binda um datasource a um ListControl incluindo uma entrada no começo da lista
        /// </summary>
        /// <param name="textField">Campo de texto do ListControl</param>
        /// <param name="valueField">Campo de valor do ListControl</param>
        /// <param name="firstEntry">Texto da primeira entrada</param>
        /// <param name="firstValue">Valor da primeira entrada</param>
        /// <param name="listControl">Controle de lista</param>
        /// <param name="dataSource">DataSource para ser inserido no ListControl</param>
        public static void GenericBind(string textField, string valueField, string firstEntry, string firstValue, ListControl listControl, object dataSource)
        {
            listControl.AppendDataBoundItems = true;
            listControl.Items.Add(new ListItem(firstEntry, firstValue));
            GenericBind(textField, valueField, listControl, dataSource);
        }

        /// <summary>
        /// Binda um datasource a um ListControl incluindo uma entrada no começo da lista
        /// </summary>
        /// <param name="textField">Campo de texto do ListControl</param>
        /// <param name="valueField">Campo de valor do ListControl</param>
        /// <param name="firstEntry">Texto da primeira entrada</param>
        /// <param name="firstValue">Valor da primeira entrada</param>
        /// <param name="listControl">Controle de lista</param>
        /// <param name="dataSource">DataSource para ser inserido no ListControl</param>
        /// <param name="clear">Apaga todos os itens do ListControl antes de inserir</param>
        public static void GenericBind(string textField, string valueField, string firstEntry, string firstValue, ListControl listControl, object dataSource, bool clear)
        {
            if (clear)
            {
                listControl.Items.Clear();
            }
            GenericBind(textField, valueField, firstEntry, firstValue, listControl, dataSource);
        }
        #endregion
    }
}
