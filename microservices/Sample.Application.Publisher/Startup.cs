using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Sample.Application.Lib.Model.Infra;
using Sample.Application.Publisher.Infra.Contract;
using Sample.Application.Publisher.Infra.Service;

namespace Sample.Application.Publisher
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
            var queueSettingsSection = Configuration.GetSection("RabbitMQ:QueueSettings");
            var queueSettings = queueSettingsSection.Get<QueueSettings>();

            services.AddControllers();

            services.AddMassTransit(config => {

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    //cfg.Host("amqp://<username>:<password>@<hostname>:<port>/");
                    cfg.Host(queueSettings.HostName, queueSettings.Port, queueSettings.VirtualHost,
                     h => {
                         h.Username(queueSettings.UserName);
                         h.Password(queueSettings.Password);
                     });
                    cfg.ExchangeType = ExchangeType.Direct;
                });


                services.AddMassTransitHostedService();
            });

            services.AddTransient<IProducerService, ProducerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
