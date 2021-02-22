using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProcessPayment.Extensions
{
    public class ProcessPaymentExtensionPoints
    {
    }

    public class ExceptionHandlerClass
    {
        private RequestDelegate _next;

        public ExceptionHandlerClass(RequestDelegate next)
        {
            _next = next;

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
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/json";



            return ctx.Response.WriteAsync(new { message = "failed", description = es.Message }.ToString());
        }
    }
}

