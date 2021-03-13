using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Application.Lib.Model.Domain;
using Sample.Application.Publisher.Infra.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IProducerService _producerService;
        private readonly ILogger<PublisherController> _logger;

        public PublisherController(IProducerService producerService, ILogger<PublisherController> logger)
        {
            _producerService = producerService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "MassTransit", "Producer" };
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmailMessage request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start Publisher");
                await _producerService.Publish(request, cancellationToken);
                _logger.LogInformation("End Publisher: Send to the Queue successfully ");
                return Ok("Send to the Queue successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Error : ex >>>" + ex.Message.ToString());
            }
        }
    }
}
