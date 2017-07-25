using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intacct.Sdk
{
    public class RequestConfig
    {

        public string ControlId = "";

        public bool Transaction = false;

        public bool UniqueId = false;

        public string PolicyId = "";

        private Encoding encoding;

        public Encoding Encoding
        {
            get { return this.encoding; }
            set
            {
                // TODO validate encoding is supported
                this.encoding = value;
            }
        }

        private int maxRetries = 5;

        public int MaxRetries
        {
            get { return this.maxRetries; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Max Retries must be zero or greater");
                }
                this.maxRetries = value;
            }
        }

        public TimeSpan MaxTimeout = TimeSpan.FromSeconds(300);

        private int[] noRetryServerErrorCodes;

        public int[] NoRetryServerErrorCodes
        {
            get { return this.noRetryServerErrorCodes; }
            set
            {
                foreach (int errorCode in value)
                {
                    if (errorCode < 500 || errorCode > 599)
                    {
                        throw new ArgumentException("No Retry Server Error Codes must be between 500-599");
                    }
                }
                this.noRetryServerErrorCodes = value;
            }
        }

        public RequestConfig()
        {
            this.ControlId = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
            this.Encoding = Encoding.GetEncoding("UTF-8");
            this.NoRetryServerErrorCodes = new int[] { 524 };
        }
    }
}
