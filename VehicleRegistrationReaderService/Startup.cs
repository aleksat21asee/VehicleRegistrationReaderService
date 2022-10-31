using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using VehicleRegistrationReaderService.Models.Exceptions;
using VehicleRegistrationReaderService.MUP;

namespace VehicleRegistrationReaderService
{
    public class Startup
    {
        private ILogger<Startup> _logger { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            services.AddHealthChecks();

            services.AddSingleton<IVehicleRegistrationReaderWrapper, VehicleRegistrationReaderWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, ILogger<Startup> logger)
        {
            _logger = logger;
            app.UseCors(

            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            hostApplicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {
            var status = VehicleRegistrationAPI.sdStartup(1);
            if (status != VehicleRegistrationAPI.S_OK)
            {
                VehicleRegistrationReaderException exception = new VehicleRegistrationReaderException()
                {
                    Method = "sdStartup",
                    Status = status,
                    StatusMessage = VehicleRegistrationAPI.ResponseMessage(status),
                    DisplayMessage = VehicleRegistrationAPI.ResponseMessage(status)
                };

                _logger.LogError("Error on Vehicle Registration Card Reader startup", exception);

                throw exception;
            }

        }

        private void OnStopped()
        {
            var status = VehicleRegistrationAPI.sdSleanup();
            if (status != VehicleRegistrationAPI.S_OK)
            {
                VehicleRegistrationReaderException exception = new VehicleRegistrationReaderException()
                {
                    Method = "sdCleanup",
                    Status = status,
                    StatusMessage = VehicleRegistrationAPI.ResponseMessage(status),
                    DisplayMessage = VehicleRegistrationAPI.ResponseMessage(status)
                };

                _logger.LogError("Error on Vehicle Registration Card Reader cleanup", exception);

                throw exception;
            }
        }

    }
}
