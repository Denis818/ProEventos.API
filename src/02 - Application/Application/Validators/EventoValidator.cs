using Domain.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class EventoValidator : AbstractValidator<EventoDto>
    {
        public EventoValidator()
        {
            RuleFor(x => x.Tema).NotEmpty().WithMessage("É obrigatório.")
                                .Length(3, 50).WithMessage("Deve ter entre 3 a 50 caracteres.");

            RuleFor(x => x.QtdPessoas).InclusiveBetween(1, 120000)
                                      .WithMessage("Não pode ser menor que 1 e maior que 120.000.");

            RuleFor(x => x.ImagemURL).Matches(@".*\.(gif|jpe?g|bmp|png)$")
                                     .WithMessage("Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png).");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("É obrigatório.")
                                    .Matches(@"^[0-9\-\(\)\s]+$").WithMessage("Está com número inválido.")
                                    .Length(8, 18).WithMessage("Permite somente de 8 a 18 caracteres.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("É obrigatório.")
                                 .EmailAddress().WithMessage("É necessário ser um Email válido.");
        }
    }
}
