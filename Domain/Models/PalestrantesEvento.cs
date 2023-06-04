using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PalestrantesEvento
    {
        public IEnumerable<Palestrante> Palestrante { get; set; }
        public IEnumerable<Evento> Evento { get; set; }

        public int PalestranteId { get; set; }
        public int EventoId { get; set; }
    }
}
