using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommomTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();


        _mock.Setup(passwordEncrypter =>
            passwordEncrypter.Encrypt(It.IsAny<String>())).Returns("!%dlfjkd545");
    }
    public IPasswordEncripter Build() => _mock.Object;

    public PasswordEncripterBuilder Verify(string? password)
    {
        if (!(string.IsNullOrWhiteSpace(password)))
        {
            _mock.Setup(passwordEncripter =>
            passwordEncripter.Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this;
    }
}
