using Domain.Models;

namespace Domain.Dtos
{
    public class PalestranteDto
    {
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestrantesEvento> PalestrantesEvento { get; set; }
    }
}
