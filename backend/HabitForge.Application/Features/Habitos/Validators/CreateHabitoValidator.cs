using FluentValidation;
using HabitForge.Application.Features.Habitos.DTOs;

namespace HabitForge.Application.Features.Habitos.Validators;

public class CreateHabitoValidator: AbstractValidator<CreateHabitoRequestDto> 
{
    public CreateHabitoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del hábito no puede estar vacío.")
            .MinimumLength(3).WithMessage("El hábito debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El hábito es demasiado largo.");
    }    
}