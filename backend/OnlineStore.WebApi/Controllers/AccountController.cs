using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Requests;

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
    public async Task<ActionResult<Account>> Register(RegisterRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var account = await _accountService.Register(request.Name, request.Email, request.Password, cts);
            return account;
        }
        catch (EmailExistsException)
        {
            return BadRequest("Такой email уже зарегистрирован");
        }
    }

    [HttpPost("authentication")]
    public async Task<ActionResult<(Guid,string)>> Authentication(AuthRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        try
        {
            var account = await _accountService.Authentication(request.Email, request.Password, cts);
            return (account.Id, account.Name);
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
}