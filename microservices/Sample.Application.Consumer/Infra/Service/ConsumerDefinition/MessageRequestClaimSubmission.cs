using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.Options;
using Sample.Application.Consumer.Infra.Service.ConsumerService;
using Sample.Application.Lib.Model.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Application.Consumer.Infra.Service.ConsumerDefinition
{
    /// <summary>
    /// The Appointment Create ClaimSubmission
    /// </summary>
    /// <seealso cref="MassTransit.Definition.ConsumerDefinition{Sample.Application.Consumer.Infra.Service.ConsumerService.MessageRequestConsumerService}" />
    public class MessageRequestClaimSubmission : ConsumerDefinition<MessageRequestConsumerService>
    {
        /// <summary>
        /// Gets the claim submission.
        /// </summary>
        /// <value>
        /// The claim submission.
        /// </value>
        private ClaimSubmission claimSubmission { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRequestClaimSubmission"/> class.
        /// </summary>
        /// <param name="queueSetting">The queue setting.</param>
        public MessageRequestClaimSubmission(IOptions<QueueSettings> queueSetting)
        {
            claimSubmission = queueSetting.Value.ClaimSubmission;
        }

        /// <summary>
        /// Add ClaimSubmission
        /// </summary>
        /// <param name="endpointConfigurator">The receive endpoint configurator for the consumer</param>
        /// <param name="consumerConfigurator">The consumer configurator</param>
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<MessageRequestConsumerService> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => {
                // Ignore ArgumentNullException when re-try happen.
                r.Ignore<ArgumentNullException>();
                // Add retry with retry count and Interval
                r.Interval(claimSubmission.RetryCount, claimSubmission.Interval);
            });
        }
    }
}
