using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Application.Lib.Model.Infra
{
    /// <summary>
    /// The SMTP client setting 
    /// </summary>
    public class SmtpEmailProvider
    {
        /// <summary>
        /// Gets or sets the SMTP client host.
        /// </summary>
        /// <value>
        /// The SMTP client host.
        /// </value>
        public string SmtpClientHost { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public ushort Port { get; set; }
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}
