using Sample.Application.Lib.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Publisher.Infra.Contract
{
    public interface IProducerService
    {
        Task Publish(EmailMessage request, CancellationToken cancellationToken);
    }
}
