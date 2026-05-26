using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class InvalidLoginException : CashFlowException
{
    public InvalidLoginException() : base("Email e/ou senha inválidos.")
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> getErros()
    {
        return [Message];
    }
}
