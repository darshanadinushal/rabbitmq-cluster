using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Application.Lib.Model.Infra
{
    /// <summary>
    /// ClaimSubmission for retry messages when consumers throw exceptions
    /// </summary>
    public class ClaimSubmission
    {
        /// <summary>
        /// Message retry count
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Message retry interval (ms)  
        /// </summary>
        public int Interval { get; set; }
    }
}
