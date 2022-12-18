using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
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
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var (account, token) = await _accountService.Register(request.Name, request.Email, request.Password, cts);
            return new RegisterResponse(account.Id, account.Name, account.Email, token);
        }
        catch (EmailExistsException)
        {
            return BadRequest("Такой email уже зарегистрирован");
        }
    }

    [HttpPost("authentication")]
    public async Task<ActionResult<AuthResponse>> Authentication(AuthRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var (account, token) = await _accountService.Authentication(request.Email, request.Password, cts);
            return new AuthResponse(account.Id, account.Name, account.Email, token);
        }
        catch (EmailNotFoundException)
        {
            return Unauthorized("Такого аккаунта не существует");
        }
        catch (InvalidPasswordException)
        {
            return Unauthorized("Неверный пароль");
        }
    }

    [Authorize]
    [HttpGet("get_current")]
    public async Task<ActionResult<Account>> GetCurrentAccount()
    {
        // var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // if (strId == null)
        // {
        //     return Unauthorized();
        // }
        //
        // var userId = Guid.Parse(strId);
        // var account = await _accountService.GetAccount(userId);
        // return account;
        return await _accountService.GetAccount(User.GetAccountId());
    }
}