using CashFlow.Application.UseCases.Expenses.Register;
using CommomTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Sucesso()
    {
        //arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        //act
        var result = validator.Validate(request);

        //result
        result.IsValid.Should().BeTrue();
    }
}
