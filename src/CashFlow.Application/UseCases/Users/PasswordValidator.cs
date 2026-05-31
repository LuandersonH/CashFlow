using FluentValidation;
using FluentValidation.Validators;
using CashFlow.Exception;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";


    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {

        if (string.IsNullOrWhiteSpace(password))
        {
            AddErrorMessage(context);
            return false;
        }

        if (password.Length < 8)
        {
            AddErrorMessage(context);
            return false;
        }

        if (UpperCaseLetter().IsMatch(password) == false)
        {
            AddErrorMessage(context);
            return false;
        }

        if (LowerCaseLetter().IsMatch(password) == false)
        {
            AddErrorMessage(context);
            return false;
        }

        if (Numbers().IsMatch(password) == false)
        {
            AddErrorMessage(context);
            return false;
        }

        if (SpecialSymbols().IsMatch(password) == false)
        {
            AddErrorMessage(context);
            return false;
        }

        return true;
    }

    private static void AddErrorMessage(ValidationContext<T> context)
    {
        context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseLetter();
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex Numbers();
    [GeneratedRegex(@"[\!\?\*\.]+")]
    private static partial Regex SpecialSymbols();
}
