using System;
using System.Collections.Generic;
using System.Text;
using FAST.Log;
using FAST.Layers.Model;
using System.Reflection;

namespace FAST.Utils
{
        /// <summary>
        /// Classe para montar a query de insert or update dinamica
        /// </summary>
    public sealed class Query
    {
        #region Fields

        //Tipo que sera criada a Query
        private TypeQuery type;

        //Dicionario de colunas por valor
        private IDictionary<string,object> chaveValor;

        //Condição.
        private string condition;

        //Sort.
        private string sort;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Query(TypeQuery type, IDictionary<string,object> dicionarioColunaObjeto)
        {
            this.type = type;
            this.chaveValor = dicionarioColunaObjeto;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Criar a query para Inset ou Update
        /// </summary>
        /// <returns></returns>
        public string CreateQuery()
        {
            string retorno =string.Empty;

            try
            {
                   retorno += AddValue();

            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Creating query", exception);
            }

            return retorno;
        }

        public void AddCondition(string condition)
        {
            this.condition = condition;
        }

        public void AddSort(string columnSort)
        {
            this.sort = columnSort;
        }

        #endregion

        #region Private Methods

        private string AddValue()
        {
            string value = string.Empty;
            string query = string.Empty;

            switch (this.type)
            {
                case TypeQuery.Insert:
                    query = GenerateColumnWithValueQuery();
                    break;
                case TypeQuery.Update:
                    if (string.IsNullOrEmpty(condition))
                        throw new Exception("ERROR:QUERY-001");
                    query = GenerateColumnWithValueQuery();
                    break;
                case TypeQuery.Select:
                    query = GenerateColumnWithValueQuery();
                    break;
                case TypeQuery.Delete:
                    if (string.IsNullOrEmpty(condition))
                        throw new Exception("ERROR:QUERY-001");

                    throw new Exception("ERROR:QUERY-001");
                    //query = string.Format("DELETE FROM {0} ", entity.GetType().Name);
                    //break;
            }

            if (!string.IsNullOrEmpty(condition))
            {
                query += string.Format("WHERE {0} ", condition);
            }

            if (!string.IsNullOrEmpty(this.sort))
            {
                query += string.Format("ORDER BY {0} ", this.sort);
            }

            return query;
        }

        private string GenerateColumnWithValueQuery()
        {
            string retorno = string.Empty;

            Dictionary<string, Dictionary<string, object>> j = new Dictionary<string, Dictionary<string, object>>();

            Dictionary<string, object> novo;
            foreach (string coluna in chaveValor.Keys)
            {
                string[] teste = coluna.Split('-');
                if (!j.ContainsKey(teste[0] +"."+ teste[1]))
                    j.Add(teste[0] + "." + teste[1], new Dictionary<string, object>());
                novo = j[(teste[0] + "." + teste[1])];

                novo.Add(teste[2], chaveValor[coluna]);
            }

            switch (type)
            {
                case TypeQuery.Insert:

                    foreach (string tabela in j.Keys)
                    {
                        string colunas = string.Empty;
                        string valores = string.Empty;

                        foreach (string coluna in j[tabela].Keys)
                        {
                            colunas += coluna + ",";
                            valores += ":" + coluna + ",";
                        }
                        colunas = colunas.Substring(colunas.Length - 2).Replace(",", "");
                        valores = valores.Substring(valores.Length - 2).Replace(",", "");

                        retorno = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", tabela, colunas, valores);
                    }
                    break;
                case TypeQuery.Update:
                    foreach (string tabela in j.Keys)
                    {
                        string valuesUpdade = string.Empty;

                        foreach (string coluna in j[tabela].Keys)
                        {
                            valuesUpdade += string.Format("{0} = :{0},", coluna);
                        }
                        valuesUpdade = valuesUpdade.Substring(valuesUpdade.Length - 2).Replace(",", "");

                        retorno = string.Format("UPDATE {0} SET {1};", tabela, valuesUpdade);
                    }
                    break;
                case TypeQuery.Select:
                  foreach (string tabela in j.Keys)
                    {
                        string valuesSelect = string.Empty;

                        foreach (string coluna in j[tabela].Keys)
                        {
                            valuesSelect += string.Format("{0},", coluna);
                        }
                        valuesSelect = valuesSelect.Substring(valuesSelect.Length - 2).Replace(",", "");

                        retorno = string.Format("SELECT {0} FROM {1};", valuesSelect,tabela);
                    }
                    break;
            }
            if (retorno.Substring(retorno.Length - 1).Trim().Equals(","))
                retorno = retorno.Substring(0, retorno.Length - 1);
            return retorno;
        }

        private string Value(object value , Type type)
        {
            string result = string.Empty;

            switch (type.Name)
            {
                case "string":
                case "String":
                    result = string.Format("'{0}'", value);
                    break;
                case "bool":
                case "Boolean":
                    result = bool.Parse(value.ToString()) ? "1" : "0";
                    break;
                default:
                    result = value.ToString();
                    break;
            }
            return result;
        }
        #endregion
    }

        public enum TypeQuery
        {
            Insert, Update, Delete, Select
        }
    }