using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace StoreApp.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        var statusCode = (int)HttpStatusCode.InternalServerError;
                        var message = contextFeature.Error.Message;

                        switch(contextFeature.Error)
                        {
                            case NotFoundException:
                                statusCode = (int)HttpStatusCode.NotFound;
                                break;
                            default:
                                break;
                        }

                        context.Response.StatusCode = statusCode;

                        var response = new
                        {
                            StatusCode = statusCode,
                            Message = message
                        };
                        var result = JsonSerializer.Serialize(response);
                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }
    }
}
