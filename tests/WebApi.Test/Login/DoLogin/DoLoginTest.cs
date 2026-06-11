using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommomTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Login";
    private readonly string _email;
    private readonly string _name;
    private readonly string _password;

    private readonly HttpClient _httpClient;
    public DoLoginTest(CustomWebApplicationFactory customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
        _email = customWebApplicationFactory.GetEmail();
        _name = customWebApplicationFactory.GetName();
        _password = customWebApplicationFactory.GetPassword();
    }

    [Fact]
    public async Task Sucess()
    {
        var request = new RequestLoginJson
        { 
            Email = _email,
            Password = _password,
        };

        var response = await _httpClient.PostAsJsonAsync(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().Be(_name);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string cultureInfo)
    {
        var request = RequestLoginJsonBuilder.Build();        

        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo));

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_LOGIN", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

}
