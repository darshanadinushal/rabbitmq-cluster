using Microsoft.Extensions.Logging;
using Sample.Application.Lib.Infra.Contract;
using Sample.Application.Lib.Model.Domain;
using System;
using System.Threading.Tasks;

namespace Sample.Application.Lib.Infra.Validate
{
    /// <summary>
    /// The AppointmentCreate Validate
    /// </summary>
    public class MessageValidate : IMessageValidate
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<MessageValidate> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageValidate"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public MessageValidate(ILogger<MessageValidate> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Validates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<bool> Validate(EmailMessage request)
        {
            try
            {
                _logger.LogInformation($"Start Validate email message : {request}");

                if (request != null && String.IsNullOrEmpty(request.To))
                {

                    if (!RegexUtilities.IsValidEmail(request.To))
                        return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
