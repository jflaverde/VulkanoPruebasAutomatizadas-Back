using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerWebsiteBDT
{
    class ParametersRequest
    {
        /// <summary>
        /// Get or set an api controller value
        /// </summary>
        public string ApiController { get; set; }

        /// <summary>
        /// Get or set a key of authentication on the api
        /// </summary>
        public string Key { get; set; }
    }
}
    