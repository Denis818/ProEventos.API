using System.Text.Json.Serialization;

namespace Application.Utilities
{

    public class Notificacao
    {
        [JsonIgnore]
        public EnumTipoNotificacao Tipo { get; set; }
        public string Status => Tipo == EnumTipoNotificacao.Error ? "Erro" : "Informação";
        public string Mensagem { get; set; }


        public Notificacao(EnumTipoNotificacao tipo, string mensagem)
        {
            Tipo = tipo;
            Mensagem = mensagem;
        }
    }

    public enum EnumTipoNotificacao
    {
        Informacao,
        Error,
    }
}
