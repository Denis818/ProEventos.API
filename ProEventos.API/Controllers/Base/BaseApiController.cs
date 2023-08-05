using Application.Interfaces.Utility;
using Application.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProEventos.API.Controllers.Base
{

    public abstract class BaseApiController : Controller
    {
        protected INotificador Notificador { get; private set; }
        public BaseApiController(IServiceProvider service)
        {
            Notificador = service.GetRequiredService<INotificador>();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is not ObjectResult result)
            {
                context.Result = CustomResponse<object>(null);
                return;
            }
            context.Result = CustomResponse(result.Value);
        }

        protected IActionResult CustomResponse<TResponse>(TResponse contentResponse)
        {
            if (Notificador.ListNotificacoes.Count >= 1)
            {
                var errosInternos = Notificador.ListNotificacoes.Where(item => item.Tipo == EnumTipoNotificacao.ErroInterno);
                if (errosInternos.Any())
                {
                    var result = new ResponseResultDTO<TResponse>(contentResponse) { Mensagens = errosInternos.ToArray() };
                    return new ObjectResult(result) { StatusCode = 500 };
                }

                var erros = Notificador.ListNotificacoes.Where(item => item.Tipo == EnumTipoNotificacao.Erro);
                if (erros.Any())
                {
                    var result = new ResponseResultDTO<TResponse>(default) { Mensagens = erros.ToArray() };
                    return BadRequest(result);
                }

                var informacoes = Notificador.ListNotificacoes.Where(item => item.Tipo == EnumTipoNotificacao.Informacao);
                if (informacoes.Any())
                    return Ok(new ResponseResultDTO<TResponse>(contentResponse) { Mensagens = informacoes.ToArray() });
            }

            return Ok(new ResponseResultDTO<TResponse>(contentResponse));

        }
    }

    public class ResponseResultDTO<TResponse>
    {
        public TResponse Data { get; set; }
        public Notificacao[] Mensagens { get; set; } = Array.Empty<Notificacao>();

        public ResponseResultDTO(TResponse data = default)
        {
            Data = data;
        }
    }
}
