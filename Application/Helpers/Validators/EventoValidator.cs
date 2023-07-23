using Domain.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators
{
    public class EventoValidator : AbstractValidator<EventoDto>
    {
        public EventoValidator()
        {
            RuleFor(x => x.Tema).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                                .Length(3, 50).WithMessage("Intervalo permitido de 3 a 50 caracteres.");

            RuleFor(x => x.QtdPessoas).InclusiveBetween(1, 120000)
                                      .WithMessage("{PropertyName} não pode ser menor que 1 e maior que 120.000");

            RuleFor(x => x.ImagemURL).Matches(@".*\.(gif|jpe?g|bmp|png)$")
                                     .WithMessage("Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                                    .Matches(@"^[0-9\-\(\)\s]+$").WithMessage("O campo {PropertyName} está com número inválido")
                                    .Length(8, 18).WithMessage("O campo {PropertyName} permite somente de 8 a 18 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                                 .EmailAddress().WithMessage("É necessário ser um {PropertyName} válido");
        }
    }
}
