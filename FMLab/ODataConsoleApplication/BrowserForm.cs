using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ODataConsoleApplication
{
    public partial class BrowserForm : Form
    {
        public BrowserForm(String url, AcsResponse param)
        {
            SetUrl(url);
            SetAcsResponseParam(param);
            InitializeComponent();
        }

        private String GetUrl()
        {
            return m_url;
        }

        private void SetUrl(String url)
        {
            m_url = url;
        }

        private void SetAcsResponseParam(AcsResponse param)
        {
            m_acsResponseParam = param;
        }

        private void ReturnAcsResponse(String acsResponseString)
        {
            m_acsResponseParam.SetAcsResponseString(acsResponseString);
        }

        private String m_url;
        private AcsResponse m_acsResponseParam; // Ref to caller provided.

        private void BrowserForm_Load(object sender, EventArgs e)
        {
            this.Browser.Navigate(GetUrl());
        }

        private void WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                // Check if we are about to submit the SAML token returned by ACS.
                if (String.Compare(e.Url.AbsoluteUri, GetUrl(), true) == 0)
                {
                    // Extract ACS response.
                    if (this.Browser.Document.All["wresult"] != null)
                    {
                        // Cancel the request so that the POST doesn't actually happen.
                        e.Cancel = true;

                        try
                        {
                            String acsResponse;

                            //
                            // The ACS response that needs to be posted back has to be in a very
                            // specific format.
                            //

                            dynamic wa = this.Browser.Document.All["wa"].DomElement;
                            String waString = System.Web.HttpUtility.HtmlDecode(wa.value);
#if false
                        Console.WriteLine("wa={0}", waString);
#endif

                            dynamic wresult = this.Browser.Document.All["wresult"].DomElement;
                            String wresultString = System.Web.HttpUtility.HtmlDecode(wresult.value);
                            wresultString = wresultString.Replace("&", "&amp;");
#if false
                        Console.WriteLine("wresult={0}", wresultString);
#endif

                            dynamic wctx = this.Browser.Document.All["wctx"].DomElement;
                            String wctxString = System.Web.HttpUtility.HtmlDecode(wctx.value);
#if false
                        Console.WriteLine("wctx={0}", wctxString);
#endif

                            acsResponse = "wa=" + System.Web.HttpUtility.UrlEncode(waString, Encoding.ASCII) + "&" +
                                           "wresult=" + System.Web.HttpUtility.UrlEncode(wresultString, Encoding.ASCII) + "&" +
                                           "wctx=" + System.Web.HttpUtility.UrlEncode(wctxString, Encoding.ASCII);

#if false
                        Console.WriteLine("URL encoded ACS response:");
                        Console.WriteLine(acsResponse);
#endif

                            ReturnAcsResponse(acsResponse);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.ToString());
                        }

                        // Close the browser form.
                        this.Dispose();
                    }
                }
            }
            catch (Exception )
            {
                
            }
        }

        private void BrowserForm_Closing(object sender, CancelEventArgs e)
        {
            //this.navigationCanceled = true;
        }

    }

    public class AcsResponse
    {
        public AcsResponse()
        {
            m_acsResponseString = String.Empty;
        }

        public void SetAcsResponseString(String acsResponseString)
        {
            m_acsResponseString = acsResponseString;
        }

        public String GetAcsResponseString()
        {
            return m_acsResponseString;
        }

        private String m_acsResponseString;
    }

}