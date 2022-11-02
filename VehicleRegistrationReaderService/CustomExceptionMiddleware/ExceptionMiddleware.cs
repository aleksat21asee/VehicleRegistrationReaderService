using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using VehicleRegistrationReaderService.Exceptions;
using VehicleRegistrationReaderService.Models;

namespace VehicleRegistrationReaderService.CustomExceptionMiddleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) => _logger = logger;
        
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(httpContext, e);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = exception.InnerException switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ServiceException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Method = exception.InnerException != null ? exception.Message : "unknown",
                DisplayMessage = exception.InnerException?.Message ?? "Something went wrong"
            }));
        }
    }
}
