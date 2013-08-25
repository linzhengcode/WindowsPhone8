using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderDemo.Models
{
    public class StatusMessage
    {
        public string Status
        {
            get;
            private set;
        }

        public int TimeoutMilliseconds
        {
            get;
            private set;
        }

        public StatusMessage(
            string status,
            int timeoutMilliseconds)
        {
            Status = status;
            TimeoutMilliseconds = timeoutMilliseconds;
        }
    }
}
