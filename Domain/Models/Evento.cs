using System.Text.Json.Serialization;
using Domain.Configurations;

namespace Domain.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
       // [JsonConverter(typeof(CustomDateTimeFormat))]
        public DateTime? DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Lote> Lote { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestrantesEvento> PalestrantesEventos { get; set; }
    }
}
