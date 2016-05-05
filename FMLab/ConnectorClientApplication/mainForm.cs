using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Configuration;

namespace ConnectorServiceApplication
{
    public partial class mainForm : Form
    {
        
        public mainForm()
        {
            InitializeComponent();

            txtBoxBaseUri.Text = ConfigurationManager.AppSettings["Rainier Uri"]; 
            txtBoxClientId.Text = ConfigurationManager.AppSettings["Azure Client Id"];
            txtBoxSecret.Text = ConfigurationManager.AppSettings[ "Azure Client Secret"];
            txtBoxTenant.Text = ConfigurationManager.AppSettings["Aad Tenant"];            
        }

        private void btnEnqueue_Click(object sender, EventArgs e)
        {
            if (richTextEnqueue.Text.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;

            //authenticate
            Helper helper = new Helper(txtBoxClientId.Text, txtBoxSecret.Text);

            //remove the unwanted curly braces
            txtBoxInActivity.Text = txtBoxInActivity.Text.Replace("{", "");
            txtBoxInActivity.Text = txtBoxInActivity.Text.Replace("}", "");

            //access the Connector API
            UriBuilder enqueueUri = new UriBuilder(txtBoxBaseUri.Text);
            enqueueUri.Path = "api/connector/Enqueue/" + txtBoxInActivity.Text;            

            helper.SendPostRequest(enqueueUri.Uri, helper.AuthenticationHeader, richTextEnqueue.Text, null);
            richTxtLog.AppendText ("Message Enqueued: " + enqueueUri.Uri + "\u2028");

            Cursor.Current = Cursors.Default;
        }

 
        private void btnDequeue_Click(object sender, EventArgs e)
        {
            if (txtBoxOutActivity.Text.Length == 0)
                return;

            Cursor.Current = Cursors.WaitCursor;

            //authenticate
            Helper helper = new Helper(txtBoxClientId.Text, txtBoxSecret.Text);
            
            //remove the unwanted curly braces
            txtBoxOutActivity.Text = txtBoxOutActivity.Text.Replace("{", "");
            txtBoxOutActivity.Text = txtBoxOutActivity.Text.Replace("}", "");

            //access the Connector API
            UriBuilder dequeueUri = new UriBuilder(txtBoxBaseUri.Text);
            dequeueUri.Path = "api/connector/dequeue/" + txtBoxOutActivity.Text;
            

            //send a request to get the next queue message
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, dequeueUri.Uri);
            request.Headers.Add("Authorization", helper.AuthenticationHeader);
            HttpResponseMessage response = helper.SendRequest(request);

            //read the response
            ActivityMessage responseMessage = response.Content.ReadAsAsync<ActivityMessage>().Result;
            if (responseMessage == null)
            {
                MessageBox.Show("No more messages in the queue");
                return;
            }           

            
            //read the downloadlocation and make a blob request
            request = new HttpRequestMessage(HttpMethod.Get, responseMessage.DownloadLocation.ToString());
            request.Headers.Add("Authorization", helper.AuthenticationHeader);

            //send a request to get the actual content from the blob storage
            HttpResponseMessage blobResponse = helper.SendRequest(request);
            richTxtDequeue.Text = blobResponse.Content.ReadAsStringAsync().Result;

            //ack the message
            UriBuilder ackUri= new UriBuilder(txtBoxBaseUri.Text);
            ackUri.Path = "api/connector/ack/" + txtBoxOutActivity.Text;
            helper.SendPostRequest(ackUri.Uri, helper.AuthenticationHeader, null, responseMessage);

            richTxtLog.AppendText ("Message dequeued and acked. MessageId: " + responseMessage.CorrelationId + "\u2028");

            Cursor.Current = Cursors.Default;
        }
    }
}
