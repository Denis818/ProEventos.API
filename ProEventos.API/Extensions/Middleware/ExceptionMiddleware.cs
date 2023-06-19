﻿using ProEventos.API.Dto.Exception;
using System.Net;

namespace Application.Configurations.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetailsDto
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = $"Error: {exception.Message}",

            }.ToString());
        }
    }
}