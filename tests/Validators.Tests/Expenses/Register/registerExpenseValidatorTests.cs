using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class registerExpenseValidatorTests
{
    [Fact]
    public void Sucesso()
    {
        //arrange
        var validator = new RegisterExpenseValidator();
        var request = new RequestRegisterExpenseJson
        {
            Amount = 100,
            Date = DateTime.Now.AddDays(-1),
            Description = "description",
            Title = "title",
            PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
        };

        //act
        var result = validator.Validate(request);

        //result
        Assert.True(result.IsValid);
    }
}
