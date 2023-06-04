using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PalestrantesEvento
    {
        public IEnumerable<Palestrante> Palestrantes { get; set; }
        public IEnumerable<Evento> Eventos { get; set; }

        public int PalestranteId { get; set; }
        public int EventoId { get; set; }
    }
}
