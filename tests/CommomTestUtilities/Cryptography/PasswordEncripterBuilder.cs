using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommomTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();


        mock.Setup(passwordEncripter =>
            passwordEncripter.Encrypt(It.IsAny<String>())).Returns("!%dlfjkd545");

        return mock.Object;
    }

    /*public static IPasswordEncripter Build_Verify()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(passwordEncripter =>
            passwordEncripter.Verify(It.IsAny<String>(), It.IsAny<String>())).Returns(true);

        return mock.Object;
    }*/
}
