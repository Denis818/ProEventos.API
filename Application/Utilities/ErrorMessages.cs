namespace Application.Utilities
{
    public static class ErrorMessages
    {
        public const string UpdateError = "Ocorreu um erro ao atualizar o registro.";
        public const string InsertError = "Ocorreu um erro ao adicionar o registro.";
        public const string DeleteError = "Ocorreu um erro ao deletar o registro.";

        public const string NotFound = "Nenhum registro foi encontrado.";
        public const string IdNotFound = "Nenhum registro foi encontrado, ID(s):";
        public const string IdNotFoundOrDifferent = "O id do evento especificado não existe ou o ID do evento não corresponde ao ID fornecido.";     
    }
}
