using Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middlewares
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
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.ValidationErrors);
                    break;
                case UnauthorizedException _:
                    code = HttpStatusCode.Unauthorized;
                    result = "";
                    break;
                case ForbiddenException _:
                    code = HttpStatusCode.Forbidden;
                    result = "";
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    result = "";
                    break;
                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
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
