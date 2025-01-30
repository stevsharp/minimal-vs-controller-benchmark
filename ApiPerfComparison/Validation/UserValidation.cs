using FluentValidation;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(u => u.Age).NotEmpty().InclusiveBetween(18, 99).WithMessage("Age must be between 18 and 99.");
    }
}
