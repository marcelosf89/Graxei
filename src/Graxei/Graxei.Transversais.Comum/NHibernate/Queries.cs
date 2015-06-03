namespace Graxei.Transversais.Comum.NHibernate
{
    public sealed class Queries
    {
        /// <summary>
        /// Retorna o Trim().ToLower() da string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringPadrao(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return str.Trim().ToLower();
        }

        public static bool CompararStrings(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1)  && string.IsNullOrEmpty(str2))
            {
                return true;
            }
            if ((string.IsNullOrEmpty(str1)  && !string.IsNullOrEmpty(str2)) ||
                (!string.IsNullOrEmpty(str1)  && string.IsNullOrEmpty(str2)))
            {
                return false;
            }
            return str1.Trim().ToLower() == str2.Trim().ToLower();
        }
    }
}
