using Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            string result = null;

            switch (exception)
            {
                case UnauthorizedException _:
                    code = HttpStatusCode.Unauthorized;
                    result = "";
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    result = "";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == null)
            {
                var errorMessage = exception.Message;

                if (_env.IsDevelopment())
                    errorMessage = exception.StackTrace;

                result = JsonConvert.SerializeObject(new { error = errorMessage });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
