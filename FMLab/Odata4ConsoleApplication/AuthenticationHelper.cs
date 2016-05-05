using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using WindowsForms = System.Windows.Forms;
using Microsoft.IdentityModel.Tokens.Saml2;
using Microsoft.IdentityModel.Tokens.Saml11;
using System.IO;
using System.Xml;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Configuration;
using System.Diagnostics;
using ODataClient.Microsoft.Dynamics.DataEntities;
using System.Data.Services.Client;
using Microsoft.OData.Client;

namespace Odata4ConsoleApplication
{
    internal class AuthenticationHelper
    {
        //const string aosUrl = "https://usnconeboxax1aos.cloud.onebox.dynamics.com/";
        private static String cookies;

        internal static string PerformAuthentication(Resources context, string aosUrl)
        {

            AcsResponse param = new AcsResponse();
            BrowserForm browserForm = new BrowserForm(aosUrl, param);
            WindowsForms.Application.Run(browserForm);

            HttpWebRequest httpRequest;
            HttpWebResponse httpResponse;
            CookieContainer cookieContainer = new CookieContainer();

            httpRequest = (HttpWebRequest)WebRequest.Create(aosUrl);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            httpRequest.AllowAutoRedirect = true;
            httpRequest.CookieContainer = cookieContainer;

            StreamWriter writer = new StreamWriter(httpRequest.GetRequestStream(), Encoding.ASCII);
            writer.Write(param.GetAcsResponseString());
            writer.Flush();
            writer.Close();

            httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            httpResponse.Close();

            StringBuilder retval = new StringBuilder();

            CookieCollection cookiesCollection = cookieContainer.GetCookies(new Uri(aosUrl));

            if (cookiesCollection.Count > 0)
            {
                foreach (Cookie c in cookiesCollection)
                {
                    retval.AppendFormat("{0}={1}; ", c.Name, c.Value);
                }

                //Remove the trailing "; "
                retval.Remove(retval.Length - 2, 2);
            }

            cookies = retval.ToString();

            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(OnSendingRequest);

            return cookies;
        }

        public static void OnSendingRequest(object sender, SendingRequest2EventArgs e)
        {
            // Add the cookie to the cookie header to all requests 
            e.RequestMessage.SetHeader(HttpRequestHeader.Cookie.ToString(), cookies);
        }

    }
}

