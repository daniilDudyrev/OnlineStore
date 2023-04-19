using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
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
        var (account, token) = await _accountService.Register(request.Name, request.Email, request.Password, cancellationToken);
        return new RegisterResponse(account.Id, account.Name, account.Email, token);
    }

    [HttpPost("authentication")]
    public async Task<ActionResult<AuthResponse>> Authentication(AuthRequest request, CancellationToken cancellationToken)
    {
        var (account, token) = await _accountService.Authentication(request.Email, request.Password, cancellationToken);
        return new AuthResponse(account.Id, account.Name, account.Email, token);
    }

    [Authorize]
    [HttpGet("get_current")]
    public async Task<ActionResult<Account>> GetCurrentAccount(CancellationToken cancellationToken) =>
        await _accountService.GetAccount(User.GetAccountId(), cancellationToken);

    [Authorize(Roles = $"{Roles.User}")]
    [HttpGet("get_all")]
    public Task<IReadOnlyCollection<Account>> GetAllAccounts(CancellationToken cancellationToken)
    {
        return _accountService.GetAll(cancellationToken);
    }

    [Authorize]
    [HttpGet("grant_admin")]
    public async Task<IActionResult> GrantAdmin(
        Guid accountId, 
        string key,
        CancellationToken cancellationToken)
    {
        if (key != "123")
        {
            return new ObjectResult("Invalid Key")
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
        
        await _accountService.GrantAdmin(accountId, cancellationToken);
        return Ok();
    }
}