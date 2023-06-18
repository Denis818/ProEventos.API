using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers.Base
{
    public abstract class MainController : ControllerBase
    {
        protected INotificador _notificador { get; private set; }
        protected IMapper AutoMapper { get; private set; }

        public MainController(IServiceProvider service)
        {
            _notificador = service.GetRequiredService<INotificador>();
            AutoMapper = service.GetRequiredService<IMapper>();
        }

        protected IActionResult CustomResponse<TResponse>(TResponse contentResponse)
        {
            if (_notificador.ListNotificacoes.Count >= 1)
            {
                var erros = _notificador.ListNotificacoes.Where(item => item.Tipo == EnumTipoNotificacao.Error);
                var informacoes = _notificador.ListNotificacoes.Where(item => item.Tipo == EnumTipoNotificacao.Informacao);

                if (erros.Any())
                    return BadRequest(new ResponseResultDTO<TResponse>
                    {
                        Message = erros.ToArray()
                    });

                if (informacoes.Any())
                    return Ok(new ResponseResultDTO<TResponse>(contentResponse)
                    {
                        Message = informacoes.ToArray()
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
