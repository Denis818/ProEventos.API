﻿using System.Text.Json.Serialization;
using Domain.Configurations;

namespace Domain.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Local { get; set; }

        [JsonConverter(typeof(CustomDateTimeFormat))]
        public DateTime DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        public string ImagemURL { get; set; }
    }
}
