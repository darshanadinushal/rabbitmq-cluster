using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sample.Application.Lib.Infra.Contract;
using Sample.Application.Lib.Model.Domain;
using System;
using System.Threading.Tasks;

namespace Sample.Application.Consumer.Infra.Service.ConsumerService
{
    /// <summary>
    /// The AppointmentCreate ConsumerService
    /// </summary>
    /// <seealso cref="MassTransit.IConsumer{Sample.Application.Lib.Model.Domain.EmailMessage}" />
    public class MessageRequestConsumerService : IConsumer<EmailMessage>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<MessageRequestConsumerService> _logger;
        /// <summary>
        /// The notification gateway
        /// </summary>
        private readonly INotificationGateway _notificationGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRequestConsumerService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="notificationGateway">The notification gateway.</param>
        public MessageRequestConsumerService(ILogger<MessageRequestConsumerService> logger, INotificationGateway notificationGateway)
        {
            _logger = logger;
            _notificationGateway = notificationGateway;
        }

        /// <summary>
        /// Consumes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Consume(ConsumeContext<EmailMessage> context)
        {
            try
            {
                _logger.LogInformation($"Start AppointmentCreateConsumerService Consume,request:" +
                    $"{JsonConvert.SerializeObject(context.Message)}");
                var result = await _notificationGateway.SendMail(context.Message);
                _logger.LogInformation($"End AppointmentCreateConsumerService Consume,response:{result}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

    }
}
