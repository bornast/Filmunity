using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Middlewares;

namespace Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            return app.UseMiddleware<CustomExceptionHandlerMiddleware>(env);
        }
    }
}
