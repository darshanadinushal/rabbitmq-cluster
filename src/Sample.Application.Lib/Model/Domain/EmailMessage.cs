using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Application.Lib.Model.Domain
{
    [Serializable]
    public class EmailMessage
    {
        public string CorrelationId { get; set; }

        public string Body { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string AppointmentNumber { get; set; }
    }
}
