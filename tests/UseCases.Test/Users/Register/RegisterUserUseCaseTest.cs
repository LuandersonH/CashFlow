using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommomTestUtilities.Cryptography;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUsecase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUsecase();
        
        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.getErros().Count() == 1 && ex.getErros().Contains(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUsecase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.getErros().Count() == 1 && ex.getErros().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    private RegisterUserUseCase CreateUsecase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncrypter = new PasswordEncripterBuilder().Build();
        var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder();

        if (string.IsNullOrWhiteSpace(email) == false)
        { 
            readOnlyRepository.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(mapper, passwordEncrypter, readOnlyRepository.Build(), writeOnlyRepository, unitOfWork, jwtTokenGenerator);
    }
}
