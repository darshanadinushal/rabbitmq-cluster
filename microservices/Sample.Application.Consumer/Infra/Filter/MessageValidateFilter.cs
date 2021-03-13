using GreenPipes;
using MassTransit;
using Newtonsoft.Json.Linq;
using Sample.Application.Lib.Infra.Validate;
using Sample.Application.Lib.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Application.Consumer.Infra.Filter
{
    /// <summary>
    /// The MessageValidateFilter
    /// </summary>
    /// <typeparam name="TConsumer"></typeparam>
    public class MessageValidateFilter<TConsumer> :
    IFilter<ConsumerConsumeContext<TConsumer>>
    where TConsumer : class
    {
        /// <summary>
        /// Add MassTransit build in filter 
        /// </summary>
        /// <param name="context"></param>
        public void Probe(ProbeContext context)
        {
        }

        /// <summary>
        /// The middleware filter 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task Send(ConsumerConsumeContext<TConsumer> context, IPipe<ConsumerConsumeContext<TConsumer>> next)
        {
            try
            {
                ConsumeContext<JToken> jsonContext;
                if (context.TryGetMessage(out jsonContext))
                {
                    var message = jsonContext.Message;
                    var messageRequest = message.ToObject<EmailMessage>();

                    if (!RegexUtilities.IsValidEmail(messageRequest.To))
                        throw new Exception("Exception occour :Email address is invalid");
                }
                await next.Send(context);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"An exception occurred: {ex.Message}");
                throw;
            }
        }
    }
}
