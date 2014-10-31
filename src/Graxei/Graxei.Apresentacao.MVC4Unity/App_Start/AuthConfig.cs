using DotNetOpenAuth.GoogleOAuth2;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.MVC4Unity
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            GoogleOAuth2Client client = new GoogleOAuth2Client(
                "329386834568-nfgtohevn3maqca3at0gvqqd17plipvt.apps.googleusercontent.com",
                "FsKU-7qwbJC1XTwsTG54xuym");
            Dictionary<string, object> extraData = new Dictionary<string, object>();
            OAuthWebSecurity.RegisterClient(client, "Google", extraData);

            //OAuthWebSecurity.RegisterGoogleClient("Graxei");
        }
    }
}