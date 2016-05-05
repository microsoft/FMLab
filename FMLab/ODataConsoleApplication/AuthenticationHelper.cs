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
using ODataConsoleApplication.ServiceReference1;
using System.Data.Services.Client;

namespace ODataConsoleApplication
{
    internal class AuthenticationHelper
    {
        const string aosUrl = "https://usncax1aos.cloud.onebox.dynamics.com/";
        private static String cookies;

        internal static string PerformAuthentication(Resources context)
        {

            AcsResponse param = new AcsResponse();
            BrowserForm browserForm = new BrowserForm(aosUrl, param);
            WindowsForms.Application.Run(browserForm);
            // Debug.Assert(!browserForm.NavigationCanceled, "Unable to perform authentication to AOS");

            HttpWebRequest httpRequest;
            HttpWebResponse httpResponse;
            CookieContainer cookieContainer = new CookieContainer();

            httpRequest = (HttpWebRequest)WebRequest.Create(aosUrl);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            //
            // Authentication was done by using the hostname URI and not
            // full request URI. Hence, we should not let autoredirect
            // happen.
            //

            httpRequest.AllowAutoRedirect = false;
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

            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(OnSendingRequest);

            return cookies;
        }

        public static void OnSendingRequest(object sender, SendingRequestEventArgs e)
        {
            // Add the cookie to the cookie header to authenticate call to service 
            e.RequestHeaders.Add(HttpRequestHeader.Cookie, cookies);
        }

    }
}

