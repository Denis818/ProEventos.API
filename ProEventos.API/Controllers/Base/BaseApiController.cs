using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers.Base
{

    public abstract class BaseApiController : Controller
    {
        protected INotificador Notificador { get; private set; }
        protected IMapper AutoMapper { get; private set; }
        public BaseApiController(IServiceProvider service)
        {
            Notificador = service.GetRequiredService<INotificador>();
            AutoMapper = service.GetRequiredService<IMapper>();
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
