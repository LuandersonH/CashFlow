using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Sucesso()
    {
        //arrange
        var validator = new RegisterExpenseValidator();
        var request = 

        //act
        var result = validator.Validate(request);

        //result
        Assert.True(result.IsValid);
    }
}
