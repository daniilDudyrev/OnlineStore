using Bogus;
using FluentAssertions;
using OnlineStore.HttpApiClient;
using OnlineStore.Models.Requests;
using Xunit.Abstractions;

namespace OnlineStore.WebApi.IntegrationTests;

public class AccountEndpointsTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly Faker _faker = new("ru");

    public AccountEndpointsTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Register_user_succeeded()
    {
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.UserName,
            Password = _faker.Internet.Password()
        };
        var registerResponse = await client.Register(registerRequest);
        registerResponse.Email.Should().Be(registerRequest.Email);
        registerResponse.AccountId.Should().NotBeEmpty();
    }

    [Fact]
    public async void Register_user_with_occupied_email_gives_error()
    {
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.UserName,
            Password = _faker.Internet.Password()
        };
        await client.Register(registerRequest);
        await FluentActions.Invoking(() => client.Register(registerRequest))
            .Should()
            .ThrowAsync<HttpRequestException>()
            .WithMessage("*Email already exists!*");
    }

    [Fact]
    public async void Login_user_succeeded()
    {
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.UserName,
            Password = _faker.Internet.Password()
        };
        await client.Register(registerRequest);
        var authRequest = new AuthRequest()
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password
        };
        var authResponse = await client.Authentication(authRequest);

        authResponse.Email.Should().Be(authRequest.Email);
    }

    [Fact]
    public async void Login_user_with_invalid_password_gives_error()
    {
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.UserName,
            Password = _faker.Internet.Password()
        };
        await client.Register(registerRequest);
        var authRequest = new AuthRequest()
        {
            Email = registerRequest.Email,
            Password = _faker.Internet.Password()
        };

        await FluentActions.Invoking(() => client.Authentication(authRequest))
            .Should()
            .ThrowAsync<HttpRequestException>()
            .WithMessage("*Invalid password!*");
    }

    [Fact]
    public async void Login_user_with_nonexistent_email_gives_error()
    {
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var authRequest = new AuthRequest()
        {
            Email =  _faker.Person.Email,
            Password = _faker.Internet.Password()
        };

        await FluentActions.Invoking(() => client.Authentication(authRequest))
            .Should()
            .ThrowAsync<HttpRequestException>()
            .WithMessage("*There is no such account!*");
    }
}