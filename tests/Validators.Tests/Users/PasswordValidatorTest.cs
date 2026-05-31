using CashFlow.Application.UseCases.Users;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Communication.Requests;
using CommomTestUtilities.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    [InlineData(null)]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("AAAAAAAa")]
    [InlineData("AAAAAAa1")]
    public void Error_Password_Empty(string password)
    {
        //Arrange
        var validator = new PasswordValidator<RequestRegisterUserJson>();
        
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = password;

        //Act
        var result = validator.
            IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        //Assert
        result.Should().BeFalse();        
    }
}
