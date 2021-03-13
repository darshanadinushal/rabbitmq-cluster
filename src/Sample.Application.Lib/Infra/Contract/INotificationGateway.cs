using Sample.Application.Lib.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Lib.Infra.Contract
{
    /// <summary>
    /// The Common NotificationGateway for send notification
    /// </summary>
    public interface INotificationGateway
    {
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<bool> SendMail(EmailMessage request);
    }
}
