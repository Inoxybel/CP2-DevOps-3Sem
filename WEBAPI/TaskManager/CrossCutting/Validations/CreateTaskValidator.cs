using FluentValidation;
using TaskManager.CrossCutting.DTO;

namespace TaskManager.CrossCutting.Validations
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskRequestDTO>
    {
        public CreateTaskValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("É obrigatório informar um título.")
                .Length(10, 100).WithMessage("O título deve ter no mínimo 10 caracteres e no máximo 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição não pode ser vazia.")
                .Length(10, 1024).WithMessage("Descrição deve ter no mínimo 10 caracteres e no máximo 1024 caracteres.");
        }
    }
}
