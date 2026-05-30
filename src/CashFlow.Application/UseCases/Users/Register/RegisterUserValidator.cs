namespace CashFlow.Application.UseCases.Users.Register;

using CashFlow.Communication.Requests;
using FluentValidation;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Name not can be empty.");
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email not can be empty.")
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage("Email invalid");

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}
