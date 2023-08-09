﻿using Application.Utilities;
using ProEventos.API.Controllers.Base;
using System.Text.Json;

namespace ProEventos.API.Configuration.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environmentHost;

        public MiddlewareException(RequestDelegate next, IWebHostEnvironment environmentHost)
        {
            _next = next;
            _environmentHost = environmentHost;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = new ResponseResultDTO<string>();

                response.Mensagens = new Notificacao[]
                {
                    new Notificacao(
                        EnumTipoNotificacao.ErroInterno,
                        $"Erro interno no servidor. {(_environmentHost.IsDevelopment()? ex.Message : "")}")
                };

                httpContext.Response.Headers.Add("content-type", "application/json; charset=utf-8");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
