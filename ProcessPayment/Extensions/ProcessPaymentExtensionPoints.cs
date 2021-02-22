using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProcessPayment.Extensions
{
    public static class ProcessPaymentExtensionPoints
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerClass>();
        }
    }

    public class ExceptionHandlerClass
    {
        private RequestDelegate _next;
        private ILogger log;
        public ExceptionHandlerClass(RequestDelegate next, ILogger log)
        {
            _next = next;
            this.log = log;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception es)
            {
                var trace = new StackTrace(es, true);
                var exceptionDetails = trace.GetFrames().FirstOrDefault();

                await HandleAsync(context, es);
            }
        }

        private Task HandleAsync(HttpContext ctx, Exception es)
        {
            log.LogError(es.Message);
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/json";



            return ctx.Response.WriteAsync(new { message = "failed", description = es.Message }.ToString());
        }
    }
}

