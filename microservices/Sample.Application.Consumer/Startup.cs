using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Application.Lib.Model.Infra;
using Sample.Application.Consumer.Infra.Service.ConsumerService;
using Sample.Application.Consumer.Infra.Service.ConsumerDefinition;
using Sample.Application.Consumer.Infra.Filter;
using Sample.Application.Lib.Infra.Contract;
using Sample.Application.Lib.Infra.Gateway.Email;
using Sample.Application.Lib.Infra.Validate;
using Sample.Application.Lib.Infra.Gateway;
using MassTransit;
using GreenPipes;

namespace Sample.Application.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var queueSettings = Configuration.GetSection("RabbitMQ:QueueSettings").Get<QueueSettings>();
            services.Configure<QueueSettings>(options => Configuration.GetSection("RabbitMQ:QueueSettings").Bind(options));
            services.Configure<SmtpEmailProvider>(options => Configuration.GetSection("SmtpEmailProvider").Bind(options));

            services.AddControllers();

            services.AddMassTransit(config => {
                config.AddConsumer<MessageRequestConsumerService, MessageRequestClaimSubmission>(configurator => configurator.UseFilter(new MessageValidateFilter<MessageRequestConsumerService>()));
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(queueSettings.HostName, queueSettings.Port, queueSettings.VirtualHost,
                      h => {
                          h.Username(queueSettings.UserName);
                          h.Password(queueSettings.Password);
                      });
                    cfg.ReceiveEndpoint("Appointment-Create", c => {

                        c.ConfigureConsumer<MessageRequestConsumerService>(ctx);
                    });
                });
                services.AddMassTransitHostedService();
            });


            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IMessageValidate, MessageValidate>();
            services.AddScoped<INotificationGateway, NotificationGateway>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
        }
    }
}
