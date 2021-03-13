using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sample.Application.Lib.Infra.Contract;
using Sample.Application.Lib.Model.Domain;
using Sample.Application.Lib.Model.Infra;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sample.Application.Lib.Infra.Gateway.Email
{
    /// <summary>
    /// The EmailService for send email
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<EmailService> _logger;

        /// <summary>
        /// Gets the SMTP email provider.
        /// </summary>
        /// <value>
        /// The SMTP email provider.
        /// </value>
        private SmtpEmailProvider _smtpEmailProvider { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="smtpEmailProvider">The SMTP email provider.</param>
        public EmailService(ILogger<EmailService> logger, IOptions<SmtpEmailProvider> smtpEmailProvider)
        {
            _logger = logger;
            _smtpEmailProvider = smtpEmailProvider.Value;
        }

        /// <summary>
        /// The Email Service
        /// </summary>
        /// <param name="Request">The message request.</param>
        /// <returns></returns>
        public async Task<bool> SendMail(EmailMessage request)
        {
            try
            {
                _logger.LogInformation($"Start SendMail,request:{JsonConvert.SerializeObject(request)}");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_smtpEmailProvider.SmtpClientHost);

                mail.From = new MailAddress(_smtpEmailProvider.From);
                mail.To.Add(request.To);
                mail.Subject = " Mr Darshana - :" + request.Subject;
                mail.Body = "This is sample body  :" + request.Body;

                SmtpServer.Port = _smtpEmailProvider.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_smtpEmailProvider.UserName, _smtpEmailProvider.Password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                _logger.LogInformation($"End SendMail statues: " + true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
