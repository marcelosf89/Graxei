using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

namespace FAST.Utils
{
    [Obsolete("Para o FrameWork apartir do 3.5 se utiliza a classe PrincipalContext")]
    public sealed class ADAutenticate
    {
        private string _path;
        private string _filterAttribute;

        [Obsolete("Para o FrameWork apartir do 3.5 se utiliza a classe PrincipalContext")]
        public ADAutenticate(string path)
        {
            _path = path;
        }

        [Obsolete("Para o FrameWork apartir do 3.5 se utiliza a classe PrincipalContext")]
        public bool IsAuthenticated(string domain, string username, string pwd)
        {

            bool retorno = false;

            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {	//Bind to the native AdsObject to force authentication.			
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    //Update the new path to the user in the directory.
                    _path = result.Path;
                    _filterAttribute = (string)result.Properties["cn"][0];
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return retorno;
        }

        [Obsolete("Para o FrameWork apartir do 3.5 se utiliza a classe PrincipalContext")]
        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
    }
}
