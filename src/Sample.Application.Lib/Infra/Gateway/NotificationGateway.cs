using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sample.Application.Lib.Infra.Contract;
using Sample.Application.Lib.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Lib.Infra.Gateway
{
    /// <summary>
    /// The Common NotificationGateway for send notification
    /// </summary>
    public class NotificationGateway : INotificationGateway
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<NotificationGateway> _logger;
        /// <summary>
        /// The email service
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationGateway"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailService">The email service.</param>
        public NotificationGateway(ILogger<NotificationGateway> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }


        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> SendMail(EmailMessage request)
        {
            try
            {
                _logger.LogInformation($"Start NotificationGateway,request:{JsonConvert.SerializeObject(request)}");

                return await _emailService.SendMail(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
