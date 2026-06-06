using Bogus;
using CashFlow.Communication.Requests;
namespace CommomTestUtilities.Requests;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {

        var request = new Faker<RequestLoginJson>()
            .RuleFor(r => r.Email, (faker) => faker.Internet.Email())
            .RuleFor(r => r.Password, (faker) => faker.Internet.Password(prefix: "!Aa1"));

        return request;
    }
}
