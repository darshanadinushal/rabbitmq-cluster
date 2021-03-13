using Sample.Application.Lib.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Lib.Infra.Contract
{
    /// <summary>
    /// The EmailService for send email 
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<bool> SendMail(EmailMessage request);
    }
}
