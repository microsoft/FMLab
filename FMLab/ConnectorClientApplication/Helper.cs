using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http.Formatting;
using System.Configuration;

namespace ConnectorServiceApplication
{
    class Helper
    {
        string authenticationHeader = "";
        public string AuthenticationHeader
        {
            get
            {
                return authenticationHeader;
            }            
        }


        public Helper(string clientId, string clientSecret)
        {
            //authentication string details
            UriBuilder uri = new UriBuilder(ConfigurationManager.AppSettings["Azure auth Uri"]);
            uri.Path = ConfigurationManager.AppSettings["Aad Tenant"];            

            AuthenticationContext authenticationContext = new AuthenticationContext(uri.ToString());
            ClientCredential clientCred = new ClientCredential(clientId, clientSecret);
            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(ConfigurationManager.AppSettings["Rainier Service Prinicipal"], clientCred);

            authenticationHeader = authenticationResult.CreateAuthorizationHeader();
        }


        public HttpResponseMessage SendPostRequest(Uri uri, string authenticationHeader, string body, ActivityMessage message)
        {
           
            using (HttpClientHandler handler = new HttpClientHandler() { UseCookies = false })
            {
                using (HttpClient webClient = new HttpClient(handler))
                {
                    webClient.DefaultRequestHeaders.Authorization = ParseAuthenticationHeader(authenticationHeader);

                    HttpResponseMessage response;

                    if (body != null)
                    {
                        //convert string into memorystream to be passed in the post request
                        byte[] bytes = Encoding.ASCII.GetBytes(body);
                        MemoryStream memStream = new MemoryStream(bytes);

                        using (StreamContent content = new StreamContent(memStream))
                        {
                            response = webClient.PostAsync(uri, content).Result;
                        }
                    }
                    else
                    {
                        response = webClient.PostAsJsonAsync<ActivityMessage>(uri.ToString(), message).Result;
                    }

                    return response;
                }
            }
        }

        public HttpResponseMessage SendRequest(HttpRequestMessage request)
        {
            using (HttpClientHandler handler = new HttpClientHandler() { UseCookies = false })
            {
                using (HttpClient webClient = new HttpClient(handler))
                {
                    HttpResponseMessage response = webClient.SendAsync(request).Result;
                    return response;
                }
            }
        }



        private static AuthenticationHeaderValue ParseAuthenticationHeader(string authenticationHeader)
        {
            string[] split = authenticationHeader.Split(' ');
            string scheme = split[0];
            string parameter = split[1];
            return new AuthenticationHeaderValue(scheme, parameter);
        }


    }
}
