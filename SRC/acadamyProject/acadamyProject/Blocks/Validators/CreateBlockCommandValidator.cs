using FluentValidation;
using acadamyProject.Blocks.Commands;

namespace acadamyProject.Blocks.Validators;

public class CreateBlockCommandValidator : AbstractValidator<CreateBlockCommand>
{
	public CreateBlockCommandValidator()
	{
		RuleFor(x => x.Data)
			.NotEmpty().WithMessage("Данные блока не могут быть пустыми")
			.MinimumLength(3).WithMessage("Данные должны содержать минимум 3 символа");
	}
}