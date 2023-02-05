using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;
using OnlineStore.WebApi.Extensions;

namespace OnlineStore.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var (account, token) = await _accountService.Register(request.Name, request.Email, request.Password, cancellationToken);
            return new RegisterResponse(account.Id, account.Name, account.Email, token);
        }
        catch (EmailExistsException)
        {
            return BadRequest("Email already exists");
        }
    }

    [HttpPost("authentication")]
    public async Task<ActionResult<AuthResponse>> Authentication(AuthRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var (account, token) = await _accountService.Authentication(request.Email, request.Password, cancellationToken);
            return new AuthResponse(account.Id, account.Name, account.Email, token);
        }
        catch (EmailNotFoundException)
        {
            return Unauthorized("There is no such account");
        }
        catch (InvalidPasswordException)
        {
            return Unauthorized("Invalid password");
        }
    }

    [Authorize]
    [HttpGet("get_current")]
    public async Task<ActionResult<Account>> GetCurrentAccount(CancellationToken cancellationToken) =>
        await _accountService.GetAccount(User.GetAccountId(), cancellationToken);

    [Authorize(Roles = $"{Roles.Admin}")]
    [HttpGet("get_all")]
    public async Task<IReadOnlyCollection<Account>> GetAllAccounts(CancellationToken cancellationToken) =>
        await _accountService.GetAll(cancellationToken);
}