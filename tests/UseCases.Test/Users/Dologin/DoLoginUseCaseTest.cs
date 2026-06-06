using CashFlow.Application.UseCases.Login;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Entities;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Dologin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Sucess() 
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        var result = await useCase.Execute(request);                            

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();        

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result.Where(ex => ex.getErros().Count == 1 && ex.getErros().Contains(ResourceErrorMessages.INVALID_LOGIN));  
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        var user = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginException>();

        result.Where(ex => ex.getErros().Count == 1 && ex.getErros().Contains(ResourceErrorMessages.INVALID_LOGIN));
    }

    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {        
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();

        return new DoLoginUseCase(readRepository, passwordEncripter, tokenGenerator);
    }
}
