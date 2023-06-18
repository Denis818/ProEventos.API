using Application.Utilities;

namespace ProEventos.API.Dto
{
    public class ResponseResultDTO<TResponse>
    {
        public TResponse Data { get; set; }
        public Notificacao[] Message { get; set; } = Array.Empty<Notificacao>();

        public ResponseResultDTO(TResponse data =  default)
        {
            Data = data;
        }
    }
}
