using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class EventDto
    {
        [DataType(DataType.Date)]
        public DateTime DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        public string ImagemURL { get; set; }
        public string Local { get; set; }
    }
}
