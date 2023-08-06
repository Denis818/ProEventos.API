namespace Application.Utilities
{
    public static class ErrorMessages
    {
        public const string UpdateError = "Ocorreu um erro ao atualizar o registro.";
        public const string InsertError = "Ocorreu um erro ao adicionar o registro.";
        public const string DeleteError = "Ocorreu um erro ao deletar um ou mais registros.";

        public const string NotFound = "Nenhum registro foi encontrado.";
        public const string NotFoundById = "Não foi encontrado um registro com o Id: ";
        public const string NotFoundByIds = "Não há registros correspondentes aos IDs: ";
        public const string NotFoundOrDifferentId = "O registro não foi encontrado, ou ID do registro não corresponde ao ID fornecido.";
    }
}
