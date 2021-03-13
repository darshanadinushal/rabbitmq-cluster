using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sample.Application.Lib.Model.Domain;
using Sample.Application.Publisher.Infra.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Publisher.Infra.Service
{
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IPublishEndpoint _endpoint;


        public ProducerService(ILogger<ProducerService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        public async Task Publish(EmailMessage request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start Publish,message:{JsonConvert.SerializeObject(request)}");

                await _endpoint.Publish<EmailMessage>(request, cancellationToken);

                _logger.LogInformation($"End Publish");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
