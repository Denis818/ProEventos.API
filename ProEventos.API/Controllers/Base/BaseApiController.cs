using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ProEventos.API.Controllers.Base
{

    public abstract class BaseApiController : Controller
    {
        protected INotificador Notificador { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        public string UserId { get; set; }
        public BaseApiController(IServiceProvider service)
        {
            Notificador = service.GetRequiredService<INotificador>();
            AutoMapper = service.GetRequiredService<IMapper>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            UserId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        protected IActionResult CustomResponse<TResponse>(TResponse contentResponse)
        {
            if (Notificador.ListNotificacoes.Any())
            {
                return Ok(new ResponseResultDTO<TResponse>(contentResponse)
                {
                    Message = Notificador.ListNotificacoes.ToArray()
                });
            }

            return Ok(new ResponseResultDTO<TResponse>(contentResponse));
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> ReadRequestBody()
        {
            Request.EnableBuffering();
            using var reader = new StreamReader(Request.Body, leaveOpen: true);

            string body = await reader.ReadToEndAsync();
            Request.Body.Position = 0;

            return body;
        }
    }

    public class ResponseResultDTO<TResponse>
    {
        public TResponse Data { get; set; }
        public Notificacao[] Message { get; set; } = Array.Empty<Notificacao>();

        public ResponseResultDTO(TResponse data = default)
        {
            Data = data;
        }
    }
}
