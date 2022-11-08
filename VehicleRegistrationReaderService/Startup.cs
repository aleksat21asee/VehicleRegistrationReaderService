using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using VehicleRegistrationReaderService.CustomExceptionMiddleware;
using VehicleRegistrationReaderService.Exceptions;
using VehicleRegistrationReaderService.MUP;
using AutoMapper;
using System.Reflection;

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

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ExceptionMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, ILogger<Startup> logger)
        {
            _logger = logger;

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader()
            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

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
                throw new WrapperException("sdStartup", new ServiceException(VehicleRegistrationAPI.ResponseMessage(status)));
            }
        }

        private void OnStopped()
        {
            var status = VehicleRegistrationAPI.sdCleanup();
            if (status != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdCleanup", new ServiceException(VehicleRegistrationAPI.ResponseMessage(status)));
            }
        }

    }
}
