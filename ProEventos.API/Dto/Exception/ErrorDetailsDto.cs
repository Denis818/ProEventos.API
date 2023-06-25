using System.Text.Json;

namespace ProEventos.API.Dto.Exception
{
    public class ErrorDetailsDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
