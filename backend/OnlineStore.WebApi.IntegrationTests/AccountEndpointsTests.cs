using Bogus;
using FluentAssertions;
using OnlineStore.HttpApiClient;
using OnlineStore.Models.Requests;

namespace OnlineStore.WebApi.IntegrationTests;

public class AccountEndpointsTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private static Faker<RegisterRequest> GetRegisterRequestFaker()
    {
        return new Faker<RegisterRequest>()
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f => f.Internet.Password(8));
    }
    
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly Faker _faker = new("ru");
    private readonly Faker<RegisterRequest> _accountRequestFakes = GetRegisterRequestFaker();
    

    public AccountEndpointsTests(CustomWebApplicationFactory<Program> factory)
    {
            // В реальном проекте эти данные будут храниться в переменных окружения тестового контура (CI).
            Environment.SetEnvironmentVariable("ASPNETCORE_JwtConfig:SigningKey", "1AC9ED10-0A0B-426E-9997-3ADE426704D6", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("ASPNETCORE_JwtConfig:LifeTime", "180.00:00:00", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("ASPNETCORE_JwtConfig:Audience", "OnlineStore.Client", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("ASPNETCORE_JwtConfig:Issuer", "OnlineStore.Backend", EnvironmentVariableTarget.Process);

            _factory = factory;
    }
    
    [Fact]
    public async Task Get_current_account_from_authorized_user_works()
    {
        // Arrange
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.UserName,
            Password = _faker.Internet.Password()
        };
        await client.Register(registerRequest);

        // Act
        var account = await client.GetCurrentAccount();

        // Assert
        account.Name.Should().Be(registerRequest.Name);
        account.Email.Should().Be(registerRequest.Email);
    }
    
    [Fact]
    public async Task Get_current_account_from_UNauthorized_user_gives_error()
    {
        // Arrange
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        
        // Act and Assert
        await FluentActions.Invoking(() => client.GetCurrentAccount())
            .Should().ThrowAsync<HttpRequestException>();
    }
    
    [Fact]
    public async Task Get_accounts_from_authorized_admin_works()
    {
        // Arrange
        var accountsCount = 10;
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var users = _accountRequestFakes.Generate(accountsCount);
        await Task.WhenAll(users.Select(request => client.Register(request)));

        var adminReqest = _accountRequestFakes.Generate();
        var admin = await client.Register(adminReqest);
        
        await client.GrantAdmin(admin.AccountId, "123");
        await client.Authentication(new AuthRequest() {Email = admin.Email, Password = adminReqest.Password});

        // Act
        var accounts = await client.GetAllAccounts();

        // Assert
        accounts.Should().HaveCount(accountsCount + 1);
        //TODO проверить, что аккаунты соответствуют тем, которые мы регистрировали
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