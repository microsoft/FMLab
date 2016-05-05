using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorServiceApplication
{
    public class ActivityMessage
    {
        /// <summary>
        /// Correlation-Id.
        /// </summary>
        
        public string CorrelationId { get; set; }

        /// <summary>
        /// Pop-Receipt.
        /// </summary>
      
        public string PopReceipt { get; set; }

        /// <summary>
        /// The content, if the content is completely contained within the queue message.
        /// </summary>
      
        public string Content { get; set; }

        /// <summary>
        /// The location to download, if the content is outside of the queue message.
        /// </summary>
       
        public Uri DownloadLocation { get; set; }
    }
}
